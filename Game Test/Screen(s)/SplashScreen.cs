using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Test
{
    public class SplashScreen : Screen
    {
        Image background;
        Image[] bar;
        int LoadSpeed = 20;
        float status;

        public SplashScreen()
        {
            background = new Image("Images/loadingbar");
            bar = new Image[17];
            for (int i = 0; i < bar.Length-1; i++)
            {
                bar[i] = new Image("Images/loadingbar_bar");
            }
            bar[bar.Length - 1] = new Image("Images/loadingbar_end");
            Size = new Vector2(506, 84);
        }

        public override void LoadContent()
        {
            ScreenManager.Instance.Dimensions = Size;
            ScreenManager.Instance.ScreenDimChanged = true;

            base.LoadContent();
            background.LoadContent( 0, 0, true, 1.0f);

            for (int i = 0; i < bar.Length; i++)
            {
                bar[i].LoadContent((int)background.Position.X + 46 + (i * 26), 0, true, 1.0f);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            background.UnloadContent();
            for (int i = 0; i < bar.Length; i++)
            {
                bar[i].UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            background.Update(gameTime);
            if (status < bar.Length+1)
            {
                status += LoadSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if ((int)status == bar.Length+1)
                ScreenManager.Instance.Start();
                //ScreenManager.Instance.ChangeScreen("MenuScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            background.Draw(spriteBatch);
            for (int i = 0; i < (int)status; i++)
            {
                bar[i].Draw(spriteBatch);
            }
        }
    }
}
