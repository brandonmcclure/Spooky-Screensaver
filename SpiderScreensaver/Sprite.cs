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
        public Rectangle playingBounds;

        public abstract void AnimateSprite();
        public abstract void ChangeDirection(int newDirection);
    }
    class Sprite_wf : iSprite
    {

        public System.Windows.Forms.PictureBox _pictureBox;
        public Sprite_wf(System.Windows.Forms.PictureBox pictureBox, Image sourceTileset, Rectangle _playingBounds)
        {
            tilesheet = sourceTileset;
            _pictureBox = pictureBox;

            width = 250;
            height = 250;
            locationX = 0;
            locationY = 0;
            getCurrentRectangleFromTileSheet();

            playingBounds = _playingBounds;


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

        internal void MoveSprite()
        {
            //Move down
            if (direction == 0)
            {
                _pictureBox.Top += MovementSpeed;
                if (playingBounds.Bottom <= _pictureBox.Top)
                {
                    _pictureBox.Top = playingBounds.Top - _pictureBox.Height;
                }
            }
            //Move right
            else if (direction == 1)
            {
                _pictureBox.Left += MovementSpeed;
                if (playingBounds.Right < _pictureBox.Left)
                {
                    _pictureBox.Left = playingBounds.Left - _pictureBox.Width;
                }
            }
            //Move up
            else if (direction == 2)
            {
                _pictureBox.Top -= MovementSpeed;
                if (playingBounds.Top > _pictureBox.Bottom)
                {
                    _pictureBox.Top = playingBounds.Bottom;
                }
            }
            //Move Left
            else if (direction == 3)
            {
                _pictureBox.Left -= MovementSpeed;
                if (playingBounds.Left > _pictureBox.Right)
                {
                    _pictureBox.Left = playingBounds.Right;
                }
            }
        }
    }
}
