/*
 * SettingsForm.cs
 * By Frank McCown
 * Summer 2010
 * 
 * Feel free to modify this code.
 */

using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SpiderScreensaver
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// Load display text from the Registry
        /// </summary>
        private void LoadSettings()
        {
            
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SpiderScreensaver");
            if (key == null)
                textBox.Text = "Random";
            else
                textBox.Text = (string)key.GetValue("movementType");
        }

        /// <summary>
        /// Save text into the Registry.
        /// </summary>
        private void SaveSettings()
        {
            // Create or get existing subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SpiderScreensaver");

            key.SetValue("movementType", textBox.Text);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
