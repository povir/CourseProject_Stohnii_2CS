using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CourseProject_Stohnii_2CS.Models;

namespace CourseProject_Stohnii_2CS
{
    public partial class GameForm : Form
    {
        private Profile currentProfile;
        private Button[,] board = new Button[3, 3];
        private bool isPlayerXTurn = true;

        public GameForm(Profile profile)
        {
            InitializeComponent();
            Text = "Гра Хрестики-нулики";
            currentProfile = profile;
            InitializeBoard();
            InitializeControls();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = new Button
                    {
                        Font = new Font("Arial", 24),
                        Width = 100,
                        Height = 100,
                        Location = new Point(j * 100 + 150, i * 100 + 50) 
                    };
                    board[i, j].Click += OnCellClick;
                    Controls.Add(board[i, j]);
                }
            }
        }

        private void InitializeControls()
        {
            
            Button btnHistory = new Button
            {
                Text = "Історія ігор",
                Location = new Point(150, 400),
                Width = 100,
                Height = 30
            };
            btnHistory.Click += (sender, e) =>
            {
                using (HistoryForm historyForm = new HistoryForm(currentProfile))
                {
                    historyForm.ShowDialog();
                }
            };
            Controls.Add(btnHistory);

            
            Button btnChangeProfile = new Button
            {
                Text = "Змінити профіль",
                Location = new Point(300, 400),
                Width = 100,
                Height = 30
            };
            btnChangeProfile.Click += (sender, e) =>
            {
                Hide(); 
                using (LoginForm loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        
                        Profile selectedProfile = loginForm.SelectedProfile;
                        GameForm newGameForm = new GameForm(selectedProfile);
                        newGameForm.ShowDialog();
                    }
                }
                Close(); 
            };
            Controls.Add(btnChangeProfile);


            
            Button btnExit = new Button
            {
                Text = "Вийти",
                Location = new Point(450, 400),
                Width = 100,
                Height = 30
            };
            btnExit.Click += (sender, e) =>
            {
                Application.Exit(); 
            };
            Controls.Add(btnExit);
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (!string.IsNullOrEmpty(button.Text)) return;

            button.Text = isPlayerXTurn ? "X" : "O";
            if (CheckWin())
            {
                string winner = isPlayerXTurn ? "X" : "O";
                MessageBox.Show($"Player {winner} wins!");

                currentProfile.GameHistory.Add(new GameRecord
                {
                    Opponent = "AI",
                    Result = $"{winner} wins",
                    Date = DateTime.Now.ToString("G")
                });

                DataStorage.SaveProfiles(DataStorage.LoadProfiles());
                ResetBoard();
            }
            else if (board.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)))
            {
                MessageBox.Show("It's a draw!");

                currentProfile.GameHistory.Add(new GameRecord
                {
                    Opponent = "AI",
                    Result = "Draw",
                    Date = DateTime.Now.ToString("G")
                });

                DataStorage.SaveProfiles(DataStorage.LoadProfiles());
                ResetBoard();
            }

            isPlayerXTurn = !isPlayerXTurn;
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0].Text == board[i, 1].Text && board[i, 1].Text == board[i, 2].Text && !string.IsNullOrEmpty(board[i, 0].Text))
                    return true;

                if (board[0, i].Text == board[1, i].Text && board[1, i].Text == board[2, i].Text && !string.IsNullOrEmpty(board[0, i].Text))
                    return true;
            }

            if (board[0, 0].Text == board[1, 1].Text && board[1, 1].Text == board[2, 2].Text && !string.IsNullOrEmpty(board[0, 0].Text))
                return true;

            if (board[0, 2].Text == board[1, 1].Text && board[1, 1].Text == board[2, 0].Text && !string.IsNullOrEmpty(board[0, 2].Text))
                return true;

            return false;
        }

        private void ResetBoard()
        {
            foreach (var button in board)
            {
                button.Text = string.Empty;
            }
            isPlayerXTurn = true;
        }
    }
}
