namespace Alternative_Language_Project {
    class cell {
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

        public cell(){

        }
        
        public void setOem(string oem) {
            this.oem = oem;
        }

        public void setModel(string model) {
            this.model = model;
        }

        public void setLaunch_announced(int launch_announced) {
            this.launch_announced = launch_announced;
        }

        public void setLaunch_status(string launch_status) {
            this.launch_status = launch_status;
        }

        public void setBody_dimensions(string body_dimensions) {
            this.body_dimensions = body_dimensions;
        }

        public void setBody_weight(float body_weight) {
            this.body_weight = body_weight;
        }

        public void setBody_sim(string body_sim) {
            this.body_sim = body_sim;
        }

        public void setDisplay_type(string display_type) {
            this.display_type = display_type;
        }

        public void setDisplay_size(float display_size) {
            this.display_size = display_size;
        }

        public void setDisplay_resolution(string display_resolution) {
            this.display_resolution = display_resolution;
        }

        public void setFeatures_sensors(string features_sensors) {
            this.features_sensors = features_sensors;
        }

        public void setPlatform_os(string platform_os) {
            this.platform_os = platform_os;
        }

        public string getOem(string oem) {
            return oem;
        }

        public string getModel(string model) {
            return model;
        }

        public int getLaunch_announced(int launch_announced) {
            return launch_announced;
        }

        public string getLaunch_status(string launch_status) {
            return launch_status;
        }

        public string getBody_dimensions(string body_dimensions) {
            return body_dimensions;
        }

        public float getBody_weight(float body_weight) {
            return body_weight;
        }

        public string getBody_sim(string body_sim) {
            return body_sim;
        }

        public string getDisplay_type(string display_type) {
            return display_type;
        }

        public float getDisplay_size(float display_size) {
            return display_size;
        }

        public string getDisplay_resolution(string display_resolution) {
            return display_resolution;
        }

        public string getFeatures_sensors(string features_sensors) {
            return features_sensors;
        }

        public string getPlatform_os(string platform_os) {
            return platform_os;
        }
    }
}