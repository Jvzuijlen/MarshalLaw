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
        static int numItems = 8;
        Control1_Field[] fields = new Control1_Field[numFields];
        Control1_Item[] items = new Control1_Item[numItems];
        Control1_Item resultionOption;
        Control1_Item fpsOption;


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

            items[0] = new Control1_Item("Test1","Test1");
            items[1] = new Control1_Item("Test2", "Test2");
            items[2] = new Control1_Item("Test3", "Test3");
            items[3] = new Control1_Item("Test4", "Test4");
            items[4] = new Control1_Item("Test5", "Test5");
            items[5] = new Control1_Item("Test6", "Test6");
            items[6] = new Control1_Item("Test7", "Test7");
            items[7] = new Control1_Item("Test8", "Test8");
        }


        public override void LoadContent()
        {
            base.LoadContent();
            control.LoadContent();

            foreach (var control_field in fields)
            {
                control_field.LoadContent();
            }

            foreach (var item in items)
            {
                item.LoadContent();
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

            foreach (var item in items)
            {
                item.UnloadContent();
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            control.Update(gameTime);

            foreach (var control_field in fields)
            {
                control_field.Status = -1;
                control_field.SetStatus(control.CurrentActiveField);
                control_field.Update(gameTime);
            }



            switch (control.CurrentActiveField)
            {
                case 2:
                    fields[control.CurrentActiveField].IsActive = true;
                    foreach (var item in items)
                    {
                        item.Update(gameTime);
                    }
                    break;
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

            switch (control.CurrentActiveField)
            {
                case 2:
                    foreach (var item in items)
                    {
                        item.Draw(spriteBatch);
                    }
                    break;
            }
        }
    }
}
