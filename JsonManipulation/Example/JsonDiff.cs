using Newtonsoft.Json;

namespace JsonManipulation.Example;

public class JsonDiff
{
    public static void FindDiffInJsonFiles(string srcFileA, string srcFileB)
    {
        var setA = new HashSet<string>();
        var setB = new HashSet<string>();
        Load(srcFileA, srcFileB, setA, setB);

        var difflist = new List<string>();
        foreach (var item in setB)
        {
            if (!setA.Contains(item))
            {
                difflist.Add(item.ToString());
            }
        }

        foreach (var item in difflist)
        {
            Console.WriteLine(item);
        }
    }

    private static void Load(string srcFileA, string srcFileB, HashSet<string> setA, HashSet<string> setB)
    {
        foreach (var (file, setVar) in new[] { (srcFileA, setA), (srcFileB, setB) })
        {
            using var streamReader = new StreamReader(file);
            using var jsonReader = new JsonTextReader(streamReader);

            var serializer = new JsonSerializer();
            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.StartObject)
                {
                    dynamic? item = serializer.Deserialize(jsonReader);
                    var id = (string?)item?.id;
                    if (id != null)
                    {
                        setVar.Add(id);
                    }
                }
            }
        }
    }
}
