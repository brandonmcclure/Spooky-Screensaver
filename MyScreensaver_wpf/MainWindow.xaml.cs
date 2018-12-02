using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyScreensaver_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConfiguration _configuration;
        private const int NumberOfColumns = 1;
        private const int NumberOfFrames = 1;
        private const int FrameWidth = 95;
        private const int FrameHeight = 95;
        public static readonly TimeSpan TimePerFrame = TimeSpan.FromSeconds(1 / 60f);
        private int currentFrame;
        private TimeSpan timeTillNextFrame;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        public MainWindow(IConfiguration configuration) : this ()
        {
            _configuration = configuration;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnUpdate);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            WindowState = WindowState.Maximized;
            DebugText.Visibility = System.Windows.Visibility.Hidden;
            if (_configuration["Debug"] == "1")
            {
                DebugText.Visibility = System.Windows.Visibility.Visible;
            }
            DebugText.Content = "currentFrame: " + this.currentFrame;
            DebugText.Content += "\nSpriteSheetOffset.X: " + this.SpriteSheetOffset.X;
            DebugText.Content += "\nSpriteSheetOffset.Y: " + this.SpriteSheetOffset.Y;

            if (_configuration["ActiveMode"] == "HalloweenMode")
            {
                //iMode myMode = new HalloweenMode(this.MainGrid. this.Bounds, _configuration);
                //dispatcherTimer.Tick += new EventHandler(myMode.moveTimer_Tick);
            }
            else if (_configuration["ActiveMode"] == "WinterMode")
            {
                //iMode myMode = new WinterMode(pictureBox1, this.Bounds, _configuration);
                //dispatcherTimer.Tick += new EventHandler(myMode.moveTimer_Tick);
            }
            else
            {
                throw new Exception("I do not know what mode to run in.");
            }

            RectAnimationExample();
            rotateAnimationExample();
            WindowState = WindowState.Maximized;
            // Mouse.OverrideCursor = Cursors.None;
        }

        private void rotateAnimationExample()
        {
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

        private void OnUpdate(object sender, object e)
        {
            
            this.timeTillNextFrame += TimeSpan.FromSeconds(1 / 60f);
            if (this.timeTillNextFrame > TimePerFrame)
            {
                this.currentFrame = (this.currentFrame + 1 + NumberOfFrames) % NumberOfFrames;
                var column = this.currentFrame % NumberOfColumns;
                var row = this.currentFrame / NumberOfColumns;

                this.SpriteSheetOffset.X = this.currentFrame += 1;//column * FrameWidth;
                this.SpriteSheetOffset.Y = row * FrameHeight;

                DebugText.Content = "currentFrame: " + this.currentFrame;
                DebugText.Content += "\nrow: " + row;
                DebugText.Content += "\ncolumn: " + column;
                DebugText.Content += "\nSpriteSheetOffset.X: " + this.SpriteSheetOffset.X;
                DebugText.Content += "\nSpriteSheetOffset.Y: " + this.SpriteSheetOffset.Y;
            }
            
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void RectAnimationExample()
        {

            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(0, 0, 95, 95);

            // Assign the geometry a name so that
            // it can be targeted by a Storyboard.
            this.RegisterName(
                "MyAnimatedRectangleGeometry", myRectangleGeometry);

            Path myPath = new Path();
            myPath.Fill = Brushes.AliceBlue;
            var fullBitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/SnowSprite.png"));
            Int32Rect croppRect = new Int32Rect(0, 0, 95, 95);
            var croppedBitmap = new CroppedBitmap(fullBitmap, croppRect);

            var mybrush = new ImageBrush(croppedBitmap);
            mybrush.Transform = new TranslateTransform(0, 0);
            mybrush.AlignmentX = AlignmentX.Left;
            mybrush.AlignmentY = AlignmentY.Top;
            mybrush.Stretch = Stretch.Fill;
            myPath.Fill = mybrush;
            myPath.StrokeThickness = 1;
            myPath.Stroke = Brushes.Black;
            myPath.Data = myRectangleGeometry;

            RectAnimation myRectAnimation = new RectAnimation();
            myRectAnimation.Duration = TimeSpan.FromSeconds(2);
            myRectAnimation.FillBehavior = FillBehavior.HoldEnd;
            // Set the animation to repeat forever. 
            myRectAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Set the From and To properties of the animation.
            myRectAnimation.From = new Rect(0,0, 95, 95);
            myRectAnimation.To = new Rect(0, this.MainGrid.ActualHeight, 95, 95);

            // Set the animation to target the Rect property
            // of the object named "MyAnimatedRectangleGeometry."
            Storyboard.SetTargetName(myRectAnimation, "MyAnimatedRectangleGeometry");
            Storyboard.SetTargetProperty(
                myRectAnimation, new PropertyPath(RectangleGeometry.RectProperty));

            //Control Rotation speed
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
            da.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform myRotateTransform = new RotateTransform();
            myRotateTransform.CenterX = Snowflake01.Width / 2;
            myRotateTransform.CenterY = Snowflake01.Height / 2;

            Storyboard.SetTargetName(da, "MyAnimatedRectangleGeometry");
            Storyboard.SetTargetProperty(
                da, new PropertyPath(RectangleGeometry.TransformProperty));
            // Create a storyboard to apply the animation.
            Storyboard ellipseStoryboard = new Storyboard();
            ellipseStoryboard.Children.Add(myRectAnimation);
            //ellipseStoryboard.Children.Add(da);

            // Start the storyboard when the Path loads.
            myPath.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                ellipseStoryboard.Begin(this);
            };

            Canvas containerCanvas = new Canvas();
            containerCanvas.Children.Add(myPath);

            Content = containerCanvas;
        }
    }
}
