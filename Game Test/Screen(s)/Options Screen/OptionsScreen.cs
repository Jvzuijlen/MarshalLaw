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
        static int numFields = 5;
        Control1_Field[] fields = new Control1_Field[numFields];


        //Contructor
        public OptionsScreen()
        {

            fields[0] = new Control1_Field("Test1");
            fields[1] = new Control1_Field("Test2");
            fields[2] = new Control1_Field("Test3");
            fields[3] = new Control1_Field("Test4");
            fields[4] = new Control1_Field("Test5");

            control = new Control1(numFields);

            fields[1].Status = 1;
            fields[2].Status = 2;
            fields[3].Status = 3;
        }


        public override void LoadContent()
        {
            base.LoadContent();
            control.LoadContent();

            foreach (var control_field in fields)
            {
                control_field.LoadContent();
            }

        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            control.UnloadContent();

            foreach (var control_field in fields)
            {
                control_field.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            control.Update(gameTime);

            foreach (var control_field in fields)
            {
                control_field.Status = -1;
                control_field.SetStatus(control.CurrentActiveItem);
                control_field.Update(gameTime);
            }

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

            foreach (var control_field in fields)
            {
                control_field.Draw(spriteBatch);
            }
        }
    }
}
