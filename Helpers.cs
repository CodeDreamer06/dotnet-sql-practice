using System;
using System.Data;

namespace DotNet_SQLite
{
  class Helpers
  {
    public static int splitInteger(string word, string keyword, string errorMessage) {
      try {
        return Convert.ToInt32(word.Split(keyword)[1]);
      }
      catch(System.FormatException) {
        Console.WriteLine(errorMessage);
        return 0;
      }
    }

    public static string splitString(string word, string keyword, string errorMessage) {
      // TODO: Merge both splitString and splitInteger functions using typeof()
      try {
        return Convert.ToString(word.Split(keyword)[1]);
      }
      catch(System.FormatException) {
        Console.WriteLine(errorMessage);
        return "";
      }
    }
  }
}
