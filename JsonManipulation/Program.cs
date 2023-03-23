using JsonManipulation.Example;

namespace JsonManipulation;

public class Program
{
    private const string sampleFile1 = @"Samples\sample-1.json";
    private const string sampleFile2 = @"Samples\sample-2.json";

    private static void Main(string[] _)
    {
        JsonLoadWrite.LoadAndWrite(sampleFile1, "output.json");
        JsonLazyLoadWrite.LoadAndWrite(sampleFile1, "output-lazywrite.json");
        JsonDiff.FindDiffInJsonFiles(sampleFile1, sampleFile2);
        JsonSplitToBuckets.SplitToBuckets(250_000, sampleFile1, ".");
    }
}