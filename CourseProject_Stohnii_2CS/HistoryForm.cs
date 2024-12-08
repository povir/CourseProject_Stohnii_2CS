using System;
using System.Windows.Forms;
using CourseProject_Stohnii_2CS.Models;

namespace CourseProject_Stohnii_2CS
{
    public partial class HistoryForm : Form
    {
        public HistoryForm(Profile profile)
        {
            
            InitializeHistory(profile);
        }

        private void InitializeHistory(Profile profile)
        {
            
            Text = $"Історія ігор: {profile.Name}";
            Width = 400;
            Height = 300;

            
            var listBox = new ListBox
            {
                Dock = DockStyle.Fill 
            };

            foreach (var record in profile.GameHistory)
            {
                listBox.Items.Add($"{record.Date} - {record.Opponent}: {record.Result}");
            }

            Controls.Add(listBox); 
        }
    }
}
