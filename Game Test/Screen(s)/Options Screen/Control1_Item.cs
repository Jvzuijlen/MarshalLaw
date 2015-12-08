using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Control1_Item
    {
        private Image arrow_left, arrow_right;
        private cText itemtitle;
        public bool IsSelected { get; set; }

        public Control1_Item(string title)
        {
            this.itemtitle = new cText(title, "DryGood");
            arrow_left = new Image("OptionsScreen/arrow_left");
            arrow_right = new Image("OptionsScreen/arrow_right");
        }

        public void LoadContent()
        {
            itemtitle.Position = new Vector2(245, 191);
            itemtitle.LoadContent();
            itemtitle.Scale.X = (GameSettings.Instance.Dimensions.X / 2560);
            itemtitle.Scale.Y = (GameSettings.Instance.Dimensions.Y / 1440);
            

            arrow_left.LoadContent( 205, 200, false, 
                                    scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                  );
            arrow_right.LoadContent(100+ itemtitle.SourceRect.Width, 200, false,
                                    scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                  );
        } 

        public void UnloadContent()
        {
            itemtitle.UnloadContent();
            arrow_left.UnloadContent();
            arrow_right.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            itemtitle.Update(gameTime);
            arrow_left.Update(gameTime);
            arrow_right.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            itemtitle.DrawString(spriteBatch);
            arrow_left.Draw(spriteBatch);
            arrow_right.Draw(spriteBatch);
        }
    }
}
