using Library.src;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbPath = Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "library.db");

            ReadDatabase readDatabase = new(dbPath);
            var running = true;
            var account = readDatabase.GetAccount(1);

            if (account == null)
            {
                Console.WriteLine("Account does not exist");
                Environment.Exit(1);
            }

            CLI app = new CLI(account, dbPath);
            app.DisplayUI();

            do
            {
                var result = app.UpdateUI();

                if (result == -1)
                {
                    running = false;
                }
            } while (running);

            readDatabase.SaveData(account);
        }
    }
}
