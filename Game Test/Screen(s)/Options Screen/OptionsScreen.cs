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
        Control1 control;
        Control1_Item videoOption;
        Control1_Item audioOption;
        Control1_Item controlOption;


        //Contructor
        public OptionsScreen()
        {
            control = new Control1(3);

            videoOption = new Control1_Item("Video", 1, 2);
            audioOption = new Control1_Item("Audio", 2, 0);
            controlOption = new Control1_Item("Control", 3, 1);
        }


        public override void LoadContent()
        {
            base.LoadContent();
            control.LoadContent();

            videoOption.LoadContent();
            audioOption.LoadContent();
            controlOption.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            control.UnloadContent();

            videoOption.UnloadContent();
            audioOption.UnloadContent();
            controlOption.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            control.Update(gameTime);

            videoOption.Update(gameTime);
            audioOption.Update(gameTime);
            controlOption.Update(gameTime);

            //When the Escape key has been pressed exit the game
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            control.Draw(spriteBatch);

            videoOption.Draw(spriteBatch);
            audioOption.Draw(spriteBatch);
            controlOption.Draw(spriteBatch);
        }
    }
}
