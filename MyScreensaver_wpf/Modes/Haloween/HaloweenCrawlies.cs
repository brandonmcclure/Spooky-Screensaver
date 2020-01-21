using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyScreensaver_wpf.Modes.Haloween
{
    public class SpiderSprite : Sprite
    {
        public SpiderSprite(int frameWidth, int frameHeight, int randomDurationStart, int randomDurationEnd, string spriteSheetName, int randomStartY, int randomStartX) : base(frameWidth, frameHeight, randomDurationStart, randomDurationEnd, spriteSheetName, randomStartY, randomStartX)
        {
        }

        public override Path DoThing(RectangleGeometry RectangleGeometry, Grid grid, string spriteName, FrameworkElement frameworkElement)
        {
            throw new NotImplementedException();
        }
    }
}
