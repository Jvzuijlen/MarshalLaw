﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Test
{
    public class Image
    {
        //Fields
        public float Alpha, Scale;
        public string Path;
        public Vector2 Position;
        public Rectangle SourceRect;
        public Texture2D Texture;
        public bool IsInvisible;
        Vector2 origin;

        ContentManager content;

        public Image(string path)
        {
            this.Path = path;
            this.Position = Vector2.Zero;
            this.Alpha = 1.0f;
            this.SourceRect = Rectangle.Empty;
        }

        public void LoadContent(int pos_X, int pos_Y, bool centered, float scale)
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Texture == null)
                Texture = content.Load<Texture2D>(Path);

            Vector2 dimensions = Vector2.Zero;
            this.Position.X = pos_X;
            this.Position.Y = pos_Y;

            if(Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.Y += Texture.Height;

                dimensions.X = scale * dimensions.X;
                dimensions.Y = scale * dimensions.Y;
            }

            if (Position.X == 0 && ScreenManager.Instance.Dimensions.X > dimensions.X && centered)
            {
                Position.X = (ScreenManager.Instance.Dimensions.X - dimensions.X) / 2;
            }
            if (Position.Y == 0 && ScreenManager.Instance.Dimensions.Y > dimensions.Y && centered)
            {
                Position.Y = (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2;
            }

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle((int)Position.X, (int)Position.Y, (int)dimensions.X, (int)dimensions.Y);
            this.Scale = scale;
        }

        public void UnloadContent()
        {
            content.Unload();
        }
        public void Update(GameTime gameTime)
        {
            if (Alpha <= 0.0f)
                IsInvisible = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(Texture, Position, SourceRect, Color.White * Alpha, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(Texture, SourceRect, Color.White * Alpha);
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White * Alpha, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
