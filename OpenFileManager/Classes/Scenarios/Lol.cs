namespace OpenFile.Scenarios;

public class Lol : FileObject
{
    public Lol(string path = "G:\\Riot Games\\Riot Client\\RiotClientServices.exe", string name = "Liga") : base(name, path)
    {
        Parameters = new string[] { "--launch-product=league_of_legends", "--launch-patchline=live" };
    }

    public static Lol CreateDefault()
    {
        return new Lol();
    }
}