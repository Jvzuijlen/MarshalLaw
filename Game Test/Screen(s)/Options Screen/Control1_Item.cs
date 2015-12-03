using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    class Control1_Item
    {
        cText title;
        Control1_Field field;

        public int ControlID;
        public int Status;

        public Control1_Item(string title, int controlID, int status)
        {
            this.title = new cText(title, "DryGood");
            this.field = new Control1_Field();
            this.Status = status;
            this.ControlID = controlID;
        }

        public void LoadContent()
        {
            title.LoadContent();
        }

        public void UnloadContent()
        {
            title.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            title.Update(gameTime);
            SetTitlePosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            title.DrawString(spriteBatch);
        }

        public void SetTitlePosition()
        {
             
            float scale = GameSettings.Instance.Dimensions.X / 1366;
            float x_position = (((375 - 170) - (title.SourceRect.Width / 2)) / 2);

            switch (Status)
            {
                case 0:
                    title.Position = new Vector2((160 + x_position) * scale, 230 * scale);
                    //title.Position = new Vector2(170, 357);
                    break;
                case 1:
                    //title.Position = new Vector2(375, 357);                            
                    title.Position = new Vector2((155 + x_position) * scale, 350 * scale);
                    //title.Position = new Vector2(242, 357);
                    break;
                case 2:

                    title.Position = new Vector2((160 + x_position) * scale, 485 * scale);
                    break;
            }
        }
    }
}
