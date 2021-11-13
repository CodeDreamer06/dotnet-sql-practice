using System;
using System.Data;
using System.Data.SQLite;

namespace DotNet_SQLite
{
  class SqlAccess
  {
    protected static void execute(string query) {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(con);
        cmd.ExecuteNonQuery();
      }
    }

    public static void createTable() {
      execute(@"CREATE TABLE logs(id INTEGER PRIMARY KEY, hours INT)");
    }

    public static void getLogs() {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(@"select * from logs", con);
        SQLiteDataReader reader = cmd.ExecuteReader();
        while (reader.Read()){
          Console.WriteLine("Log " + reader["id"] + ": " + reader["hours"] + " hours");
        }
      }
    }

    public static void AddLog(int hours) {
      execute("INSERT INTO logs(hours) VALUES(" + hours + ")");
    }
  }
}
