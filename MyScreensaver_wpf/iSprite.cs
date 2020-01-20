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
    public interface iSprite
    {
        System.Windows.Shapes.Path doThing(RectangleGeometry RectangleGeometry, Grid grid, string spriteName, FrameworkElement frameworkElement);
    }
}
