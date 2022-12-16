using System.Security.Cryptography;
using System.Text;
using ACMESharp.Crypto.JOSE;
using ACMESharp.Protocol;
using Furion.DependencyInjection;
using Furion.Logging;
using Jx.Gateway.Entity;
using Jx.Toolbox.Extensions;
using Microsoft.AspNetCore.Identity;
using Constants = Jx.Gateway.Utils.Constants;

namespace Jx.Gateway.Services;

public class AcmeService: ITransient
{
    public async Task StartChallenge(AcmeEntity acmeEntity)
    {
        Log.Information("开始申请证书");
        var url = new Uri(acmeEntity.CaUrl);
        using (var acme = new AcmeProtocolClient(url))
        {
            IJwsTool accountSigner = default;
            if (acmeEntity.Directory == null)
            {
                Log.Information("刷新目录信息");
                acmeEntity.Directory = await acme.GetDirectoryAsync();
                Log.Information("目录信息刷新完成");
            }

            await acme.GetNonceAsync();

            if (acmeEntity.AccountKey != null)
            {
                accountSigner = acmeEntity.AccountKey.GenerateTool();
            }

            if (acmeEntity.AccountDetails == null || acmeEntity.AccountKey == null)
            {
                if (acmeEntity.Email is null or {Count: 0})
                {
                    throw new Exception("Email不能为空");
                }
                
                acmeEntity.AccountDetails = await acme.CreateAccountAsync(acmeEntity.Email.Select(x => $"mailto:{x}"));
                accountSigner = acme.Signer;

                acmeEntity.AccountKey = new AcmeAccountKeyEntity()
                {
                    KeyType = accountSigner.JwsAlg,
                    KeyExport = accountSigner.Export()
                };
            }

            if (acmeEntity.Domains is null or {Count: 0})
            {
                return;
            }
            
            acmeEntity.Domains = acmeEntity.Domains.Distinct().ToList();

            var certName = string.Join(",", acmeEntity.Domains).Replace("%", "");
            Log.Information($"申请证书：{certName}");
            var certNameHash = ComputeHash(certName);
            var orderId = certNameHash;

            if (acmeEntity.Order == null)
            {
                Log.Information("创建订单");
                acmeEntity.Order = await acme.CreateOrderAsync(acmeEntity.Domains);
            }
            
            if (acmeEntity.RefreshOrder)
            {
                Log.Information("刷新订单");
                acmeEntity.Order = await acme.GetOrderDetailsAsync(acmeEntity.Order.OrderUrl, existing: acmeEntity.Order);
            }
            
            Log.Information(
                    $"""
                            订单路径:{acmeEntity.Order.OrderUrl}  
                            订单状态:{acmeEntity.Order.Payload.Status}
                            订单过期时间:{acmeEntity.Order.Payload.Expires}
                            """);

            if (acmeEntity.Order.Payload.Status == Constants.InvalidStatus)
            {
                throw new Exception("订单状态无效");
            }
            
            if (acmeEntity.Order.Payload.Status == Constants.PendingStatus)
            {
                Log.Information("开始处理订单");
                foreach (var authorization in acmeEntity.Order.Payload.Authorizations)
                {
                    if (authorization.IsNullOrEmpty())
                    {
                        continue;
                    }

                    if (!acmeEntity.Authorizations.TryGetValue(authorization, out var authorizationDetails) || acmeEntity.RefreshOrder)
                    {
                        Log.Information($"获取授权信息：{authorization}");
                        authorizationDetails = await acme.GetAuthorizationDetailsAsync(authorization);
                        acmeEntity.Authorizations[authorization] = authorizationDetails;
                    }
                    
                    Log.Information(
                        $"""
                            Identifier:{authorizationDetails.Identifier.Value}  
                            授权状态:{authorizationDetails.Status}
                            授权过期时间:{authorizationDetails.Expires}
                            """);
                }
            }
        }
    }
    
    private string ComputeHash(string value)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
        return BitConverter.ToString(hash).Replace("-", "");
    }
}