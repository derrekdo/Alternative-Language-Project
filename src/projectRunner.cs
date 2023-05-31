using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Alternative_Language_Project {

    public class ProjectRunner {

        static void Main(string[] args) {
            string file = Path.Combine(Directory.GetFiles("InputFile"));
            
            // Database phoneDatabase = new Database(file);
            // phoneDatabase.createDatabase();
        
          


            string pattern = @"[0-9]{1-3}\sg)";
            string input = "45 g (3G)/ 115 g (LTE) (4.06 oz)";
            
            
            Console.Write(MatchFound);
            // String[] arr = input.Split('g');
            // foreach (string c in arr) {
            //     Console.WriteLine(c);
            // }
            // string year = arr[0];
            // if (year.Length == 4 && Regex.IsMatch(year, pattern)) {
            //     Console.WriteLine("yes");
            // } else {
            //     Console.Write("no");
            // }
            Console.ReadKey();
        }
    }
}