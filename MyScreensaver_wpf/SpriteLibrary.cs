using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    class SpriteLibrary
    {
        List<iSprite> iSprites = new List<iSprite>();

        internal void GenerateSprites(SpriteType snowflake, int numberOfSprites)
        {
            throw new NotImplementedException();
        }
    }

    enum SpriteType { Snowflake, Spider}
}
