using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    class SprintAnimationEventArgs: EventArgs
        {
            private readonly string spriteName;

            public SprintAnimationEventArgs(string spriteName)
            {
                this.spriteName = spriteName;
            }

            public string SpriteName
        {
                get { return this.spriteName; }
            }
        }
    }
