using System.Text.RegularExpressions;
using System.Text;

namespace Alternative_Language_Project {
    class Cell {
        private string? oem;
        private string? model;
        private int? launch_announced;
        private string? launch_status;
        private string? body_dimensions;
        private float? body_weight;
        private string? body_sim;
        private string? display_type;
        private float? display_size;
        private string? display_resolution;
        private string? features_sensors;
        private string? platform_os;
        
        private string[]? fields;

        public Cell(string[]? fields) {
            this.fields = fields;
            string? data;
            //moves through each column and saves that data
            for(int i = 0; i < 12; i++) {
                // Console.WriteLine(i);
                //if it contains an empty string or '-' dat field set to null
                if (string.IsNullOrEmpty(fields[i]) || fields[i] == "-") {
                    data = null;
                } else {
                    //the current data being saved
                    data = fields[i];
                }

                //determines which data type it is
                checkType(i, data);
            }
        }

        //switch statments that determine which data goes wehre bases on postion in array
        //depending on data type it will go throgh regex algorithm before being saved
        private void checkType(int i, string data) {
            switch(i) {
                case 0 :  setOem(data);
                          break;
                case 1 :  setModel(data);
                          break;
                case 2 :  regexLaunchAnnounce(data);
                          break;
                case 3 :  regexLaunchStatus(data);
                          break;
                case 4 :  setBody_dimensions(data);
                          break;
                case 5 :  regexBodyWeight(data);
                          break;
                case 6 :  regexBodySim(data);
                          break;
                case 7 :  setDisplay_type(data);
                          break;
                case 8 :  regexDisplaySize(data);
                          break;
                case 9 :  setDisplay_resolution(data);
                          break;
                case 10 : setFeatures_sensors(data);
                          break;
                case 11 : regexPlatformOS(data);
                          break;
            }

        }
        
        //*FOR ALL REGEX METHODS*
        //filters out the wanted data from given string
        //checks for null values or invalid inputs
        //if none splits the string into smaller parts
        //checks if the each part matches the pattern or required data
        //if it does, save it end the algorirhtm
        private void regexLaunchAnnounce(string data) {
            if (data == null) {
                setLaunch_announced(null);
            } else {
                string[] arr = data.Split(new char [] {'.', ',', ' '});
                string pattern = @"^[1-2]{1}[0-9]";
                foreach (string s in arr) {
                    if (Regex.IsMatch(s, pattern)) {
                        setLaunch_announced(Convert.ToInt32(s));
                        break;
                    } else if (s == "V1" || s == null) {
                        setLaunch_announced(null);
                    }
                }
            }
        }

        private void regexLaunchStatus(string data) {
            if (data == "Cancelled" || data == "Discontinued" || data == null) {
                setLaunch_status(data);
            } else { 
                string pattern = @"^[1-2]{1}";
                string[] arr = data.Split(new char [] {'.', ',', ' '});
                
                foreach (string s in arr) {
                    if (Regex.IsMatch(s, pattern)) {
                    setLaunch_status(s);
                    break;
                    }
                }
            }
        }

        private void regexBodyWeight(string data) {
            if (data == null) {
                setBody_weight(null);
            } else {
                string[] arr = data.Split(new char [] {',', ' '});
                string pattern = @"^[1-9]{1}";
                
                foreach(string s in arr) {
                    if (Regex.IsMatch(s, pattern)) {
                        setBody_weight(float.Parse(s));
                        break;
                    }
                }
            } 
        }

        private void regexBodySim(string data) {
            if (data == null || data == "Yes" || data == "No") {
                setBody_sim(null);
            } else {
                setBody_sim(data);
            }
        }

        private void regexDisplaySize(string data) {
            if (data == null) {
                setDisplay_size(null);
            } else {
                string[] arr = data.Split(' ');
                string pattern = @"^[1-9]{1}";
                foreach(string s in arr) {
                    if (Regex.IsMatch(s, pattern)) {
                        setDisplay_size(float.Parse(s));
                        break;
                    }
                }
            }
        }
        
        private void regexPlatformOS(string data) {
            if (data == null) {
                setPlatform_os(null);
            } else {
                string[] arr = data.Split(',');
                setPlatform_os(arr[0]);
            }
        }

        public void setOem(string? oem) {
            this.oem = oem;
        }

        public void setModel(string? model) {
            this.model = model;
        }

        public void setLaunch_announced(int? launch_announced) {
            this.launch_announced = launch_announced;
        }

        public void setLaunch_status(string? launch_status) {
            this.launch_status = launch_status;
        }

        public void setBody_dimensions(string? body_dimensions) {
            this.body_dimensions = body_dimensions;
        }

        public void setBody_weight(float? body_weight) {
            this.body_weight = body_weight;
        }

        public void setBody_sim(string? body_sim) {
            this.body_sim = body_sim;
        }

        public void setDisplay_type(string? display_type) {       
            this.display_type = display_type;
        }

        public void setDisplay_size(float? display_size) {
            this.display_size = display_size;
        }

        public void setDisplay_resolution(string? display_resolution) {
            this.display_resolution = display_resolution;
        }

        public void setFeatures_sensors(string? features_sensors) {
            this.features_sensors = features_sensors;
        }

        public void setPlatform_os(string? platform_os) {
            this.platform_os = platform_os;
        }

        public string? getOem() {
            return oem;
        }

        public string? getModel() {
            return model;
        }

        public int? getLaunch_announced() {
            return launch_announced;
        }

        public string? getLaunch_status() {
            return launch_status;
        }

        public string? getBody_dimensions() {
            return body_dimensions;
        }

        public float? getBody_weight() {
            return body_weight;
        }

        public string? getBody_sim() {
            return body_sim;
        }

        public string? getDisplay_type() {
            return display_type;
        }

        public float? getDisplay_size() {
            return display_size;
        }

        public string? getDisplay_resolution() {
            return display_resolution;
        }

        public string? getFeatures_sensors() {
            return features_sensors;
        }

        public string? getPlatform_os() {
            return platform_os;
        }
    }
}