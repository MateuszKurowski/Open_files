using OpenFile;
using OpenFile.Scenarios;

var filesToOpen = new FileObject[]
{
    Lol.CreateDefault(),
    Porofesor.CreateDefault(),
    Discord.CreateDefault(),
};

new Manager(filesToOpen).OpenFiles();