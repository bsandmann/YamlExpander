using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: YamlExpander.exe <input_file.yaml> <output_file.yaml>");
            return;
        }

        string inputFile = args[0];
        string outputFile = args[1];

        try
        {
            string yaml = File.ReadAllText(inputFile);
            var expandedYaml = ExpandYaml(yaml);
            File.WriteAllText(outputFile, expandedYaml);
            Console.WriteLine($"YAML expanded successfully. Output written to {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static string ExpandYaml(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var expander = new YamlExpander();
        var expandedObject = expander.Expand(deserializer.Deserialize<object>(yaml));

        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .Build();

        return serializer.Serialize(expandedObject);
    }
}