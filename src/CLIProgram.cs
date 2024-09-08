using System;
using System.IO;

namespace Library.src
{
    public class CLIProgam
    {

        private readonly string _dbPath;
        public CLIProgam(string path) => _dbPath = path;

        public void Run()
        {
            ReadDatabase readDatabase = new(_dbPath);
            var running = true;
            var account = readDatabase.GetAccount(1);

            if (account == null)
            {
                Console.WriteLine("Account does not exist");
                Environment.Exit(1);
            }

            CLI app = new CLI(account, _dbPath);
            app.DisplayUI();

            do
            {
                var result = app.UpdateUI();

                if (result == -1)
                {
                    running = false;
                }
            } while (running);

            //readDatabase.SaveData(account);
        }
    }
}
