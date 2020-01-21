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
        public string _SpriteSheetName { get; private set; }
        public int _randomStartY { get; private set; }
        public int _randomStartX { get; private set; }

        protected Sprite(int frameWidth, int frameHeight, int randomDurationStart, int randomDurationEnd, string spriteSheetName, int randomStartY, int randomStartX)
        {
            _FrameWidth = frameWidth;
            _FrameHeight = frameHeight;
            _randomDurationEnd = randomDurationEnd;
            _randomDurationStart = randomDurationStart;
            _SpriteSheetName = spriteSheetName ?? throw new ArgumentNullException(nameof(spriteSheetName));
            _randomStartY = randomStartY;
            _randomStartX = randomStartX;

        }

        public abstract System.Windows.Shapes.Path DoThing(RectangleGeometry RectangleGeometry, Grid grid, string spriteName, FrameworkElement frameworkElement);

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
