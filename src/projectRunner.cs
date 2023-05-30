using Microsoft.VisualBasic.FileIO;

namespace Alternative_Language_Project {

    public class ProjectRunner {

        static void Main(string[] args) {
            string file = Path.Combine(Directory.GetFiles("InputFile"));
            
            Database phoneDatabase = new Database(file);
            phoneDatabase.createDatabase();
        }
    }
}