/*
 * ScreenSaverForm.cs
 * By Frank McCown
 * Summer 2010
 * 
 * Feel free to modify this code.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Extensions.Configuration;

namespace SpiderScreensaver
{
    public partial class ScreenSaverForm : Form
    {
        #region Win32 API functions

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion


        private Point mouseLocation;
        private bool previewMode = false;
        private Random rand = new Random();
        IConfiguration _configuration;




        public ScreenSaverForm()
        {
            InitializeComponent();
            
        }

        public ScreenSaverForm(Rectangle Bounds, IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            
            System.Windows.Forms.PictureBox _pictureBox = pictureBox1;
            _pictureBox.Image = SpiderScreensaver.Properties.Resources.sprites;
            pictureBox1.Size = new Size(250, 250);
            pictureBox1.BackColor = Color.LimeGreen;
            pictureBox1.Visible = true;
            pictureBox1.Refresh();
            
            //this.TransparencyKey = Color.LimeGreen;
            this.Bounds = Bounds;
        }

        public ScreenSaverForm(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            var rsl = SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);


            previewMode = true;
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            LoadSettings();

            Cursor.Hide();
            TopMost = true;

            moveTimer.Interval = 100;
            if (_configuration["ActiveMode"] == "HalloweenMode") { 
                iMode myMode = new HalloweenMode(pictureBox1, this.Bounds, _configuration);
                moveTimer.Tick += new EventHandler(myMode.moveTimer_Tick);
            }
            else if(_configuration["ActiveMode"] == "WinterMode")
            {
                iMode myMode = new WinterMode(pictureBox1, this.Bounds, _configuration);
                moveTimer.Tick += new EventHandler(myMode.moveTimer_Tick);
            }
            else
            {
                throw new Exception("I do not know what mode to run in.");
            }
            
            moveTimer.Start();
        }

        private void LoadSettings()
        {

            
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                if (!mouseLocation.IsEmpty)
                {
                    // Terminate if mouse is moved a significant distance
                    if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                        Math.Abs(mouseLocation.Y - e.Y) > 5)
                        Application.Exit();
                }

                // Update current mouse location
                mouseLocation = e.Location;
            }
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }

    }
}
