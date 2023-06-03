using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Alternative_Language_Project {

    public class ProjectRunner {
        static void Main(string[] args) {
            string file = Path.Combine(Directory.GetFiles("InputFile"));
            
            Database phoneDatabase = new Database(file);
            phoneDatabase.createDatabase();
          
            string[] newPhone = {"Google", "Pixel", "2019", "Cancelled", "156.1 x 73.9 x 8.3 mm (6.15 x 2.91 x 0.33 in)", 
                        "188 g (6.63 oz)", "Dual SIM (dual stand-by)", "TFT resistive touchscreen, 256K colors", 
                        "5.6 inches, 79.6 cm (~75.0% screen-to-body ratio)", "1080 x 2220 pixels, 18.5:9 ratio (~441 ppi density)",
                        "Fingerprint (side-mounted), accelerometer, gyro, proximity, compass", "Android 9.0 (Pie), upgradable to Android 10"};

            phoneDatabase.mostCommonSensors();
            phoneDatabase.mostCommonOem();
            phoneDatabase.mostCommonOS();
            Console.WriteLine(phoneDatabase.averageWeight());

            phoneDatabase.addPhone(newPhone);
            Console.WriteLine(phoneDatabase.getPhone(1000));

            phoneDatabase.diffReleasedYear();
            phoneDatabase.phoneOneSensor();
            phoneDatabase.mostLaunchYear();
            Console.ReadKey();
        }
    }
}