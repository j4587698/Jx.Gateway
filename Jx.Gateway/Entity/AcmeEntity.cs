using System.Diagnostics.CodeAnalysis;
using ACMESharp.Protocol;
using ACMESharp.Protocol.Resources;

namespace Jx.Gateway.Entity;

public class AcmeEntity
{
    public long Id { get; set; }

    [NotNull]
    public string? CaUrl { get; set; }

    public List<string>? Email { get; set; }

    public List<string>? Domains { get; set; }

    public ServiceDirectory? Directory { get; set; }

    public AccountDetails? AccountDetails { get; set; }

    public AcmeAccountKeyEntity? AccountKey { get; set; }

    public OrderDetails? Order { get; set; }

    public bool RefreshOrder { get; set; }

    public Dictionary<string, Authorization> Authorizations { get; set; } = new();
}