using Jx.Gateway.Entity;
using LiteDB;

namespace Jx.Gateway.Utils;

public class LitedbHelper
{
    private static ILiteDatabase? _database;
    
    private const string GatewayTableName = "gateway";
    
    public static ILiteDatabase GetDatabase()
    {
        if (!Directory.Exists("./db"))
        {
            Directory.CreateDirectory("./db");
        }
        return _database ??= new LiteDatabase("./db/Gateway.db");
    }
    
    public static ILiteCollection<GatewayEntity> GetGatewayCollection()
    {
        return GetDatabase().GetCollection<GatewayEntity>(GatewayTableName);
    }
}