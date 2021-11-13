using System;
using System.Data;
using System.Data.SQLite;

namespace DotNet_SQLite
{
  class SqlAccess
  {
    public static void createTable() {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(con);
        cmd.CommandText = @"CREATE TABLE logs(id INTEGER PRIMARY KEY, hours INT)";
        cmd.ExecuteNonQuery();
      }
    }

    public static void AddLog(int hours) {
      using(SQLiteConnection con = new SQLiteConnection("Data Source=./abhinav.db;Version=3;")){
        con.Open();
        using var cmd = new SQLiteCommand(con);
        cmd.CommandText = "INSERT INTO logs(hours) VALUES(" + hours + ")";
        cmd.ExecuteNonQuery();
      }
    }
  }
}
