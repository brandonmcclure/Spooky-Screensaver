using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyScreensaver_wpf.Modes.Winter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyScreensaver_wpf.Modes.Winter.Tests
{
    [TestClass()]
    public class SnowSpriteTests
    {
        [TestMethod()]
        public void SnowSpriteTest_NullStringInputThrowsException()
        {
            SnowSprite _sprite;
            Assert.ThrowsException<ArgumentNullException>(() =>
            _sprite = new SnowSprite(0, 0, 0, 0, null, 0, 0)
            );
        }

        [TestMethod()]
        public void DoThingTest_NullSpriteNameThrowsException()
        {
            SnowSprite _sprite = new SnowSprite(0, 0, 0, 0, "TestSprite", 0, 0);

            Assert.ThrowsException<ArgumentException>(() =>
                _sprite.DoThing(new System.Windows.Media.RectangleGeometry(), new System.Windows.Controls.Grid(), null, new System.Windows.FrameworkElement())
            );

        }

        [TestMethod()]
        public void GetFrameTest_IfFrameWidthNotSetThrowException()
        {
            SnowSprite _sprite = new SnowSprite(0, 0, 0, 0, "TestSprite", 0, 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                _sprite.GetFrame(new System.Windows.Media.Imaging.BitmapImage())
            );
        }
        [TestMethod()]
        public void GetFrameTest_IfFrameHeightNotSetThrowException()
        {
            SnowSprite _sprite = new SnowSprite(50, 0, 0, 0, "TestSprite", 0, 0);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                _sprite.GetFrame(new System.Windows.Media.Imaging.BitmapImage())
            );
        }

    }
}