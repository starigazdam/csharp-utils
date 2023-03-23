using Newtonsoft.Json;

namespace JsonManipulation.Example
{
    public static class JsonLazyLoadWrite
    {

        public static void LoadAndWrite(string srcFile, string dstFile)
        {
            using var streamReader = new StreamReader(srcFile);
            using var jsonReader = new JsonTextReader(streamReader);

            using var streamWriter = new StreamWriter(dstFile);
            using var jsonTextWriter = new JsonTextWriter(streamWriter);

            //jsonTextWriter.Formatting = Formatting.Indented;
            jsonTextWriter.WriteStartArray();

            var serializer = new JsonSerializer();
            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                {
                    dynamic? item = serializer.Deserialize(jsonReader);
                    var id = (string)(item?.id ?? "null");

                    if (id != null)
                    {
                        serializer.Serialize(jsonTextWriter, item);
                    }
                }
            }
            jsonTextWriter.WriteEndArray();
        }
    }
}