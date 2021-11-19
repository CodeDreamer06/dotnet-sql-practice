using System;
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
# Code Time
  A simple code time manager for you to measure your progress!
* exit or 0: stop the program
* show: display existing logs ('today', 'yesterday' or a date can optionally be specified)
* add [hours]: insert data into the database
* remove: delete the last recent log (A log id can optionally be specified)
* update [id] [hours]: change existing data in the database

Also, please note that you can't add 0 hours or more than the hours possible on that day (You can't add more than 6 hours for example, if it's 6:00AM)
";
          SqlAccess.createTable();
          Console.WriteLine("Welcome to CodeTime! Type a command to get started");
          while(true) {
            command = Console.ReadLine().ToLower();

            if(command == "exit" || command == "0")
              break;

            else if(command == "help")
              Console.WriteLine(help);

            else if(command.StartsWith("add")){
              int hours = Helpers.splitInteger(command, "add", "Add commands should be in this format: 'add [number]'. \nFor example: 'add 5' means 5 hours");
               SqlAccess.AddLog(hours);
            }

            else if(command.StartsWith("remove")){
              if(command == "remove") {
                SqlAccess.removeLastLog();
                continue;
              }

              int id = Helpers.splitInteger(command, "remove", "Add commands should be in this format: 'remove [id]'. \nFor example: 'remove 3' deletes the third log");
              SqlAccess.removeLog(id);
            }

            else if(command.StartsWith("show")) {
              if(command == "show") {
                SqlAccess.getLogs();
                continue;
              }

              string day = Helpers.splitString(command, "show", "show commands should be in this format: 'show [today, yesterday or date]. \n For example: 'show today' shows logs from today");
              SqlAccess.getTimedLogs(day);
            }

            else if(command.StartsWith("update"))
              SqlAccess.updateLog(Convert.ToInt32(command.Split()[1]), Convert.ToInt32(command.Split()[2]));

            else Console.WriteLine("Not a command. Use 'help' if required. ");
          }
        }
    }
}
