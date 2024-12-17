using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CourseProject_Stohnii_2CS.Models
{
    public static class DataStorage
    {
        private static string FilePath = "Data/profiles.json";

        public static List<Profile> LoadProfiles()
        {
            if (!File.Exists(FilePath))
                return new List<Profile>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Profile>>(json);
        }

        public static void SaveProfiles(List<Profile> profiles)
        {
            string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
            Directory.CreateDirectory("Data");
            File.WriteAllText(FilePath, json);
        }
    }
}
