using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    interface IMode
    {
        IConfiguration _configuration { get; set; }
        void moveTimer_Tick(object sender, EventArgs e);
    }
    class WinterModeConfiguration
    {
        int NumberOFSnowflakes { get; set; }
    }
    public class AMode : IMode
    {
        public IConfiguration _configuration { get; set; }
        public List<iSprite> Sprite_wfCollection = new List<iSprite>();

        public virtual void moveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Sprite_wf spr in Sprite_wfCollection)
            {
                spr.tick();
            }
        }
    }

    class WinterMode : IMode
    {
        public IConfiguration _configuration { get; set; }
       readonly  private List<Sprite_wf> Sprite_wfCollection = new List<Sprite_wf>();

        public WinterMode(Rectangle Bounds, IConfiguration configuration)
        {
            _configuration = configuration;
            //mySprite_wf = new Sprite_wf(pictureBox, SpiderScreensaver.Properties.Resources.SnowSprite, Bounds, 98, 98 );
            //mySprite_wf.movementType = "Fall";
            //Sprite_wfCollection.Add(mySprite_wf);
            Configure();
        }
        public void moveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Sprite_wf spr in Sprite_wfCollection)
            {

                spr.tick();

            }
        }
        public void Configure()
        {
            /*
             * If I need to do any configuration I would "override" this method
             */

        }
    }

    class HalloweenMode : IMode
    {
        private List<Sprite_wf> Sprite_wfCollection = new List<Sprite_wf>();
        private string movementType;
        public IConfiguration _configuration { get; set; }

        public HalloweenMode(System.Windows.Forms.PictureBox pictureBox, Rectangle Bounds, IConfiguration configuration)
        {
            _configuration = configuration;
            Configure();
        }
        public void Configure()
        {
                movementType = _configuration["movementType"];
        }
        public void moveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Sprite_wf spr in Sprite_wfCollection)
            {
                spr.tick();

            }
        }
    }
}
