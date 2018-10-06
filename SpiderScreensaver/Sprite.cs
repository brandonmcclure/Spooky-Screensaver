using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderScreensaver
{
    class Sprite
    {
        int width;
        int height;

        int locationX;
        int locationY;

        int direction;
        private Random rand = new Random();

        public System.Windows.Forms.PictureBox _pictureBox;
        public Sprite(System.Windows.Forms.PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            width = 250;
            height = 250;
            locationX = 0;
            locationY = 0;
            direction = 0;
        }
    }
}
