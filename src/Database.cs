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
            //skips first row ( column titles)
            fields = parser.ReadFields();
            int phoneID = 0;
            while(!parser.EndOfData) {
            // for(int i = 0; i < 30; i++) {
                //saves all data from current row, into array 
                fields = parser.ReadFields();
                //creates new phone object with obtained data and adds to database
                database.Add(phoneID, new Cell(fields));
                
                phoneID++;
 
            }
            
            parser.Close();
            Console.ReadKey();
        }

    }
}