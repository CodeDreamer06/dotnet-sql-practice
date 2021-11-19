using System;
using System.Data;
using System.Data.SQLite;

namespace DotNet_SQLite
{
  class SqlAccess
  {
    protected static void execute(string query) {
      try {
        using(SQLiteConnection con = new SQLiteConnection("Data Source=./codeTime.db;Version=3;")){
          con.Open();
          using var cmd = new SQLiteCommand(query, con);
          cmd.ExecuteNonQuery();
        }
      }

      catch {
        Console.WriteLine("An unknown error occured.");
      }
    }

    public static void getTimedLogs(string day) {
      DateTime today = DateTime.Today;
      DateTime tomorrow = today.AddDays(-1);
      string dateToday = $"{today.Year}-{today.Month}-{today.Day}";
      string dateTomorrow = $"{tomorrow.Year}-{tomorrow.Month}-{tomorrow.Day}";
      if(day == "today")
        getLogs($"select * from logs WHERE created_at = \"{dateToday}\";");
      if(day == "yesterday")
        getLogs($"select * from logs WHERE created_at = \"{dateTomorrow}\";");
    }

    public static void getLogs(string query = @"select * from logs") {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./codeTime.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(query, con);
        SQLiteDataReader reader = cmd.ExecuteReader();
        while (reader.Read()){
          string suffix = (int) reader["hours"] == 1 ? " hour" : " hours";
          var time = Convert.ToDateTime(reader["created_at"]).ToString().Split()[0];
          Console.WriteLine($"Log {reader["id"]}: {reader["hours"]}{suffix} on {time}");
        }
      }
    }

    public static void createTable() {
      try {
        using(SQLiteConnection con = new SQLiteConnection("Data Source=./codeTime.db;Version=3;")){
          con.Open();
          var cmd = new SQLiteCommand(@"SELECT name FROM sqlite_master WHERE type='table' AND name='logs'", con);
           if(cmd.ExecuteScalar() == null)
            execute(@"CREATE TABLE logs(id INTEGER PRIMARY KEY, hours INT, created_at DATETIME DEFAULT CURRENT_DATE)");
        }
      }
      catch {
        Console.WriteLine("Unable to check if table exists.");
      }
    }

    public static void AddLog(int hours) {
      if(hours == 0 || hours > DateTime.Now.Hour) return; // Logging 0 hours isn't required, neither you code more than the number of hours already passed in the day
      execute($"INSERT INTO logs(hours) VALUES({hours});");
    }

    public static void removeLog(int index) {
      execute($"DELETE FROM logs WHERE id = {index};");
    }

    public static void removeLastLog() {
      execute(@"DELETE FROM logs WHERE id = (SELECT MAX(id) FROM logs);");
    }

    public static void updateLog(int id, int hours) {
      execute($"UPDATE logs SET hours = {hours}  WHERE id = {id}");
    }
  }
}
