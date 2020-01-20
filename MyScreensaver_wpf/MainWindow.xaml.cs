using Microsoft.Extensions.Configuration;
using MyScreensaver_wpf.Modes.Haloween;
using MyScreensaver_wpf.Modes.Winter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        
        public static readonly TimeSpan TimePerFrame = TimeSpan.FromSeconds(1 / 60f);
        private int currentFrame;
        private TimeSpan timeTillNextFrame;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private System.Drawing.Rectangle windowBounds;
        Random Randomer = new Random();
        Canvas containerCanvas = new Canvas();
        List<string> AnimatingSprites = new List<string>();

        public int MaxConcurrentSpritesAnimating { get; private set; } = 2;

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
            NameScope.SetNameScope(this, new NameScope());

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnUpdate);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            WindowState = WindowState.Maximized;

            windowBounds = new System.Drawing.Rectangle() { X = 0, Y = 0, Height = (int)this.MainGrid.ActualHeight, Width = (int)this.MainGrid.ActualWidth };
            if (windowBounds.Height == 0 || windowBounds.Width == 0)
            {
                throw new IncorrectConfigurationException("Could not get the window bounds");
            }

           
            DebugText.Visibility = System.Windows.Visibility.Hidden;
            if (_configuration["Debug"] == "1")
            {
                DebugText.Visibility = System.Windows.Visibility.Visible;
            }
            UpdateDebugText();

            if (_configuration["ActiveMode"] == "HalloweenMode")
            {
                SolidColorBrush backgroundBrush = new SolidColorBrush(System.Windows.Media.Colors.LightGray);
                this.Background = backgroundBrush;
            }
            else if (_configuration["ActiveMode"] == "WinterMode")
            {
                int minSprites;
                int maxSprites;
                if (!int.TryParse(_configuration["WinterMode:minNumberOfSprites"], out minSprites))
                {
                    Trace.TraceError("Could not read the minNumberOfSprites from the Configuration file, defaulting to 1");
                    minSprites = 1;
                }
                if (!int.TryParse(_configuration["WinterMode:maxNumberOfSprites"], out maxSprites))
                {
                    Trace.TraceError("Could not read the maxNumberOfSprites from the Configuration file, defaulting to 1");
                    maxSprites = 1;
                }
                
                SnowFallingAnimation(Randomer.Next(minSprites, maxSprites));
            }


            WindowState = WindowState.Maximized;
#if DEBUG
                Mouse.OverrideCursor = Cursors.None;
#endif
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
                var column = CalculateColumn();
                var row = CalculateRow();
                this.SpriteSheetOffset.X = this.currentFrame += 1;

                UpdateDebugText();
            }
            if (AnimatingSprites.Count() < MaxConcurrentSpritesAnimating)
            {
                if (_configuration["ActiveMode"] == "HalloweenMode")
                {
                    HalloweenCrawlies();
                }
            }
            
        }
        private Int32 CalculateColumn()
        {
            return this.currentFrame % NumberOfColumns;
        }
        private Int32 CalculateRow()
        {
            return this.currentFrame / NumberOfColumns;
        }
        private void UpdateDebugText()
        {
            DebugText.Content = "ActiveMode: " + _configuration["ActiveMode"];
            DebugText.Content += "\ncurrentFrame: " + this.currentFrame;
            DebugText.Content += "\nrow: " + CalculateRow();
            DebugText.Content += "\ncolumn: " + CalculateColumn();
            DebugText.Content += "\nSpriteSheetOffset.X: " + this.SpriteSheetOffset.X;
            DebugText.Content += "\nSpriteSheetOffset.Y: " + this.SpriteSheetOffset.Y;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void SnowFallingAnimation(int numberOfSnowflakes)
        {
            int FrameWidth = 95;
            int FrameHeight = 95;
            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());
            Canvas containerCanvas = new Canvas();
            List<string> sprites = new List<string>();
            for(int i = 1; i <= numberOfSnowflakes; i++)
            {
                string spriteName = "SnowSprite" + i.ToString("0000");
                AnimatingSprites.Add(spriteName);

                RectangleGeometry myRectangleGeometry = new RectangleGeometry();

                var randomStartY = Randomer.Next(0, (int)this.MainGrid.ActualWidth - FrameWidth);
                int randomStartX = -Randomer.Next(FrameHeight, FrameHeight * 4);
                var randomDuration = Randomer.Next(1000, 4000);

                myRectangleGeometry.Rect = new Rect(randomStartY, randomStartX, FrameWidth, FrameHeight);
                // Assign the geometry a name so that
                // it can be targeted by a Storyboard.
                this.RegisterName(
                    spriteName, myRectangleGeometry);

                Sprite sprite = new SnowSprite(FrameWidth,FrameHeight, 1000,4000, "SnowSprite.png",randomStartY,randomStartX);
                var p = sprite.doThing(myRectangleGeometry, this.MainGrid, spriteName, this);
                sprite.AnimationComplete += HalloweenSpiderRectAnimation_Completed;


                // Start the storyboard when the Path loads.



                containerCanvas.Children.Add(p);

                Content = containerCanvas;

            }
        }

        public void HalloweenCrawlies()
        {
            int FrameWidth = 250;
            int FrameHeight = 245;
            // Create a NameScope for this page so that
            // Storyboards can be used.
            NameScope.SetNameScope(this, new NameScope());
            Canvas containerCanvas = new Canvas();
            List<string> sprites = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
         
                string spriteName = "Spider" + i.ToString("0000");
                AnimatingSprites.Add(spriteName);

                RectangleGeometry myRectangleGeometry = new RectangleGeometry();

                var randomStartY = Randomer.Next(0, FrameWidth);
                int randomStartX = -Randomer.Next(FrameHeight, FrameHeight * 4);
                
                myRectangleGeometry.Rect = new Rect(randomStartY, randomStartX, FrameWidth, FrameHeight);
                // Assign the geometry a name so that
                // it can be targeted by a Storyboard.
                this.RegisterName(
                    spriteName, myRectangleGeometry);

                Sprite sprite = new SpiderSprite(FrameWidth, FrameHeight,1000,4000,"png",randomStartY,randomStartX);
                var p = sprite.doThing(myRectangleGeometry, this.MainGrid, spriteName,this);
                sprite.AnimationComplete += HalloweenSpiderRectAnimation_Completed;


                // Start the storyboard when the Path loads.



                containerCanvas.Children.Add(p);

                Content = containerCanvas;

            }
        }

        private void HalloweenSpiderRectAnimation_Completed(object sender, EventArgs e)
        {
            int x = 0;
            int y = x;
        }

    }

}
