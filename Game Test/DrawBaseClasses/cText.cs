using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Test
{
    public class cText
    {
        //Fields
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        //Vector2 origin;
        ContentManager content;
        SpriteFont font;

        public cText(string Text, string fontname)
        {
            this.Text = Text;
            this.Path = String.Empty;

            switch(fontname)
            {
                case "Carne":
                    this.FontName = "SpriteFonts/Carne";
                    break;
                case "DryGood":
                    this.FontName = "SpriteFonts/DryGood/Carne";
                    break;
            }
            this.Position = Vector2.Zero;
            this.Scale = Vector2.One;
            this.Alpha = 1.0f;
            this.SourceRect = Rectangle.Empty;
        }

        public void LoadContent()
        {
            //Load the content for the text
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            //Make sure the text class has the dimensions from the font
            if(Text != string.Empty)
            {
                dimensions.X += font.MeasureString(Text).X;
                dimensions.Y += font.MeasureString(Text).Y;
            }

            //Create a rectangle wich other classes can work with
            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            this.Scale.X = ScreenManager.Instance.Dimensions.X / 1920;
            this.Scale.Y = ScreenManager.Instance.Dimensions.Y / 1080;
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            //To be Added
        }

        public void DrawString(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, Position, Color.Black * Alpha, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
