using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game_Test
{
    public class ScreenManager
    {
        //Fields
        private static ScreenManager instance;

        /// <summary>
        /// Maken van een Instance, Deze Class is een Singleton class
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        //Waardes voor het scherm zodat hiermee gewerkt kan worden in andere classes
        public Vector2 Dimensions;
        public Vector2 Screensize;
        public Point Position;
        public DisplayMode DisplayMode;

        //Contentmanager regelt het laden en verwijdered van content, zoals sprites en text
        public ContentManager Content { private set; get; }


        public bool IsTransitioning;
        public bool HasChangedScreen;

        public bool ScreenDimChanged;

        public GraphicsDevice GraphicsDevice;   //Maak een Graphics Devic
        public SpriteBatch SpriteBatch;         //Maak een Sprite

        Screen currentscreen;
        string newscreen;

        Image fade;
        FadeEffect fadeEffect;

        //Contructor
        private ScreenManager()
        {
            DisplayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            Screensize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);


            Dimensions = new Vector2(1920, 1080);
            //Dimensions = new Vector2(506, 84);
            //Zorgt ervoor dat de window in het midden van je beeldscherm begint
            Position = new Point((int)(Screensize.X - Dimensions.X) / 2, (int)(Screensize.Y - Dimensions.Y) / 2);

            //CurrentScreen begint met het SplashScreen
            currentscreen = new SplashScreen();
            IsTransitioning = false;
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentscreen.LoadContent();

            if (fade == null)
                fade = new Image("Images/black");
            if (fadeEffect == null)
                fadeEffect = new FadeEffect(3.0f, 0.0f);
            fade.SourceRect = new Rectangle(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            fade.LoadContent( 0, 0, true, new Vector2(Dimensions.X, Dimensions.Y));
        }

        public void UnloadContent()
        {
            currentscreen.UnloadContent();
            fade.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentscreen.Update(gameTime);
            TransitionScreen(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            currentscreen.Draw(spriteBatch);

            if (IsTransitioning)
                fade.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void Start()
        {
            currentscreen.UnloadContent();
            currentscreen = new MenuScreen();
            currentscreen.LoadContent();
            fade.SetScale(new Vector2(Dimensions.X, Dimensions.Y));
        }

        public void ChangeScreen(string screenName)
        {
            newscreen = screenName;
            IsTransitioning = true;
            fade.Alpha = 0.0f;
        }
        
        public void TransitionScreen(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                fade.Alpha = fadeEffect.Update(gameTime);
                if (fade.Alpha >= 1.0f)
                {
                    currentscreen.UnloadContent();
                    currentscreen = (Screen)Activator.CreateInstance(Type.GetType("Game_Test." + newscreen));
                    currentscreen.LoadContent();
                    fade.SetScale(new Vector2(Dimensions.X, Dimensions.Y));
                    HasChangedScreen = true;
                }

                if (HasChangedScreen && fade.Alpha <= 0.0f)
                    IsTransitioning = false;

                //fade.Update(gameTime);
            }
        }
    }
}
