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
    public class MapTestScreen : Screen
    {
        Map map;

        Texture2D Texture;
        string Path = "SpriteSheets/terrain";
        Player player;

        public MapTestScreen()
        {

            map = new Map("testmap2");
            player = new Player();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

                Texture = content.Load<Texture2D>(Path);

            player.LoadContent(0, 0);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            player.Update(gameTime);

            //When the Escape key has been pressed exit the game
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen("MenuScreen");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            float Scale = 1.0f;

            spriteBatch.Draw(Texture, new Vector2 (0, 0), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(0, 20), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(20, 0), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(20, 20), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(40, 0), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(40, 20), new Rectangle(756, 288, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(Texture, new Vector2(40, 20), new Rectangle(0, 0, 32, 32), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0.5f);

            player.Draw(spriteBatch);
        }
    }
}
