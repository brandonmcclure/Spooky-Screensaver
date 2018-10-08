using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderScreensaver
{
    abstract class iSprite
    {
        public int width;
        public int height;
        public int imageStartX;
        public int imageStartY;

        public int locationX;
        public int locationY;

        public int direction;
        public int MovementSpeed;
        public Random rand = new Random();
        public Image tilesheet;

        public abstract void AnimateSprite();
        public abstract void ChangeDirection(int newDirection);
    }
    class Sprite_wf : iSprite
    {

        public System.Windows.Forms.PictureBox _pictureBox;
        public Sprite_wf(System.Windows.Forms.PictureBox pictureBox, Image sourceTileset)
        {
            tilesheet = sourceTileset;
            _pictureBox = pictureBox;

            width = 250;
            height = 250;
            locationX = 0;
            locationY = 0;
            getCurrentRectangleFromTileSheet();


            direction = 1;
            ChangeDirection(direction);
            MovementSpeed = 30;
            _pictureBox.Size = new Size(width, height);

        }

        private void getCurrentRectangleFromTileSheet()
        {
            
            Bitmap bTileSheet = new Bitmap(tilesheet);

            // Clone a portion of the Bitmap object.
            System.Drawing.Imaging.PixelFormat format = tilesheet.PixelFormat;
            Bitmap tile = bTileSheet.Clone(new Rectangle(imageStartX, imageStartY, width, height), format);


            _pictureBox.Image =tile;
            

        }

        public override void AnimateSprite()
        {
            
        }

        public override void ChangeDirection( int newDirection)
        {
            if (newDirection == 0 || newDirection == 1 || newDirection == 2|| newDirection == 3)
            {
                direction = newDirection;
                imageStartY = height * newDirection;
                getCurrentRectangleFromTileSheet();
                int x = 0;
            }
        }
    }
}
