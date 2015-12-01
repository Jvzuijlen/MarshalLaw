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

        public Vector2 Dimensions;
        public Vector2 Screensize;
        public Point Position;
        public DisplayMode DisplayMode;
        public int Scale;

        public ContentManager Content { private set; get; }     //Contentmanager regelt het laden en verwijdered van content, zoals sprites en text

        public bool IsTransitioning;
        public bool HasChangedScreen;

        public bool ScreenDimChanged;

        public GraphicsDevice GraphicsDevice;   //Maak een Graphics Devic
        public SpriteBatch SpriteBatch;         //Maak een Sprite

        Screen currentscreen;
        //Screen newscreen;
        string newscreen;

        Image fade;
        FadeEffect fadeEffect;

        //Contructor
        public ScreenManager()
        {
            DisplayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            Screensize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            //Screen Centered
            Dimensions = new Vector2(506, 84);
            Position = new Point((int)(Screensize.X - Dimensions.X) / 2, (int)(Screensize.Y - Dimensions.Y) / 2);

            //FullScreen, zorg dat screen op borderless staat
            //Dimensions = new Vector2(Screensize.X, Screensize.Y-40);
            //Position = new Point(0, 0);

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
                fadeEffect = new FadeEffect(2.0f, 0.0f);
            fade.SourceRect = new Rectangle(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            fade.LoadContent( 0, 0, true, 1.0f);
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
                    HasChangedScreen = true;
                }

                if (HasChangedScreen && fade.Alpha <= 0.0f)
                    IsTransitioning = false;
                fade.Update(gameTime);
            }
        }
    }
}
