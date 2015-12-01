using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Test
{
    public class MenuScreen : Screen
    {
        Image background, sign, poster;
        MenuItem[] menuItems;
        Vector2 menuLenght;
        Vector2 menuPosition;
        int currentSelected;
        string[] text = { "Start", "Options", "Exit"};
        cText test_text;
        List<DisplayMode> dmList = new List<DisplayMode>();


    public MenuScreen()
        {
            background = new Image("TitleScreen/background");
            sign = new Image("TitleScreen/woodsign_marshal_law");
            poster = new Image("TitleScreen/gun_poster1280x720");
            menuItems = new MenuItem[text.Length];


            for (int i = 0; i < text.Length; i++)
            {
                menuItems[i] = new MenuItem();
                menuItems[i].imageselected = new Image("TitleScreen/menutext_" + (i+1).ToString() + "_selected");
                menuItems[i].imageunselected = new Image("TitleScreen/menutext_" + (i+1).ToString() + "_unselected");
                menuItems[i].ItemID = i;
            }
            menuItems[0].Selected = true;

            Size = new Vector2(1366, 768);
            ScreenManager.Instance.Dimensions = Size;
            ScreenManager.Instance.ScreenDimChanged = true;

            foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                dmList.Add(dm);
            }

            test_text = new cText(dmList[0].AspectRatio.ToString() + " NumDisplaymodes:" + dmList.Count);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background.LoadContent(0, 0, true, ScreenManager.Instance.Dimensions.X / 2732f);
            sign.LoadContent(0, 0, false, ScreenManager.Instance.Dimensions.X / 2732f);
            poster.LoadContent(0, (int)(350 * (ScreenManager.Instance.Dimensions.X / 1920f)), true, ScreenManager.Instance.Dimensions.X / 1920f);
            test_text.LoadContent();

            //Maak menu aan en zet op midde van scherm
            for (int i = 0; i < text.Length; i++)
            {
                menuItems[i].Position.X = 0;
                menuItems[i].Position.Y = 0;
                menuItems[i].LoadContent();
                menuLenght.Y += menuItems[i].imageselected.SourceRect.Height;
            }
            menuPosition.Y = poster.Position.Y + ((poster.SourceRect.Height - menuLenght.Y) / 2);

            int temp = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0)
                {
                    temp += menuItems[i - 1].imageselected.SourceRect.Height;
                    menuItems[i].Position.Y = menuPosition.Y + temp;
                }
                else
                    menuItems[i].Position.Y = menuPosition.Y;
                menuItems[i].SetPosition();
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            for (int i = 0; i < text.Length; i++)
            {
                menuItems[i].imageselected.UnloadContent();
                menuItems[i].imageunselected.UnloadContent();
            }

            background.UnloadContent();
            sign.UnloadContent();
            poster.UnloadContent();
            test_text.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < text.Length; i++)
            {
                menuItems[i].Update(gameTime);
            }

            background.Update(gameTime);
            sign.Update(gameTime);
            poster.Update(gameTime);
            test_text.Update(gameTime);

            if (background.Alpha > 0.0f)
                IsVisible = true;

            if (InputManager.Instance.KeyPressed(Keys.Down))
            {
                menuItems[currentSelected].Selected = false;
                currentSelected++;
                if (currentSelected == text.Length)
                    currentSelected = 0;
                menuItems[currentSelected].Selected = true;

            }

            if (InputManager.Instance.KeyPressed(Keys.Up))
            {
                menuItems[currentSelected].Selected = false;
                currentSelected--;
                if (currentSelected == -1)
                    currentSelected = text.Length -1;
                menuItems[currentSelected].Selected = true;
            }

            if (menuItems[currentSelected].ItemID == 2 && InputManager.Instance.KeyPressed(Keys.Enter))
            {
                Game1.ExitGame = true;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            background.Draw(spriteBatch);
            sign.Draw(spriteBatch);
            poster.Draw(spriteBatch);
            test_text.DrawString(spriteBatch);
            

            for (int i = 0; i < text.Length; i++)
            {
                menuItems[i].Draw(spriteBatch);
            }
        }
    }

    public class MenuItem
    {
        //Fields
        public Vector2 Position;
        public int ItemID;
        public bool Selected;
        public Image imageselected;
        public Image imageunselected;
        FadeEffect fadeEffect;

        public void LoadContent()
        {
            imageselected.LoadContent( 0, (int)Position.Y, true, ScreenManager.Instance.Dimensions.X / 1920f);
            imageunselected.LoadContent( 0, (int)Position.Y, true, ScreenManager.Instance.Dimensions.X / 1920f);
        }

        public void UnloadContent()
        {
            imageselected.UnloadContent();
            imageunselected.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Selected == true)
            {
                if (fadeEffect == null)
                    fadeEffect = new FadeEffect(0.5f, 1.0f, 0.5f);
                imageselected.Alpha = fadeEffect.Update(gameTime);
            }
            else
            {
                imageselected.Alpha = 1.0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            imageunselected.Draw(spriteBatch);
            if (Selected)
                imageselected.Draw(spriteBatch);

        }

        public void SetPosition()
        {
            imageselected.Position.Y = Position.Y;
            imageunselected.Position.Y = Position.Y;
        }

    }
}
