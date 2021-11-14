// Data Source=c:\mydb.db;Version=3;
﻿using System;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace DotNet_SQLite
{
    class Program
    {

      static void Main(string[] args)
      {
          var command = "";
          const string help = @"
exit or 0: stop the program
add: insert data into the database
show: display existing logs";
          if (!File.Exists(@"abhinav.db")) {
            SQLiteConnection.CreateFile("abhinav.db");
          }

          Console.WriteLine("Hi there! Type a command to get started");
          while(true) {
            command = Console.ReadLine().ToLower();

            if(command == "exit" || command == "0")
              break;

            else if(command == "help")
              Console.WriteLine(help);

            else if(command.StartsWith("add")){
              int hours = Convert.ToInt32(command.Split("add")[1]);
              SqlAccess.AddLog(hours);
            }

            else if(command == "show")
              SqlAccess.getLogs();

            else Console.WriteLine("Not a command. Use 'help' if required. ");
          }
        }
    }
}
