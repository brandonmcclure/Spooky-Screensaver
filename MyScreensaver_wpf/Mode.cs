using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScreensaver_wpf
{
    interface iMode
    {
        IConfiguration _configuration { get; set; }
        void moveTimer_Tick(object sender, EventArgs e);
    }

    class WinterMode : iMode
    {
        public IConfiguration _configuration { get; set; }
        private List<Sprite_wf> Sprite_wfCollection = new List<Sprite_wf>();

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
        }
    }

    class HalloweenMode : iMode
    {
        Sprite_wf mySprite_wf;
        private List<Sprite_wf> Sprite_wfCollection = new List<Sprite_wf>();
        private string movementType;
        public IConfiguration _configuration { get; set; }

        public HalloweenMode(System.Windows.Forms.PictureBox pictureBox, Rectangle Bounds, IConfiguration configuration)
        {
            _configuration = configuration;
            //mySprite_wf = new Sprite_wf(pictureBox, SpiderScreensaver.Properties.Resources.sprites, Bounds,250,250);
            Sprite_wfCollection.Add(mySprite_wf);
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
