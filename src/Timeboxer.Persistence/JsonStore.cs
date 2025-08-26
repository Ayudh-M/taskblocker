using System.Text.Json;
using System.Text.Json.Serialization;

namespace Timeboxer.Persistence;

public static class JsonStore
{
  static readonly JsonSerializerOptions Options = new()
  {
    PropertyNameCaseInsensitive = true,
    Converters = { new JsonStringEnumConverter() },
    WriteIndented = true
  };

  public static async Task<T> LoadAsync<T>(string path, CancellationToken ct = default)
  {
    using var stream = File.OpenRead(path);
    var result = await JsonSerializer.DeserializeAsync<T>(stream, Options, ct);
    if (result == null) throw new InvalidDataException($"Failed to deserialize {path}");
    return result;
  }

  public static async Task SaveAsync<T>(string path, T data, CancellationToken ct = default)
  {
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    using var stream = File.Create(path);
    await JsonSerializer.SerializeAsync(stream, data, Options, ct);
  }
}
