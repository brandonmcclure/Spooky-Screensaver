﻿/*
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
        private List<Sprite_wf> Sprite_wfCollection = new List<Sprite_wf>();
        private string movementType;

        public ScreenSaverForm()
        {
            InitializeComponent();
            
        }

        public ScreenSaverForm(Rectangle Bounds)
        {
            InitializeComponent();
            this.BackColor = Color.LimeGreen;
            
            System.Windows.Forms.PictureBox _pictureBox = pictureBox1;
            _pictureBox.Image = SpiderScreensaver.Properties.Resources.sprites;
            pictureBox1.Size = new Size(250, 250);
            pictureBox1.BackColor = Color.LimeGreen;
            pictureBox1.Visible = true;
            pictureBox1.Refresh();
            Sprite_wf mySprite_wf = new Sprite_wf(pictureBox1, SpiderScreensaver.Properties.Resources.sprites,Bounds);
            Sprite_wfCollection.Add(mySprite_wf);
            //this.TransparencyKey = Color.LimeGreen;
            this.Bounds = Bounds;
        }

        public ScreenSaverForm(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

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
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
            moveTimer.Start();
        }

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {
            foreach (Sprite_wf spr in Sprite_wfCollection)
            {
                if (rand.Next(1,100) < 20)
                {
                    spr.ChangeDirection(rand.Next(0, 3));
                }
                if (movementType == "Random") { 
                    spr._pictureBox.Left = rand.Next(Math.Max(1, Bounds.Width - spr._pictureBox.Width));
                    spr._pictureBox.Top = rand.Next(Math.Max(1, Bounds.Height - spr._pictureBox.Height));
                }

                else if (movementType == "Crawl")
                {
                    spr.MoveSprite();
                }
            }
        }

        private void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\SpiderScreensaver");
            if (key == null)
                movementType = "Crawl";
            else
                movementType = (string)key.GetValue("movementType");
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
