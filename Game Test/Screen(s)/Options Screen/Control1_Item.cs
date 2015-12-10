using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Control1_Item
    {
        private int ItemID;
        public Vector2 Dimensions;
        static int nextItemID = 0;
        private Image arrow_left, arrow_right;
        private cText itemtitle;
        private cText itemsetting;
        int fieldID;

        FadeEffect fadeeffect;
        Vector2 textScale;
        float textPos_Y;
        float imagePos_Y;
        float x_scale;
        Vector2 imageScale;
        public bool IsSelected { get; set; }

        enum selection
        {
            title, arrowleft, arrowright
        }
        selection currentSelected;

        public Control1_Item(string itemname, string itemsetting, int fieldID)
        {
            this.fieldID = fieldID;
            ItemID = nextItemID++;
            this.itemtitle = new cText(itemname + ":", "DryGood");
            this.itemsetting = new cText(itemsetting, "DryGood");
            arrow_left = new Image("OptionsScreen/arrow_left");
            arrow_right = new Image("OptionsScreen/arrow_right");
            arrow_left.Color = Color.Black;
            arrow_right.Color = Color.Black;
            fadeeffect = new FadeEffect(1.5f, 1.0f, 0.3f);
            currentSelected = selection.title;
        }

        public void LoadContent()
        {
            textScale = new Vector2(GameSettings.Instance.Dimensions.X / (3200 / 1.2f), GameSettings.Instance.Dimensions.Y / (1800 / 1.2f));
            imageScale = new Vector2(GameSettings.Instance.Dimensions.X / 2732f, GameSettings.Instance.Dimensions.Y / 1536f);

            #region "Position the Text on the Y axis"
            textPos_Y = 400;
            imagePos_Y = 420;
            switch (ItemID)
            {
                case 0:
                    break;
                case 1:
                    textPos_Y += 110;
                    imagePos_Y += 110;
                    break;
                case 2:
                    textPos_Y += 220;
                    imagePos_Y += 220;
                    break;
                case 3:
                    textPos_Y += 330;
                    imagePos_Y += 330;
                    break;
                case 4:
                    textPos_Y += 440;
                    imagePos_Y += 440;
                    break;
                case 5:
                    textPos_Y += 550;
                    imagePos_Y += 550;
                    break;
                case 6:
                    textPos_Y += 660;
                    imagePos_Y += 660;
                    break;
                case 7:
                    textPos_Y += 770;
                    imagePos_Y += 770;
                    break;
                case 8:
                    textPos_Y += 880;
                    imagePos_Y += 880;
                    break;
                case 9:
                    textPos_Y += 990;
                    imagePos_Y += 990;
                    break;
            }

            textPos_Y = textPos_Y * (GameSettings.Instance.Dimensions.Y / 1920);
            imagePos_Y = imagePos_Y * (GameSettings.Instance.Dimensions.Y / 1920);
            #endregion

            #region "Load Content"
            itemtitle.LoadContent();
            itemsetting.LoadContent();

            arrow_right.LoadContent(
                        pos_X: 0,
                        pos_Y: imagePos_Y,
                        centered: false,
                        scale: imageScale
                        );
            arrow_left.LoadContent(
                        pos_X: 0,
                        pos_Y: imagePos_Y,
                        centered: false,
                        scale: imageScale
                        );
            #endregion

            itemtitle.Scale = textScale;
            itemsetting.Scale = textScale;


            x_scale = (GameSettings.Instance.Dimensions.X / 1920);


            float tempPosition = 610 * x_scale;

            itemtitle.Position = new Vector2(tempPosition, textPos_Y);

            tempPosition += (itemtitle.SourceRect.Width * textScale.X) + (10 * x_scale);

            arrow_left.Position = new Vector2(tempPosition, arrow_left.Position.Y);

            tempPosition += (arrow_left.SourceRect.Width / imageScale.X) + (10 * x_scale);

            itemsetting.Position = new Vector2(tempPosition, textPos_Y);

            tempPosition += (itemsetting.SourceRect.Width * textScale.X) + (10 * x_scale);

            arrow_right.Position = new Vector2(tempPosition, arrow_right.Position.Y);

        } 

        public void UnloadContent()
        {
            itemtitle.UnloadContent();
            itemsetting.UnloadContent();
            arrow_left.UnloadContent();
            arrow_right.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            itemtitle.Update(gameTime);
            itemsetting.Update(gameTime);
            arrow_left.Update(gameTime);
            arrow_right.Update(gameTime);

            itemtitle.Color = Color.Black;
            arrow_left.Color = Color.Black;
            arrow_right.Color = Color.Black;

            if(InputManager.Instance.KeyPressed(Keys.Right))
            {
                currentSelected++;
                if (currentSelected == selection.arrowright + 1)
                    currentSelected = selection.title;
            }

            if (InputManager.Instance.KeyPressed(Keys.Left))
            {
                currentSelected--;
                if (currentSelected == selection.title - 1)
                    currentSelected = selection.arrowright;
            }

            if (IsSelected)
            {
                var temp = fadeeffect.Update(gameTime);
                itemtitle.Alpha = temp;
                itemsetting.Alpha = temp;
                arrow_left.Alpha = temp;
                arrow_right.Alpha = temp;
                switch (currentSelected)
                {
                    case selection.title:
                        itemtitle.Color = Color.White;
                        break;
                    case selection.arrowleft:
                        arrow_left.Color = Color.White;
                        break;
                    case selection.arrowright:
                        arrow_right.Color = Color.White;
                        break;
                }
            }
            else
            {
                itemtitle.Alpha = 1.0f;
                itemsetting.Alpha = 1.0f;
                arrow_left.Alpha = 1.0f;
                arrow_right.Alpha = 1.0f;
                itemtitle.Color = Color.Black;
                arrow_left.Color = Color.Black;
                arrow_right.Color = Color.Black;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            itemtitle.DrawString(spriteBatch);
            itemsetting.DrawString(spriteBatch);
            arrow_left.Draw(spriteBatch);
            arrow_right.Draw(spriteBatch);
        }
    }
}
