using Microsoft.VisualBasic.FileIO;

namespace Alternative_Language_Project {

    public class Database {

        private Dictionary<int, Cell> database = new Dictionary<int, Cell>(1000);
        private string file;

        public Database(string file){
            this.file = file;
        }

        public void createDatabase(){
             TextFieldParser parser = new TextFieldParser(new StreamReader(File.OpenRead(file)));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            string[]? fields;
            //skips column titles
            fields = parser.ReadFields();
            int phoneID = 0;
            while(!parser.EndOfData) {
                fields = parser.ReadFields();
                // database.Add(phoneID, new Cell(fields));
                // phoneID++;
                // foreach(string field in fields){
                //     Console.WriteLine(field);
                // }
                Console.WriteLine(fields.Length + ", " + phoneID);
                phoneID++;
            }
            parser.Close();
            Console.ReadKey();
        }

    }
}