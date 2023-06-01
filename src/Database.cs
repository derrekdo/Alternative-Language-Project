using Microsoft.VisualBasic.FileIO;
using System.Text;

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
                //saves all data from current row, into array 
                fields = parser.ReadFields();
                //creates new phone object with obtained data and adds to database
                database.Add(phoneID, new Cell(fields));
                // Console.WriteLine(database[phoneID].getBody_dimensions());
                phoneID++;
                
            }
            
            parser.Close();
        }

        public void removePhone(int index){
            database.Remove(index);
        }

        public void addPhone() {
            Console.WriteLine(database[0]);
        }

        public string? getPhone(int index) {
            if (!database.ContainsKey(index)) {
                return null;
            }
            StringBuilder specs = new StringBuilder("\n\t\tAbout");
            specs.Append("\nCompany:\t\t" + database[index].getOem());
            specs.Append("\nModel:\t\t\t" + database[index].getModel());
            specs.Append("\nYear Announced:\t\t" + database[index].getLaunch_announced());
            specs.Append("\nYear Launched:\t\t" + database[index].getLaunch_status());
            specs.Append("\nDimensions:\t\t" + database[index].getBody_dimensions());
            specs.Append("\nWeight:\t\t\t" + database[index].getBody_weight() + " g");
            specs.Append("\nSim Type:\t\t" + database[index].getBody_sim());
            specs.Append("\nDisplay Type:\t\t" + database[index].getDisplay_type());
            specs.Append("\nDisplay Size:\t\t" + database[index].getDisplay_size());
            specs.Append("\nDisplay Resolution:\t" + database[index].getDisplay_resolution());
            specs.Append("\nSensors:\t\t" + database[index].getFeatures_sensors());
            specs.Append("\nOperating System:\t\t" + database[index].getPlatform_os());
            return specs + "";
        }
    }
}