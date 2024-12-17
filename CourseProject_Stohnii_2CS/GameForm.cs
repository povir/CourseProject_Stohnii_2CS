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
        private bool isAiMode = false;

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
            // Переключатель режимів
            Button btnToggleMode = new Button
            {
                Text = "Режим: Людина",
                Location = new Point(300, 350),
                Width = 150,
                Height = 30
            };
            btnToggleMode.Click += (sender, e) =>
            {
                isAiMode = !isAiMode;
                btnToggleMode.Text = isAiMode ? "Режим: ИИ" : "Режим: Людина";
            };
            Controls.Add(btnToggleMode);

            // Кнопка - исторія
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

            // Кнопка зміни профіля
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

            // Кнопка Exit
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
                HandleGameEnd(isPlayerXTurn ? "X" : "O");
                return;
            }

            if (board.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)))
            {
                HandleGameEnd("Нічія");
                return;
            }

            isPlayerXTurn = !isPlayerXTurn;

            if (isAiMode && !isPlayerXTurn)
            {
                MakeAiMove();

                if (CheckWin())
                {
                    HandleGameEnd("O");
                    return;
                }

                if (board.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)))
                {
                    HandleGameEnd("Нічія");
                    return;
                }

                isPlayerXTurn = true;
            }
        }

        private void MakeAiMove()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(board[i, j].Text))
                    {
                        board[i, j].Text = "O";
                        return;
                    }
                }
            }
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

        private void HandleGameEnd(string result)
        {
            string message = result == "Draw" ? "It's a draw!" : $"Player {result} wins!";
            MessageBox.Show(message);

            currentProfile.GameHistory.Add(new GameRecord
            {
                Opponent = isAiMode ? "AI" : "Human",
                Result = result == "Draw" ? "Draw" : $"{result} wins",
                Date = DateTime.Now.ToString("G"),
                AgainstAI = isAiMode
            });

            var profiles = DataStorage.LoadProfiles();
            var profile = profiles.FirstOrDefault(p => p.Name == currentProfile.Name);
            if (profile != null)
            {
                profile.GameHistory = currentProfile.GameHistory;
            }
            else
            {
                profiles.Add(currentProfile);
            }
            DataStorage.SaveProfiles(profiles);

            ResetBoard();
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
