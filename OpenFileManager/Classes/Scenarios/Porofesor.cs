namespace OpenFile.Scenarios;

public class Porofesor : FileObject
{
    public Porofesor(string path = "P:\\Overwolf\\OverwolfLauncher.exe", string name = "Porofesor") : base(name, path)
    {
        Parameters = new string[] { "-launchapp pibhbkkgefgheeglaeemkkfjlhidhcedalapdggh", "-from-startmenu" };
        SetCustomProcessName("Porofessor.gg");
    }

    public static Porofesor CreateDefault()
    {
        return new Porofesor();
    }
}