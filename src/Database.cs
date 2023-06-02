using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace Alternative_Language_Project {

    public class Database {

        private Dictionary<int, Cell> database = new Dictionary<int, Cell>(1000);
        private Dictionary<string, int> platOsStats = new Dictionary<string, int>();
        private Dictionary<string, int> companyStats = new Dictionary<string, int>();
        private Dictionary<string, int> sensorsStats = new Dictionary<string, int>();
        private Queue<int> removedIndex = new Queue<int>();
        private float? totWeight;
        private int nullWeight = 0;
        private string file;
        private int phoneID = 0;

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
            while(!parser.EndOfData) {
                //saves all data from current row, into array 
                fields = parser.ReadFields();
                //creates new phone object with obtained data and adds to database
                database.Add(phoneID, new Cell(fields));
                buildStats();
                phoneID++;
            }
            parser.Close();
        }

        private void buildStats() {
            string data;
            if (database[phoneID].getPlatform_os() != null) {
                data = database[phoneID].getPlatform_os();
                if (!platOsStats.ContainsKey(data)) {
                    platOsStats.Add(data, 0);
                } else {
                    platOsStats[data] = platOsStats[data] + 1;
                }
            }
            if (database[phoneID].getOem() != null) {
                data = database[phoneID].getOem();
                if (!companyStats.ContainsKey(data)) {
                    companyStats.Add(data, 0);
                } else {
                    companyStats[data] = companyStats[data] + 1;
                }
            }
            if (database[phoneID].getFeatures_sensors() != null) {
                data = database[phoneID].getFeatures_sensors();
                if (!sensorsStats.ContainsKey(data)) {
                    sensorsStats.Add(data, 0);
                } else {
                    sensorsStats[data] = sensorsStats[data] + 1;
                }
            }
            if (database[phoneID].getBody_weight() != null) {
                totWeight += database[phoneID].getBody_weight();
            } else {
                nullWeight++;
            }
            
        }

        public void mostCommonSensors() {
            LinkedList<int> list = new LinkedList<int>(sensorsStats.Values);
            int max = 0;
            foreach(int i in list) {
                max = Math.Max(i, max);
            }
            string sensors = "";
            foreach(string s in sensorsStats.Keys) {
                if (sensorsStats[s] == max) {
                    sensors += sensorsStats[s] + " ";
                }
            }
            Console.WriteLine(sensors);
        }

        public void mostCommonOS() {
            LinkedList<int> list = new LinkedList<int>(platOsStats.Values);
            int max = 0;
            foreach(int i in list) {
                max = Math.Max(i, max);
            }

            string os = "";
            foreach(string s in platOsStats.Keys) {
                if (platOsStats[s] == max) {
                    os += platOsStats[s] + " ";
                }
            }
            Console.WriteLine(os);
        }

        public void mostCommonOem() {
            LinkedList<int> list = new LinkedList<int>(companyStats.Values);
            int max = 0;
            foreach(int i in list) {
                max = Math.Max(i, max);
            }
            string oem = "";
            foreach(string s in companyStats.Keys) {
                if (companyStats[s] == max) {
                    oem += companyStats[s] + " ";
                }
            }
            Console.WriteLine(oem);
        }

        public float? averageWeight() {
            return totWeight / (database.Count - nullWeight);
        } 

        public void removePhone(int index){
            if (database.ContainsKey(index)) {
                removedIndex.Enqueue(index);
                database.Remove(index);
            } else {
                Console.WriteLine("index does not exist");
            }
        }

        public int addPhone(string[] phoneData) {
            int index;
            if (removedIndex.Count != 0) {
                index = removedIndex.Dequeue();
                database.Add(index, new Cell(phoneData));
                return index;
            } else {
                database.Add(phoneID, new Cell(phoneData));
                index = phoneID;
                phoneID++;
                return index;
            }
        }

        public string? getPhone(int index) {
            if (!database.ContainsKey(index)) {
                return "Invalid Index";
            }
            
            StringBuilder specs = new StringBuilder("\n\t\tPhone ID: " + index);
            
            specs.Append("\nCompany:\t\t" + checkNullString(database[index].getOem()));
            specs.Append("\nModel:\t\t\t" + checkNullString(database[index].getModel()));
            specs.Append("\nYear Announced:\t\t" + checkNullInt(database[index].getLaunch_announced()));
            specs.Append("\nYear Launched:\t\t" + checkNullString(database[index].getLaunch_status()));
            specs.Append("\nDimensions:\t\t" + checkNullString(database[index].getBody_dimensions()));
            specs.Append("\nWeight(grams):\t\t" + checkNullFloat(database[index].getBody_weight()));
            specs.Append("\nSim Type:\t\t" + checkNullString(database[index].getBody_sim()));
            specs.Append("\nDisplay Type:\t\t" + checkNullString(database[index].getDisplay_type()));
            specs.Append("\nDisplay Size(inches):\t" + checkNullFloat(database[index].getDisplay_size()));
            specs.Append("\nDisplay Resolution:\t" + checkNullString(database[index].getDisplay_resolution()));
            specs.Append("\nSensors:\t\t" + checkNullString(database[index].getFeatures_sensors()));
            specs.Append("\nOperating System:\t" + checkNullString(database[index].getPlatform_os()));
            return specs + "";
        }

         private string? checkNullString(string? data) {
                if (data == null ){
                    return "Not Available";
                } else {
                    return data;
                }
        }

        private string? checkNullInt(int? data) {
            if (data == null){
                return "Not Available";
            } else {
                return data +"";
            }

        }

         private string? checkNullFloat(float? data) {
            if (data == null){
                return "Not Available";
            } else {
                return data +"";
            }

        }
        
        public string? getOem(int index) {
            return checkNullString(database[index].getOem());
        }

        public string? getModel(int index) {
            return checkNullString(database[index].getModel());
        }

        public string? getLaunch_announced(int index) {
            return checkNullInt(database[index].getLaunch_announced());
        }

        public string? getLaunch_status(int index) {
            return checkNullString(database[index].getLaunch_status());
        }

        public string? getBody_dimensions(int index) {
            return checkNullString(database[index].getBody_dimensions());
        }

        public string? getBody_weight(int index) {
            return checkNullFloat(database[index].getBody_weight());
        }

        public string? getBody_sim(int index) {
            return checkNullString(database[index].getBody_sim());
        }

        public string? getDisplay_type(int index) {
            return checkNullString(database[index].getDisplay_type());
        }

        public string? getDisplay_size(int index) {
            return checkNullFloat(database[index].getDisplay_size());
        }

        public string? getDisplay_resolution(int index) {
            return checkNullString(database[index].getDisplay_resolution());
        }

        public string? getFeatures_sensors(int index) {
            return checkNullString(database[index].getFeatures_sensors());
        }

        public string? getPlatform_os(int index) {
            return checkNullString(database[index].getPlatform_os());
        }
    }
}