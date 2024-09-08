using Library.src;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbPath = Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "library.db");
            var cli = new CLIProgram(dbPath);
            cli.Run();
        }
    }
}
