using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyScreensaver_wpf;

namespace UnitTests
{
    [TestClass]
    public class AMode_Test
    {
        [TestMethod]
        public void moveTimer_Tick_ShouldCallTheTickFunctionOnAllSprite()
        {
            AMode aMode = new AMode();
            List<Mock<ASprite>> mockedSprites = new List<Mock<ASprite>>();

            for (int index = 0; index <= 9; index++){
                Mock<ASprite> sprite = new Mock<ASprite>();
                aMode.Sprite_wfCollection.Add(sprite.Object);
                mockedSprites.Add(sprite);
            }
           

            aMode.moveTimer_Tick(new object(),new EventArgs());

            for (int index = 0; index <= 9; index++)
            {
                mockedSprites[index].Verify(mock => mock.tick(), Times.Once());
            }
        }
    }
}
