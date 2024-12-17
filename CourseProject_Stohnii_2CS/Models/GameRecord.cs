namespace CourseProject_Stohnii_2CS.Models
{
    public class GameRecord
    {
        public string Opponent { get; set; }
        public string Result { get; set; } // "Win", "Lose", "Draw"
        public string Date { get; set; }
        public bool AgainstAI { get; set; } // True для ігор против ШІ
    }
}
