namespace HogWarp.Mongo.Models;

public sealed class MongoSettings
{
    public string Database { get; set; } = default!;
    public string Host { get; set; } = default!;
    public int Port { get; set; } = 27017;
}
