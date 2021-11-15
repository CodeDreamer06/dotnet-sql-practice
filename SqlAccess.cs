using System;
using System.Data;
using System.Data.SQLite;

namespace DotNet_SQLite
{
  class SqlAccess
  {
    protected static void execute(string query) {
      try {
        using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
          con.Open();
          using var cmd = new SQLiteCommand(query, con);
          cmd.ExecuteNonQuery();
        }
      }

      catch {
        Console.WriteLine("An unknown error occured.");
      }
    }

    public static void getLogs() {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(@"select * from logs", con);
        SQLiteDataReader reader = cmd.ExecuteReader();
        while (reader.Read()){
          string suffix = (int) reader["hours"] == 1 ? " hour" : " hours";
          Console.WriteLine("Log " + reader["id"] + ": " + reader["hours"] + suffix);
        }
      }
    }

    public static void createTable() {
      execute(@"CREATE TABLE logs(id INTEGER PRIMARY KEY, hours INT)");
    }

    // public static void checkIfTableExists() {
    //
    // }

    public static void AddLog(int hours) {
      if(hours == 0 || hours > 24) return; // Logging 0 hours isn't required, neither can you code more than 24 hours a day
      execute("INSERT INTO logs(hours) VALUES(" + hours + ")");
    }
  }
}
