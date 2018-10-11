using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderScreensaver
{
    abstract class iSprite : IDisposable
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~iSprite() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    class Sprite_wf : iSprite
    {
        private bool disposedValue = false; // To detect redundant calls
        public System.Windows.Forms.PictureBox _pictureBox;
        public string movementType { get; protected set; } = "Crawl";

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

            bTileSheet.Dispose();

        }

        public override void AnimateSprite()
        {
            
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        internal void tick()
        {
            
                MoveSprite();
            
        }

        public override void ChangeDirection( int newDirection)
        {
            if (newDirection == 0 || newDirection == 1 || newDirection == 2|| newDirection == 3)
            {
                direction = newDirection;
                imageStartY = height * newDirection;
                getCurrentRectangleFromTileSheet();
            }
        }

        internal void MoveSprite()
        {
            if (rand.Next(1, 100) < 20)
            {
                ChangeDirection(rand.Next(0, 3));
            }
            if (movementType == "Random")
            {
                _pictureBox.Left = rand.Next(Math.Max(1, playingBounds.Width - _pictureBox.Width));
                _pictureBox.Top = rand.Next(Math.Max(1, playingBounds.Height - _pictureBox.Height));
            }

            else if (movementType == "Crawl")
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
}
