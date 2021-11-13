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
          if (!File.Exists(@"abhinav.db")) {
            SQLiteConnection.CreateFile("abhinav.db");
          }
          SqlAccess.createTable();
          SqlAccess.AddLog(5);
          SqlAccess.AddLog(10);
        }
    }
}
