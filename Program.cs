using Library.src;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbPath = Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "library.db");

            if (args[0] == "-cli")
            {
                var cli = new CLIProgram(dbPath);
                cli.Run();
            }
        }
    }
}
