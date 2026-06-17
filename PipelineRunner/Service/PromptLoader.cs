public static class PromptLoader
{
    public static string Load(string basePath, string fileName)
    {
        var path = Path.Combine(basePath, fileName);

        if (!File.Exists(path))
            throw new Exception($"Prompt non trovato: {path}");

        return File.ReadAllText(path);
    }
}