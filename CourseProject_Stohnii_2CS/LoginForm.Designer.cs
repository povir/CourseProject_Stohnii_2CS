namespace CourseProject_Stohnii_2CS
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxProfiles;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.Button btnAddProfile;
        private System.Windows.Forms.Button btnDeleteProfile;
        private System.Windows.Forms.Button btnSelectProfile;

        private void InitializeComponent()
        {
            this.listBoxProfiles = new System.Windows.Forms.ListBox();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.btnAddProfile = new System.Windows.Forms.Button();
            this.btnDeleteProfile = new System.Windows.Forms.Button();
            this.btnSelectProfile = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // listBoxProfiles
            this.listBoxProfiles.FormattingEnabled = true;
            this.listBoxProfiles.Location = new System.Drawing.Point(12, 12);
            this.listBoxProfiles.Size = new System.Drawing.Size(260, 150);

            // txtProfileName
            this.txtProfileName.Location = new System.Drawing.Point(12, 170);
            this.txtProfileName.Size = new System.Drawing.Size(150, 20);

            // btnAddProfile
            this.btnAddProfile.Location = new System.Drawing.Point(170, 170);
            this.btnAddProfile.Size = new System.Drawing.Size(100, 23);
            this.btnAddProfile.Text = "Add Profile";
            this.btnAddProfile.Click += new System.EventHandler(this.btnAddProfile_Click);

            // btnDeleteProfile
            this.btnDeleteProfile.Location = new System.Drawing.Point(12, 200);
            this.btnDeleteProfile.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteProfile.Text = "Delete Profile";
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);

            // btnSelectProfile
            this.btnSelectProfile.Location = new System.Drawing.Point(170, 200);
            this.btnSelectProfile.Size = new System.Drawing.Size(100, 23);
            this.btnSelectProfile.Text = "Select Profile";
            this.btnSelectProfile.Click += new System.EventHandler(this.btnSelectProfile_Click);

            // LoginForm
            this.ClientSize = new System.Drawing.Size(284, 241);
            this.Controls.Add(this.listBoxProfiles);
            this.Controls.Add(this.txtProfileName);
            this.Controls.Add(this.btnAddProfile);
            this.Controls.Add(this.btnDeleteProfile);
            this.Controls.Add(this.btnSelectProfile);
            this.Text = "Login";
            this.ResumeLayout(false);
        }
    }
}
