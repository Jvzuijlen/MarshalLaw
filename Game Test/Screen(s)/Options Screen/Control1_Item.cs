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
        public cText Title;
        Control1_Field field;

        public int ControlID;
        public int Status;

        /// <summary>
        /// Contructor for a Control Item
        /// </summary>
        /// <param name="title">The title of the Item</param>
        /// <param name="controlID">Each control needs its own ID</param>
        /// <param name="status">This wil be the starting status</param>
        public Control1_Item(string title, int controlID, int status)
        {
            this.Title = new cText(title, "DryGood");
            this.field = new Control1_Field();
            this.Status = status;
            this.ControlID = controlID;
        }

        public void LoadContent()
        {
            Title.LoadContent();
        }

        public void UnloadContent()
        {
            Title.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Title.Update(gameTime);
            SetTitlePosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Title.DrawString(spriteBatch);
        }

        /// <summary>
        /// Set the titleposition, corresponding it's status.
        /// The title text wil be centered, this is nessecary because each title has a different width.
        /// </summary>
        public void SetTitlePosition()
        {
            //The scale is nessecary because if the window gets resized the position changes aswell.
            float scale = GameSettings.Instance.Dimensions.X / 1366;
            //The middle x coordiantes get calculated here, it takes the width of the text and the width of the control bar,
            //and divides those by 2 to calculate the middle
            float x_position = (((375 - 170) - (Title.SourceRect.Width / 2)) / 2);

            switch (Status)
            {
                case 0:
                    Title.Position = new Vector2((160 + x_position) * scale, 230 * scale);
                    break;
                case 1:                           
                    Title.Position = new Vector2((155 + x_position) * scale, 350 * scale);
                    break;
                case 2:

                    Title.Position = new Vector2((160 + x_position) * scale, 485 * scale);
                    break;
            }
        }
    }
}
