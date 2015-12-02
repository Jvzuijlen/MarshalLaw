using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Test
{
    public class OptionsScreen : Screen
    {

        Image background;


        //Contructor
        public OptionsScreen()
        {
            background = new Image("OptionsScreen/poster_background");
        }


        public override void LoadContent()
        {
            base.LoadContent();
            background.LoadContent(0, 0, true, new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            background.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            background.Update(gameTime);

            //When the Escape key has been pressed exit the game
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            background.Draw(spriteBatch);
            
        }
    }
}
