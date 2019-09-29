using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    public abstract class ASprite : IDisposable
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
        public abstract void tick();

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
        // ~ASprite() {
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
    public class Sprite_wf : ASprite
    {
        private bool disposedValue = false; // To detect redundant calls
        public string movementType { get; set; } = "Crawl";

        public Sprite_wf(Image sourceTileset, Rectangle _playingBounds,int tilex, int tiley)
        {
            tilesheet = sourceTileset;

            width = tilex;
            height = tiley;
            locationX = 0;
            locationY = 0;
            getCurrentRectangleFromTileSheet();

            playingBounds = _playingBounds;


            direction = 1;
            ChangeDirection(direction);
            MovementSpeed = 30;

        }

        private void getCurrentRectangleFromTileSheet()
        {
            
            Bitmap bTileSheet = new Bitmap(tilesheet);

            // Clone a portion of the Bitmap object.
            System.Drawing.Imaging.PixelFormat format = tilesheet.PixelFormat;
            Bitmap tile = bTileSheet.Clone(new Rectangle(imageStartX, imageStartY, width, height), format);


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
                }


                disposedValue = true;
            }
        }

        public override void tick()
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
         }
    }
}
