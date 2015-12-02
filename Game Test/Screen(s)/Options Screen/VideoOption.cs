using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class VideoOption
    {
        cText title;

        public VideoOption()
        {
            title = new cText("Video", "DryGood");
        }

        public void LoadContent()
        {
            title.LoadContent();
        }
        public void UnloadContent()
        {
            title.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            title.Update(gameTime);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            title.DrawString(spriteBatch);
        }
    }
}
