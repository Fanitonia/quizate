using System.Text.Json;

namespace Quizate.Application.Shared.Serializer;

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
