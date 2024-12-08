using System;
using System.Windows.Forms;

namespace CourseProject_Stohnii_2CS
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new GameForm(loginForm.SelectedProfile));
            }
        }
    }
}
