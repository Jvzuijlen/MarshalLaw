using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Test
{
    class CharCreationScreen : Screen
    {
        Control1 control;
        static int numFields = 3;
        static int numItems = 4;
        Control1_Field[] fields = new Control1_Field[numFields];
        Control1_Item[] items = new Control1_Item[numItems];

        //Contructor
        public CharCreationScreen()
        {

            fields[0] = new Control1_Field( 0, numFields, "Character", numItems);
            fields[1] = new Control1_Field( 1, numFields, "Attributes", numItems);
            fields[2] = new Control1_Field( 2, numFields, "Appearance", numItems);

            control = new Control1(numFields, numItems);

            items[0] = new Control1_Item( 0, "Name", "Test1", 0, 1);
            items[1] = new Control1_Item( 1, "Starting Perk", "Test2", 0, 1);
            items[2] = new Control1_Item( 2, "Head", "Test3", 0, 1);
            items[3] = new Control1_Item( 3, "Test4", "Test4", 0, 1);
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

            foreach (var item in items)
            {
                item.IsSelected = false;
                item.SetSelected((int)control.currentSelectedItemControl);
                item.Update(gameTime);
            }
            if (control.currentSelectedMainControl == Control1.selection.fieldactive)
            {
                items[control.CurrentActiveItem].IsSelected = true;
                items[control.CurrentActiveItem].Update(gameTime);
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
                case 0:
                    foreach (var item in items)
                    {
                        item.Draw(spriteBatch);
                    }
                    break;
            }
        }
    }
}
