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
        enum selection
        {
            buttonup, buttonmiddle, buttondown, fieldactive,
        };

        selection currentSelected;

        public Control1(int numItems)
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

            currentSelected = selection.buttonmiddle;
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
        }
    }
}
