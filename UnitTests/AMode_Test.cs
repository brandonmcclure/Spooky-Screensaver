using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyScreensaver_wpf;

namespace UnitTests
{
    [TestClass]
    public class AMode_Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            AMode aMode = new AMode();
            Mock<iSprite> sprite = new Mock<iSprite>();
            aMode.Sprite_wfCollection.Add(sprite.Object);

            aMode.moveTimer_Tick(new object(),new EventArgs());
            sprite.Verify(mock => mock.tick(), Times.Once());
        }
    }
}
