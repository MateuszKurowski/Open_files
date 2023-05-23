using System.Diagnostics;
using System.Text;

namespace OpenFile;

public class Manager
{
    public FileObject[] FilesToOpen { get; set; }

    private readonly StringBuilder _Logger;
    private bool _WasError;

    public Manager(FileObject[] filesToOpen)
    {
        if (filesToOpen is null || filesToOpen.Length == 0)
            throw new ArgumentNullException(nameof(filesToOpen));

        FilesToOpen = filesToOpen;
        _Logger = new StringBuilder();
        _WasError = false;
    }

    public void OpenFiles()
    {
        for (var i = 0; i < FilesToOpen.Length; i++)
        {
            _Logger.AppendLine($"Otwieranie pliku o nazwie {FilesToOpen[i]?.Name} z ścieżki '{FilesToOpen[i]?.Path}' zostało rozpoczęte...");
            try
            {
                if (!File.Exists(FilesToOpen[i].Path))
                {
                    _Logger.AppendLine($"Plik o nazwie {FilesToOpen[i]?.Name} nie został odnaleziony!!!");
                    _Logger.AppendLine();
                    _WasError = true;
                    continue;
                }

                if (IsProcessOpen(FilesToOpen[i].GetProcessName()))
                {
                    _Logger.AppendLine($"Plik o nazwie {FilesToOpen[i]?.Name} jest już uruchomiony!");
                    _Logger.AppendLine();
                    continue;
                }

                var startInfo = new ProcessStartInfo(FilesToOpen[i].Path)
                {
                    Arguments = FilesToOpen[i].GetParameters()
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                _Logger.AppendLine($"Otwieranie pliku o nazwie {FilesToOpen[i]?.Name} z ścieżki '{FilesToOpen[i]?.Path}' zostało przerwane! Błąd: {ex?.Message}");
                _Logger.AppendLine();
                _WasError = true;
                continue;
            }
            _Logger.AppendLine($"Plik {FilesToOpen[i].Name} został otwarty.");

            _Logger.AppendLine();
        }

        _Logger.AppendLine("=====================================================================================");
        _Logger.AppendLine("Koniec!");
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(_Logger.ToString());
        if (_WasError)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Nie wszystkie aplikacje udało się uruchomić pomyślnie. Sprawdź powyższe komunikaty!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Naciśnij dowolny przycisk aby wyjść.");
            WriteToFile();
            Console.ReadKey();
        }
        else
        {
            WriteToFile();
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }

    private static bool IsProcessOpen(string name)
    {
        foreach (var clsProcess in Process.GetProcesses())
        {
            if (clsProcess.ProcessName.Contains(name))
                return true;
        }
        return false;
    }

    private void WriteToFile()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var fullPath = Path.Combine(basePath, "logger.txt");
        try
        {
            File.WriteAllText(fullPath, _Logger.ToString());
        }
        catch (Exception)
        {
        }
    }
}