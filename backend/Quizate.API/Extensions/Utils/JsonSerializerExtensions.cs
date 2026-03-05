using System.Text.Json;

namespace Quizate.API.Extensions.Utils;

public static class JsonSerializerExtensions
{
    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public static string SerializeWithCamelCasing(this object obj)
    {
        return JsonSerializer.Serialize(obj, options);
    }
}
