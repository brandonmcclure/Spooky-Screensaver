using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyScreensaver_wpf
{
    public abstract class Sprite : ISprite
    {        
        public int _FrameWidth { get; private set; }
        public int _FrameHeight { get; private set; }
        public int _randomDurationStart { get; private set; }
        public int _randomDurationEnd { get; private set; }
        public string SpriteSheetName { get; private set; }
        public int _randomStartY { get; private set; }
        public int _randomStartX { get; private set; }

        protected Sprite(int frameWidth, int frameHeight, int randomDurationStart, int randomDurationEnd, string spriteSheetName, int randomStartY, int randomStartX)
        {
            _FrameWidth = frameWidth;
            _FrameHeight = frameHeight;
            _randomDurationEnd = randomDurationEnd;
            _randomDurationStart = randomDurationStart;
            spriteSheetName = spriteSheetName ?? throw new ArgumentNullException(nameof(spriteSheetName));
            _randomStartY = randomStartY;
            _randomStartX = randomStartX;

        }

        public System.Windows.Shapes.Path DoThing(RectangleGeometry RectangleGeometry, Grid grid, string spriteName, FrameworkElement frameworkElement)
        {

            var randomDuration = RandomNumber(1000, 4000);
            // Assign the geometry a name so that
            // it can be targeted by a Storyboard.
            frameworkElement.RegisterName(
                spriteName, RectangleGeometry);

            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path
            {
                Fill = System.Windows.Media.Brushes.AliceBlue
            };
            var fullBitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/SnowSprite.png"));
            Int32Rect croppRect = new Int32Rect(0, 0, _FrameWidth, _FrameHeight);
            var croppedBitmap = new CroppedBitmap(fullBitmap, croppRect);

            var mybrush = new ImageBrush(croppedBitmap)
            {
                Transform = new TranslateTransform(0, 0),
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Stretch = Stretch.Fill
            };
            myPath.Fill = mybrush;
            myPath.StrokeThickness = 1;
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.Data = RectangleGeometry;

            RectAnimation myRectAnimation = new RectAnimation
            {
                BeginTime = TimeSpan.FromSeconds(RandomNumber()),
                Duration = TimeSpan.FromMilliseconds(randomDuration),
                FillBehavior = FillBehavior.HoldEnd,
                // Set the animation to repeat forever. 
                RepeatBehavior = RepeatBehavior.Forever,

                // Set the From and To properties of the animation.
                From = new Rect(_randomStartY, _randomStartX, _FrameWidth, _FrameHeight),
                To = new Rect(_randomStartY, frameworkElement.ActualHeight, _FrameWidth, _FrameHeight)
            };

            // Set the animation to target the Rect property
            // of the object named "MyAnimatedRectangleGeometry."
            Storyboard.SetTargetName(myRectAnimation, spriteName);
            Storyboard.SetTargetProperty(
                myRectAnimation, new PropertyPath(RectangleGeometry.RectProperty));

            //Control Rotation speed
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            RotateTransform myRotateTransform = new RotateTransform
            {
                CenterX = RectangleGeometry.Rect.Width / 2,
                CenterY = RectangleGeometry.Rect.Height / 2
            };

            Storyboard.SetTargetName(da, spriteName);
            Storyboard.SetTargetProperty(
                da, new PropertyPath(RectangleGeometry.TransformProperty));

            Storyboard ellipseStoryboard = new Storyboard();

            ellipseStoryboard.Children.Add(myRectAnimation);

            myPath.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                ellipseStoryboard.Begin(frameworkElement);
            };

            return myPath;
        }

        private void BeginStoryBoard(object sender, EventArgs e)
        {

        }

        private void RectAnimation_Completed(object sender, EventArgs e)
        {
            AnimationCompleteEventArgs ea = new AnimationCompleteEventArgs("testing");
            AnimationComplete(sender, ea);
        }

        public delegate void AnimationCompleteEventHandler(
       object sender,
       AnimationCompleteEventArgs args);

        public event AnimationCompleteEventHandler AnimationComplete;

        //Function to get a random number 
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
        public static double RandomNumber()
        {
            lock (syncLock)
            { // synchronize
                return random.NextDouble();
            }
        }
    }

    public class AnimationCompleteEventArgs : EventArgs
    {
        private readonly string test;

        public AnimationCompleteEventArgs(string test)
        {
            this.test = test;
        }

        public string Test
        {
            get { return this.test; }
        }
    }


}
