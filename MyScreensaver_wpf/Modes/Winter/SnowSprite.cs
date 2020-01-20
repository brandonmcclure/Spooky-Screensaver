﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MyScreensaver_wpf.Modes.Winter
{
    public class SnowSprite : Sprite
    {
        private Random Randomer = new Random();

        public SnowSprite(int frameWidth, int frameHeight, int randomDurationStart, int randomDurationEnd, string spriteSheetName, int randomStartY, int randomStartX) : base(frameWidth, frameHeight, randomDurationStart, randomDurationEnd, spriteSheetName, randomStartY, randomStartX)
        {
        }

        public System.Windows.Shapes.Path doThing(RectangleGeometry RectangleGeometry, Grid grid, string spriteName, FrameworkElement frameworkElement)
        {

            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
            myPath.Fill = System.Windows.Media.Brushes.LightGray;
            var fullBitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/" + spriteSheetName));
            Int32Rect croppRect = new Int32Rect(0, 0, _FrameWidth, _FrameHeight);
            var croppedBitmap = new CroppedBitmap(fullBitmap, croppRect);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder(); Guid photoID = System.Guid.NewGuid(); String photolocation = photoID.ToString() + ".jpg"; encoder.Frames.Add(BitmapFrame.Create((BitmapImage)fullBitmap)); using (var filestream = new FileStream(photolocation, FileMode.Create)) { encoder.Save(filestream); }

            var mybrush = new ImageBrush(croppedBitmap);
            mybrush.Transform = new TranslateTransform(0, 0);
            mybrush.AlignmentX = AlignmentX.Left;
            mybrush.AlignmentY = AlignmentY.Top;
            mybrush.Stretch = Stretch.Fill;
            myPath.Fill = mybrush;
            myPath.StrokeThickness = 1;
            myPath.Stroke = System.Windows.Media.Brushes.LightGray;
            myPath.Data = RectangleGeometry;

            RectAnimation myRectAnimation = new RectAnimation();
            myRectAnimation.BeginTime = TimeSpan.FromSeconds(Randomer.NextDouble());
            myRectAnimation.Duration = TimeSpan.FromMilliseconds(RandomNumber(_randomDurationStart, _randomDurationEnd));
            myRectAnimation.FillBehavior = FillBehavior.HoldEnd;

            // Set the From and To properties of the animation.
            myRectAnimation.From = new Rect(RectangleGeometry.Rect.Y, RectangleGeometry.Rect.X, _FrameWidth, _FrameHeight);
            myRectAnimation.To = new Rect(RectangleGeometry.Rect.Y, grid.ActualHeight, _FrameWidth, _FrameHeight);

            EventArgs ea = new EventArgs();
            myRectAnimation.Completed += new EventHandler(RectAnimation_Completed);

            // Set the animation to target the Rect property
            // of the object named "MyAnimatedRectangleGeometry."
            Storyboard.SetTargetName(myRectAnimation, spriteName);
            Storyboard.SetTargetProperty(
                myRectAnimation, new PropertyPath(RectangleGeometry.RectProperty));

            Storyboard ellipseStoryboard = new Storyboard();

            ellipseStoryboard.Children.Add(myRectAnimation);

            myPath.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                ellipseStoryboard.Begin(frameworkElement);
            };

            return myPath;
        }

        private void RectAnimation_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
