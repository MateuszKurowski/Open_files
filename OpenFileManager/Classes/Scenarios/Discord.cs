namespace OpenFile.Scenarios;

public class Discord : FileObject
{
    public Discord(string path = "C:\\Users\\Mateusz\\AppData\\Local\\Discord\\Update.exe", string name = "Discord") : base(name, path)
    {
        Parameters = new string[] { "--processStart Discord.exe" };
        SetCustomProcessName("Discord");
    }

    public static Discord CreateDefault()
    {
        return new Discord();
    }
}