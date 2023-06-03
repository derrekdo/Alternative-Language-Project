using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace Alternative_Language_Project {

    public class Database {

        private Dictionary<int, Cell> database = new Dictionary<int, Cell>(1000);
        private Dictionary<string, int> platOsStats = new Dictionary<string, int>();
        private Dictionary<string, int> companyStats = new Dictionary<string, int>();
        private Dictionary<string, int> sensorsStats = new Dictionary<string, int>();
        private Queue<int> removedIndex = new Queue<int>();
        private Dictionary<string, Dictionary<int, float>> oemWeights = new Dictionary<string, Dictionary<int, float>>();
        private Dictionary<string, string> diffAnnouncedReleased = new Dictionary<string, string>();
        private int oneSensor = 0;
        private Dictionary<int, int> launched2000 = new Dictionary<int, int>();
        private float? totWeight = 0;
        private int nullWeight = 0;
        private string file;
        private int phoneID = 0;

        public Database(string file){
            this.file = file;
        }

        //reads the csv file and records the element in a cell object
        public void createDatabase(){
            //reads the file
            TextFieldParser parser = new TextFieldParser(new StreamReader(File.OpenRead(file)));
            //seperates columns
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            string[]? fields;
            //skips first row ( column titles)
            fields = parser.ReadFields();
            //reads through each line of the csv file
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
            //records how many phones use this OS
            if (database[phoneID].getPlatform_os() != null) {
                data = database[phoneID].getPlatform_os();
                if (!platOsStats.ContainsKey(data)) {
                    platOsStats.Add(data, 1);
                } else {
                    platOsStats[data] = platOsStats[data] + 1;
                }
            }
            //checks if the phone has a recorded company
            if (database[phoneID].getOem() != null) {
                data = database[phoneID].getOem();
                //counts how many phones are made by a compnay
                if (!companyStats.ContainsKey(data)) {
                    companyStats.Add(data, 1);
                } else {
                    companyStats[data] = companyStats[data] + 1;
                }
            }
            //checkss if the phone has features
            if (database[phoneID].getFeatures_sensors() != null) {
                data = database[phoneID].getFeatures_sensors();
                //find total phones wiht one feature
                string[] arr = data.Split(",");
                if (arr.Length == 1) {
                    oneSensor++;
                }
                //counts how many phones have this sensor
                if (!sensorsStats.ContainsKey(data)) {
                    sensorsStats.Add(data, 1);
                } else {
                    sensorsStats[data] = sensorsStats[data] + 1;
                }
            }

            //adds the weight of all phones
            if (database[phoneID].getBody_weight() != null) {
                totWeight += database[phoneID].getBody_weight();
            } else {
                //keep track of how many phones didnt not have a recorded weight
                nullWeight++;
            }
            
            
            string temp = database[phoneID].getLaunch_status();
            //Checks if the phone was able to release
            if (temp != "Cancelled" && temp != "Discontinued" && temp != null) {
                //if the launch year and announce years are not the same, save the model and oem
                if (database[phoneID].getLaunch_announced() != Convert.ToInt32(database[phoneID].getLaunch_status())) {
                    if (!diffAnnouncedReleased.ContainsKey(database[phoneID].getModel())) {
                        diffAnnouncedReleased.Add(database[phoneID].getModel(), database[phoneID].getOem());
                    }
                }
                //records total phone launched for each year in 2000's
                int year = Convert.ToInt32(database[phoneID].getLaunch_status());
                if (year >= 2000) {
                    if (!launched2000.ContainsKey(year)) {
                        launched2000.Add(year, 1);
                    } else {
                        launched2000[year] = launched2000[year] + 1;
                    }
                }
            }
            
        }

        //prints the model and oem of a phone that has diff announce and release year
        public void diffReleasedYear() {
            LinkedList<string> list = new LinkedList<string>(diffAnnouncedReleased.Keys);
            Console.WriteLine("Phones with different announce and release  years: ");
            foreach (string s in list) {
                Console.WriteLine("Model: " + s + " | Oem: " + diffAnnouncedReleased[s]);
            }
        }


        //prints the year with mosst launched phones
        public void mostLaunchYear() {
            LinkedList<int> list = new LinkedList<int>(launched2000.Values);
            int max = 0;
            //finds the highest amount launched
            foreach (int i in list) {
                max = Math.Max(i, max);
            }
            //matches it with correct year
            foreach(int y in launched2000.Keys) {
                if (launched2000[y] == max) {
                    Console.WriteLine("Year with most phones launched: " + y);
                }
            }
        }

        //prints total number of one sensor phones
        public void phoneOneSensor() {
            Console.WriteLine("Total Phones with one sensor: " + oneSensor);
        }

        //prints the most common sensor
        public void mostCommonSensors() {
            LinkedList<int> list = new LinkedList<int>(sensorsStats.Values);
            int max = 0;
            //finds the highest ammount
            foreach(int i in list) {
                max = Math.Max(i, max);
            }
            //matches the amount with the correect sensor
            foreach(string s in sensorsStats.Keys) {
                if (sensorsStats[s] == max) {
                    Console.WriteLine("Most Common Sensors: " + s);
                }
            }
            
        }

        //prints the most common OS
        public void mostCommonOS() {
            LinkedList<int> list = new LinkedList<int>(platOsStats.Values);
            int max = 0;
            //finds the highest amount 
            foreach(int i in list) {
                max = Math.Max(i, max);
            }
            //matches the amount with the correct OS
            foreach(string s in platOsStats.Keys) {
                if (platOsStats[s] == max) {
                    Console.WriteLine("Most Common OS: " + s);
                }
            }
            
        }

        //prints the Commpany with the mosot phones
        public void mostCommonOem() {
            LinkedList<int> list = new LinkedList<int>(companyStats.Values);
            int max = 0;
            //finds the highest amount
            foreach(int i in list) {
                max = Math.Max(i, max);
            }
            //matches amount iwth the correct company
            foreach(string s in companyStats.Keys) {
                if (companyStats[s] == max) {
                    Console.WriteLine("Most Common Company: " + s);
                }
            }
            
        }

        //returns the average weight of all phones
        public float? averageWeight() {
            return totWeight / (database.Count - nullWeight);
        } 

        //removes a phone from database
        public void removePhone(int index){
            if (database.ContainsKey(index)) {
                removedIndex.Enqueue(index);
                database.Remove(index);
            } else {
                Console.WriteLine("index does not exist");
            }
        }

        //add phones to data base with string of data
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

        //prints all elemtns of the phone
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

        //helper method to check if the string is null
         private string? checkNullString(string? data) {
                if (data == null ){
                    return "Not Available";
                } else {
                    return data;
                }
        }

        //helper method to check if int is null
        private string? checkNullInt(int? data) {
            if (data == null){
                return "Not Available";
            } else {
                return data +"";
            }

        }

        //helper method to check if float is n ull
         private string? checkNullFloat(float? data) {
            if (data == null){
                return "Not Available";
            } else {
                return data +"";
            }

        }
        
        //Getters and setters
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