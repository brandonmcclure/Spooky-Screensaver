using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyScreensaver_wpf.Modes.Haloween;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf.Modes.Haloween.Tests
{
    [TestClass()]
    public class SpiderSpriteTests
    {
        [TestMethod()]
        public void SpiderSpriteTest_NullStringInputThrowsException()
        {
            //spiderSprite.DoThing(new System.Windows.Media.RectangleGeometry(), new System.Windows.Controls.Grid(), null, new System.Windows.FrameworkElement())
            SpiderSprite spiderSprite;
            Assert.ThrowsException<ArgumentNullException>(() =>
            spiderSprite = new SpiderSprite(0, 0, 0, 0, null, 0, 0)
            );
        }


    }
}