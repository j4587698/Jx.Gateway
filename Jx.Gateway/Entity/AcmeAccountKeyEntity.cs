using ACMESharp.Crypto.JOSE;

namespace Jx.Gateway.Entity;

public class AcmeAccountKeyEntity
{
    public string? KeyType { get; set; }
    public string? KeyExport { get; set; }

    public IJwsTool GenerateTool()
    {
        if (KeyType.StartsWith("ES"))
        {
            var tool = new ACMESharp.Crypto.JOSE.Impl.ESJwsTool();
            tool.HashSize = int.Parse(KeyType.Substring(2));
            tool.Init();
            tool.Import(KeyExport);
            return tool;
        }

        if (KeyType.StartsWith("RS"))
        {
            var tool = new ACMESharp.Crypto.JOSE.Impl.RSJwsTool();
            tool.HashSize = int.Parse(KeyType.Substring(2));
            tool.Init();
            tool.Import(KeyExport);
            return tool;
        }

        throw new Exception($"不支持此类加密 [{KeyType}]");
    }
}