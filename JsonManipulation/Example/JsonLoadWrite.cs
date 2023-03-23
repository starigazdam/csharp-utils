using Newtonsoft.Json;

namespace JsonManipulation.Example;

public static class JsonLoadWrite
{
    public static void LoadAndWrite(string srcFile, string dstFile)
    {
        var array = LoadAsDynamic(srcFile);
        Write(array, dstFile);
    }

    public static dynamic? LoadAsDynamic(string file)
    {
        using var r = new StreamReader(file);
        var content = r.ReadToEnd();

        dynamic? array = JsonConvert.DeserializeObject(content);
        foreach (var item in array ?? Enumerable.Empty<dynamic>())
        {
            item.Id = ((string)item.id).ToLower();
        }

        return array;
    }

    public static void Write(object? array, string file)
    {
        using var w = new StreamWriter(file);
        var text = JsonConvert.SerializeObject(array, Formatting.Indented);
        w.Write(text);
    }
}
