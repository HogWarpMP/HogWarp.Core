namespace HogWarp.Common.IO;

public sealed class ConfigurationLoader
{
    private static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public static T? LoadFromJson<T>() where T : class
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, typeof(T).Name.ToLower() + ".json");
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return null;
        }

        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return null;
        }
    }
}
