using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Control1_Field
    {
        private int status;
        private cText title;
        private int FieldID;
        public Vector2 Dimensions;
        static int nextFieldID = 0;
        public int selectedItem { get; set; }
        public bool IsActive { get; set; }
        public int currentActiveItem;
        int numberItems;

        public cText Title { get { return title; } }
        public int Status { get { return status; } set { status = value; } }



        /// <summary>
        /// Contructor for a Control Item
        /// </summary>
        /// <param name="">The title of the Item</param>
        public Control1_Field(string title, int numItems)
        {
            this.title = new cText(title, "DryGood");
            FieldID = nextFieldID++;
            this.status = 0;
            selectedItem = 0;
            IsActive = false;
            currentActiveItem = 0;
            numberItems = numItems;
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
            if (IsActive)
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                {
                    
                    currentActiveItem++;
                    if (currentActiveItem == numberItems)
                        currentActiveItem = 0;
                }
                if (InputManager.Instance.KeyPressed(Keys.Up))
                {
                    currentActiveItem--;
                    if (currentActiveItem == -1)
                        currentActiveItem = numberItems - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(status >= 1 && status <= 3)
                title.DrawString(spriteBatch);
        }

        public void SetTitlePosition()
        {
            //The scale is nessecary because if the window gets resized the position changes aswell.
            float scale = GameSettings.Instance.Dimensions.X / 1366;
            //The middle x coordiantes get calculated here, it takes the width of the text and the width of the control bar,
            //and divides those by 2 to calculate the middle
            float x_position = (((375 - 170) - (title.SourceRect.Width / 2)) / 2);

            switch (status)
            {
                case 1:
                    title.Position = new Vector2((160 + x_position) * scale, 225 * scale);
                    break;
                case 2:
                    title.Position = new Vector2((155 + x_position) * scale, 350 * scale);
                    break;
                case 3:

                    title.Position = new Vector2((160 + x_position) * scale, 475 * scale);
                    break;
            }
        }


        public void SetStatus(int currentActive)
        {
            if (currentActive == FieldID)
                status = 2;
            else if (currentActive - 1 == FieldID)
                status = 1;
            else if (currentActive - 2 == FieldID)
                status = 0;
            else if (currentActive + 1 == FieldID)
                status = 3;
            else if (currentActive + 2 == FieldID)
                status = 4;
            if (FieldID == 0 && currentActive == nextFieldID - 1)
                status = 3;
            if (FieldID == nextFieldID - 1 && currentActive == 0)
                status = 1;
        }
    }
}
