using System;
using System.IO;
using System.Data.SQLite;

namespace DotNet_SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
          if (File.Exists(@"abhinavDb.sqlite")) {
            Console.WriteLine("abhinavDb already exists.");
          }

          else {
            Console.WriteLine("Creating abhinavDb.sqlite");
            SQLiteConnection.CreateFile("abhinavDb.sqlite");
          }
        }
    }
}
