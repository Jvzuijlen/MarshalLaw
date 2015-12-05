using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Control1
    {
        Image background, field_active, mainbuttonup, mainbuttonmiddle, mainbuttondown, mainbuttonup_pressed, mainbuttondown_pressed;

        cText title1, title2, title3;
        enum selection
        {
            buttonup, buttonmiddle, buttondown, fieldactive,
        };

        

        int numberControlItems;
        public int CurrentActiveItem;
        selection currentSelected;

        public Control1(int numItems, string title1, string title2, string title3)
        {
            //Create instances of all the Images
            #region
            background = new Image("OptionsScreen/poster_background");
            field_active = new Image("OptionsScreen/field_active");
            mainbuttonup = new Image("OptionsScreen/buttonup_selected");
            mainbuttonmiddle = new Image("OptionsScreen/buttonmiddel_selected");
            mainbuttondown = new Image("OptionsScreen/buttondown_selected");
            mainbuttonup_pressed = new Image("OptionsScreen/buttonup_selected_pressed");
            mainbuttondown_pressed = new Image("OptionsScreen/buttondown_selected_pressed");
            #endregion

            this.numberControlItems = numItems;
            if (numberControlItems > 2)
                CurrentActiveItem = 1;
            currentSelected = selection.buttonmiddle;
            this.title1 = new cText(title1, "DryGood");
            this.title2 = new cText(title2, "DryGood");
            this.title3 = new cText(title3, "DryGood");
            
        }

        public virtual void LoadContent()
        {
            //LoadContent for the Images, and position them to the window dimensions
            #region
            background.LoadContent(
                                   pos_X: 0,
                                   pos_Y: 0,
                                   centered: true,
                                   scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                  );
            field_active.LoadContent(
                                     pos_X: (int)(573 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: 0,
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttonup.LoadContent(
                                     pos_X: (int)(214 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(174 * GameSettings.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttonup_pressed.LoadContent(
                                     pos_X: (int)(214 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(174 * GameSettings.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttonmiddle.LoadContent(
                                     pos_X: (int)(214 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(443 * GameSettings.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttondown.LoadContent(
                                     pos_X: (int)(214 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(802 * GameSettings.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttondown_pressed.LoadContent(
                                     pos_X: (int)(214 * GameSettings.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(802 * GameSettings.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f)
                                    );
            #endregion


            SetTitlePosition(title1, 0);
            SetTitlePosition(title2, 1);
            SetTitlePosition(title3, 2);
        }
        public virtual void UnloadContent()
        {
            background.UnloadContent();
            field_active.UnloadContent();

            mainbuttonup.UnloadContent();
            mainbuttonup_pressed.UnloadContent();
            mainbuttonmiddle.UnloadContent();
            mainbuttondown.UnloadContent();
            mainbuttondown_pressed.UnloadContent();

            title1.UnloadContent();
            title2.UnloadContent();
            title3.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            
            //
            //SwitchCase for Selected Update
            //
            #region
            switch (currentSelected)
            {
                case selection.buttonup:
                    mainbuttonup.Update(gameTime);
                    break;
                case selection.buttonmiddle:
                    mainbuttonmiddle.Update(gameTime);
                    break;
                case selection.buttondown:
                    mainbuttondown.Update(gameTime);
                    break;
                case selection.fieldactive:
                    field_active.Update(gameTime);
                    break;
            }
            #endregion

            title1.Update(gameTime);
            title2.Update(gameTime);
            title3.Update(gameTime);

            if(InputManager.Instance.KeyPressed(Keys.Up))
            {
                if(currentSelected == selection.buttonmiddle)
                    currentSelected = selection.buttonup;
                else if (currentSelected == selection.buttondown)
                    currentSelected = selection.buttonup;
            }

            if (InputManager.Instance.KeyPressed(Keys.Down))
            {
                if (currentSelected == selection.buttonmiddle)
                    currentSelected = selection.buttondown;
                else if (currentSelected == selection.buttonup)
                    currentSelected = selection.buttondown;
            }

            if (InputManager.Instance.KeyPressed(Keys.Right))
            {
                currentSelected = selection.fieldactive;
            }
            if (InputManager.Instance.KeyPressed(Keys.Left))
            {
                currentSelected = selection.buttonup;
            }
            //mainbuttonup_pressed.Update(gameTime);
            //mainbuttondown_pressed.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            switch(currentSelected)
            {
                case selection.buttonup:
                    mainbuttonup.Draw(spriteBatch);
                    break;
                case selection.buttondown:
                    mainbuttondown.Draw(spriteBatch);
                    break;
                case selection.fieldactive:
                    field_active.Draw(spriteBatch);
                    break;
            }

            mainbuttonmiddle.Draw(spriteBatch);
            //mainbuttonup_pressed.Draw(spriteBatch);
            //mainbuttondown_pressed.Draw(spriteBatch);

            title1.DrawString(spriteBatch);
            title2.DrawString(spriteBatch);
            title3.DrawString(spriteBatch);
        }

        public void AnimationDown(GameTime gameTime)
        {
            //Title pos
            //
        }

        public void SetTitlePosition(cText text, int status)
        {
            //The scale is nessecary because if the window gets resized the position changes aswell.
            float scale = GameSettings.Instance.Dimensions.X / 1366;
            //The middle x coordiantes get calculated here, it takes the width of the text and the width of the control bar,
            //and divides those by 2 to calculate the middle
            float x_position = (((375 - 170) - (text.SourceRect.Width / 2)) / 2);

            switch (status)
            {
                case 0:
                    text.Position = new Vector2((160 + x_position) * scale, 230 * scale);
                    break;
                case 1:
                    text.Position = new Vector2((155 + x_position) * scale, 350 * scale);
                    break;
                case 2:

                    text.Position = new Vector2((160 + x_position) * scale, 485 * scale);
                    break;
            }
        }
    }
}
