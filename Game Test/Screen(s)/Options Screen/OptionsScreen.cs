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

        Image background, field_active, mainbuttonup, mainbuttonmiddle, mainbuttondown;



        //Contructor
        public OptionsScreen()
        {
            background = new Image("OptionsScreen/poster_background");
            field_active = new Image("OptionsScreen/field_active");
            mainbuttonup = new Image("OptionsScreen/buttonup_selected");
            mainbuttonmiddle = new Image("OptionsScreen/buttonmiddel_selected");
            mainbuttondown = new Image("OptionsScreen/buttondown_selected");
        }


        public override void LoadContent()
        {
            base.LoadContent();

            //LoadContent for the Images, and position them to the window dimensions
            #region
            background.LoadContent(
                                   pos_X:0,
                                   pos_Y:0,
                                   centered:true,
                                   scale:new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f)
                                  );
            field_active.LoadContent( 
                                     pos_X:(int)(573 * ScreenManager.Instance.Dimensions.X / 1920),
                                     pos_Y:0,
                                     centered:true,
                                     scale:new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttonup.LoadContent(
                                     pos_X: (int)(214 * ScreenManager.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(174 * ScreenManager.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttonmiddle.LoadContent(
                                     pos_X: (int)(214 * ScreenManager.Instance.Dimensions.X / 1920),
                                     pos_Y: (int)(443 * ScreenManager.Instance.Dimensions.Y / 1080),
                                     centered: true,
                                     scale: new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f)
                                    );
            mainbuttondown.LoadContent(
                                     pos_X: (int)(210 * ScreenManager.Instance.Dimensions.X / 1920),
                                     pos_Y: 0,
                                     centered: true,
                                     scale: new Vector2(ScreenManager.Instance.Dimensions.X / 2732f, ScreenManager.Instance.Dimensions.Y / 1536f)
                                    );
            #endregion

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            background.UnloadContent();
            field_active.UnloadContent();

            mainbuttonup.UnloadContent();
            mainbuttonmiddle.UnloadContent();
            mainbuttondown.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            background.Update(gameTime);
            field_active.Update(gameTime);

            mainbuttonup.Update(gameTime);
            mainbuttonmiddle.Update(gameTime);
            mainbuttondown.Update(gameTime);

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
            field_active.Draw(spriteBatch);

            //mainbuttonup.Alpha = 0.5f;
            mainbuttonup.Draw(spriteBatch);
            mainbuttonmiddle.Draw(spriteBatch);
            //mainbuttondown.Draw(spriteBatch);
        }
    }
}
