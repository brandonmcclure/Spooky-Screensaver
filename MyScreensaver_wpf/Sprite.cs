using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MyScreensaver_wpf
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
    class Sprite_wpf : iSprite
    {
        System.Windows.Shapes.Rectangle rec = new System.Windows.Shapes.Rectangle()
        {
            Width = width,
            Height = height,
            Fill = Brushes.Green,
            Stroke = Brushes.Red,
            StrokeThickness = 2,
        };

        
        Sprite_wpf()
        {
            // Add to a canvas for example
            Canvas.Children.Add(rec);
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(0, 0, 95, 95);

            Path myPath = new Path();
            myPath.Fill = System.Windows.Media.Brushes.AliceBlue;
            var fullBitmap = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Resources/SnowSprite.png"));
            Int32Rect croppRect = new Int32Rect(0, 0, 95, 95);
            var croppedBitmap = new System.Windows.Media.Imaging.CroppedBitmap(fullBitmap, croppRect);

            var mybrush = new ImageBrush(croppedBitmap);
            mybrush.Transform = new TranslateTransform(0, 0);
            mybrush.AlignmentX = AlignmentX.Left;
            mybrush.AlignmentY = AlignmentY.Top;
            mybrush.Stretch = Stretch.Fill;
            myPath.Fill = mybrush;
            myPath.StrokeThickness = 1;
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.Data = myRectangleGeometry;

            var transformGroup = new TransformGroup();
            RectAnimation myRectAnimation = new RectAnimation();
            myRectAnimation.Duration = TimeSpan.FromSeconds(2);
            myRectAnimation.FillBehavior = FillBehavior.HoldEnd;

            // Set the animation to repeat forever. 
            myRectAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Set the From and To properties of the animation.
            myRectAnimation.From = new Rect(0, 0, 100, 100);
            myRectAnimation.To = new Rect(0, this.MainGrid.ActualHeight, 200, 50);
            TranslateTransform myTranslateTransform = new TranslateTransform();
            Snowflake01.RenderTransform = myTranslateTransform;
            //myTranslateTransform.BeginAnimation(TranslateTransform.YProperty, myRectAnimation);

            //Control Rotation speed
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
            da.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform myRotateTransform = new RotateTransform();
            myRotateTransform.CenterX = Snowflake01.Width / 2;
            myRotateTransform.CenterY = Snowflake01.Height / 2;

            transformGroup.Children.Add(myRotateTransform);

            Snowflake01.RenderTransform = transformGroup;
            myRotateTransform.BeginAnimation(RotateTransform.AngleProperty, da);
        }
        public override void AnimateSprite()
        {
            throw new NotImplementedException();
        }

        public override void ChangeDirection(int newDirection)
        {
            throw new NotImplementedException();
        }
    }
    class Sprite_wf : iSprite
    {
        private bool disposedValue = false; // To detect redundant calls
        public System.Windows.Forms.PictureBox _pictureBox;
        public string movementType { get; set; } = "Crawl";

        public Sprite_wf(System.Windows.Forms.PictureBox pictureBox, Image sourceTileset, Rectangle _playingBounds,int tilex, int tiley)
        {
            tilesheet = sourceTileset;
            _pictureBox = pictureBox;

            width = tilex;
            height = tiley;
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
            else if (movementType == "Fall")
            {
                _pictureBox.Top += MovementSpeed;
                if (playingBounds.Bottom <= _pictureBox.Top)
                {
                    _pictureBox.Top = playingBounds.Top - _pictureBox.Height;
                }
            }
        }
    }
}
