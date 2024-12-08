using System.Collections.Generic;

namespace CourseProject_Stohnii_2CS.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public List<GameRecord> GameHistory { get; set; } = new List<GameRecord>();
    }
}
