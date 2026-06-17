using Microsoft.Extensions.Options;
using SpecDriven.Runner.Config;

public class PipelineExecutor
{
    private readonly PipelineSettings _settings;

    public PipelineExecutor(IOptions<PipelineSettings> options)
    {
        _settings = options.Value;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("🚀 Avvio pipeline");

        var inputPath = Path.Combine(_settings.InputPath, "analysis.md");

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"❌ Nessun file trovato in {_settings.InputPath}");
            return;
        }

        var input = File.ReadAllText(inputPath);

        // STEP 1
        var structPrompt = PromptLoader.Load(_settings.PipelinesPath, "orchestrator-pipeline-auto.md");
        var structResult = await Execute(structPrompt, input);

        Save("structuring-v1.md", structResult);

        Console.WriteLine("✅ STEP 1");

        // STEP 2
        var refPrompt = PromptLoader.Load(_settings.PipelinesPath, "orchestrator-pipeline-auto_Step2.md");
        var refResult = await Execute(refPrompt, structResult);

        Save("refinement-v1.md", refResult);

        Console.WriteLine("✅ STEP 2");

        Console.WriteLine("⛔ STOP (instructions.md)");
    }

    private Task<string> Execute(string prompt, string input)
    {
        // simulazione copilot
        return Task.FromResult($"[OUTPUT]\n\n{prompt}\n\n{input}");
    }

    private void Save(string fileName, string content)
    {
        Directory.CreateDirectory(_settings.OutputPath);

        var fullPath = Path.Combine(_settings.OutputPath, fileName);

        File.WriteAllText(fullPath, content);
    }
}