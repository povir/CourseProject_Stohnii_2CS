using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CourseProject_Stohnii_2CS.Models;

namespace CourseProject_Stohnii_2CS
{
    public partial class LoginForm : Form
    {
        private List<Profile> profiles;

        public Profile SelectedProfile { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            profiles = DataStorage.LoadProfiles();
            UpdateProfileList();
        }

        private void UpdateProfileList()
        {
            listBoxProfiles.Items.Clear();
            foreach (var profile in profiles)
            {
                listBoxProfiles.Items.Add(profile.Name);
            }
        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            string name = txtProfileName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                profiles.Add(new Profile { Name = name });
                DataStorage.SaveProfiles(profiles);
                UpdateProfileList();
                txtProfileName.Clear();
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (listBoxProfiles.SelectedItem != null)
            {
                var selectedName = listBoxProfiles.SelectedItem.ToString();
                profiles.RemoveAll(p => p.Name == selectedName);
                DataStorage.SaveProfiles(profiles);
                UpdateProfileList();
            }
        }

        private void btnSelectProfile_Click(object sender, EventArgs e)
        {
            if (listBoxProfiles.SelectedItem != null)
            {
                var selectedName = listBoxProfiles.SelectedItem.ToString();
                SelectedProfile = profiles.Find(p => p.Name == selectedName);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
