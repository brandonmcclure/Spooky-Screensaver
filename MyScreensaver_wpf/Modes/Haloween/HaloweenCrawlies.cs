using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf.Modes.Haloween
{
    public class SpiderSprite : Sprite
    {
        public SpiderSprite(int frameWidth, int frameHeight, int randomDurationStart, int randomDurationEnd, string spriteSheetName, int randomStartY, int randomStartX) : base(frameWidth, frameHeight, randomDurationStart, randomDurationEnd, spriteSheetName, randomStartY, randomStartX)
        {
        }
    }
}
