namespace HogWarp.Mongo;

public sealed class Plugin : IPlugin
{
    public string Author { get; } = "HogWarp Team";
    public string Name { get; } = "HogWarp.Mongo";

    public Version Version { get; } = new(1, 0, 0);
    public DBContext? Db { get; private set; }

    private readonly bool _stopServer;
    private readonly Exception? _stopServerException;

    public Plugin()
    {
        try
        {
            var settings = ConfigurationLoader.LoadFromJson<MongoSettings>();
            if (settings is null)
                throw new FileNotFoundException("mongosettings.json not found.");
                
            DB.InitAsync(settings.Database, settings.Host, settings.Port);
            Db = new();
        }
        catch (Exception e)
        {
            _stopServerException = e;
            _stopServer = true;
        }
    }

    public void PostLoad()
    {
        if (_stopServer) 
            throw _stopServerException ?? new ApplicationException();
    }
    
    public void Shutdown() {}
}
