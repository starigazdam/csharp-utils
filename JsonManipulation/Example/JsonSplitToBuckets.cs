using JsonManipulation.Model;
using Newtonsoft.Json;

namespace JsonManipulation.Example;

public class JsonSplitToBuckets
{
    public static void SplitToBuckets(int bucketSize, string srcFile, string dstDir)
    {
        using var streamReader = new StreamReader(srcFile);
        using var jsonReader = new JsonTextReader(streamReader);

        var cnt = 0;
        var bucket = new List<OrderGuid>(bucketSize);

        var serializer = new JsonSerializer();
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonToken.StartObject)
            {
                var id = serializer.Deserialize<OrderGuid>(jsonReader);
                if (string.IsNullOrEmpty(id?.Id))
                    continue;

                bucket.Add(id);
                if (bucket.Count == bucketSize)
                {
                    var bucketName = $"bucket_{++cnt}.json";
                    WriteBucket(serializer, bucket, bucketName);
                    bucket.Clear();
                }
            }
        }

        if (bucket.Any())
        {
            var bucketName = $"bucket_{++cnt}.json";
            WriteBucket(serializer, bucket, bucketName);
            bucket.Clear();
        }
    }

    private static void WriteBucket(JsonSerializer serializer, List<OrderGuid> bucket, string bucketName)
    {
        using var streamWriter = new StreamWriter(bucketName);
        using var jsonTextWriter = new JsonTextWriter(streamWriter);

        jsonTextWriter.Formatting = Formatting.Indented;
        jsonTextWriter.WriteStartArray();

        foreach (var item in bucket)
        {
            serializer.Serialize(jsonTextWriter, item);
        }

        jsonTextWriter.WriteEndArray();
    }
}
