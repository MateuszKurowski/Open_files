namespace OpenFile;

public class FileObject
{
    public string Name { get; init; }
    public string Path { get; init; }
    public string[] Parameters { get; init; } = Array.Empty<string>();
    public string? CustomProcessName { get; private set; } = null;

    public FileObject(string name, string path)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path));

        Name = name;
        Path = path;
    }

    public FileObject(string name, string path, string[] parameters) : this(name, path)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));
        Parameters = parameters;
    }

    public string GetParameters()
    {
        if (Parameters is null)
            return string.Empty;
        else
            return string.Join(' ', Parameters);
    }

    public string GetProcessName()
    {
        if (string.IsNullOrWhiteSpace(CustomProcessName))
            return System.IO.Path.GetFileNameWithoutExtension(Path);
        else
            return CustomProcessName;
    }

    public void SetCustomProcessName(string processName) => CustomProcessName = processName;
}