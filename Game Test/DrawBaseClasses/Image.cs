using System;
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
        public float Alpha;
        public string Path;
        public Vector2 Position, Scale;
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

        /// <summary>
        /// Load the content for the Image
        /// </summary>
        /// <param name="pos_X">Defines the X coordinate for the position</param>
        /// <param name="pos_Y">Defines the Y coordinate for the position</param>
        /// <param name="centered">If true then if the coordinates are 0 then the image will be centered</param>
        /// <param name="scale">Scale is used to create the Image size</param>
        public void LoadContent(int pos_X, int pos_Y, bool centered, Vector2 scale)
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

                dimensions.X = scale.X * dimensions.X;
                dimensions.Y = scale.Y * dimensions.Y;
            }

            //When the Position is 0 and the Texture dimensions are smaller then the window and
            //the image is supposed to be centered it will center the image
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
            //If the Alpha is 0.0f or lower then the image isn't visible
            if (Alpha <= 0.0f)
                IsInvisible = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(Texture, Position, SourceRect, Color.White * Alpha, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
            //spriteBatch.Draw(Texture, SourceRect, Color.White * Alpha);

            //Draw the Image
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White * Alpha, 0.0f, Vector2.Zero, new Vector2(Scale.X, Scale.Y), SpriteEffects.None, 0.0f);
        }

        public void SetScale(Vector2 scale)
        {
            this.Scale = scale;
        }
    }
}
