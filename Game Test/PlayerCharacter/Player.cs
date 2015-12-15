using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Game_Test
{
    public class Player
    {
        Playerstats player;

        private int scale; //Scales up the movementspeed

        private int sprSheetX;
        private int MaxSheetX;

        float MoveScale;

        private enum Movestate
        {
            up = 8,
            left = 9,
            down = 10,
            right = 11
        };

        Movestate sprSheetY;

        private SprSheetImage sprite;

        public Player()
        {
            //TODO add playerstats
            //this.player = player;

            sprSheetY = Movestate.down;
            sprSheetX = 0;
            MaxSheetX = 6;

            MoveScale = 2.0f;
            
            sprite = new SprSheetImage("OptionsScreen/light");
            
            scale = 10;
        }

        public void LoadContent(int X, int Y)
        {
            sprite.LoadContent(X, Y, false, Vector2.One);
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            //Check if keys are pressed
            if (InputManager.Instance.KeyDown(Keys.Up))
            {
                Move(0f, -1f, "up");
            }
            else if (InputManager.Instance.KeyDown(Keys.Left))
            {
                Move(-1f, 0f, "left");
            }
            else if (InputManager.Instance.KeyDown(Keys.Down))
            {
                Move(0f, 1f, "down");
            }
            else if (InputManager.Instance.KeyDown(Keys.Right))
            {
                Move(1f, 0f, "right");
            }

            //Check if keys are released
            else if (InputManager.Instance.KeyReleased(Keys.Up) || InputManager.Instance.KeyReleased(Keys.Left) || InputManager.Instance.KeyReleased(Keys.Down) || InputManager.Instance.KeyReleased(Keys.Right))
                sprite.SprSheetX = 0;
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public void Move(float dirX, float dirY, string direction)
        {
            //Scale the movement
            dirX /= MoveScale;
            dirY /= MoveScale;

            //change sprSheetX and sprSheetY based on previous movement direction
            switch (direction)
            {
                case "up":
                    if (sprSheetY == Movestate.up)
                        sprSheetX++;
                    else
                    {
                        sprSheetY = Movestate.up;
                        sprSheetX = 0;
                    }
                    break;
                case "left":
                    if (sprSheetY == Movestate.left)
                        sprSheetX++;
                    else
                    {
                        sprSheetY = Movestate.left;
                        sprSheetX = 0;
                    }
                    break;
                case "down":
                    if (sprSheetY == Movestate.down)
                        sprSheetX++;
                    else
                    {
                        sprSheetY = Movestate.down;
                        sprSheetX = 0;
                    }
                    break;
                case "right":
                    if (sprSheetY == Movestate.right)
                        sprSheetX++;
                    else
                    {
                        sprSheetY = Movestate.right;
                        sprSheetX = 0;
                    }
                    break;
            }

            //Reset X at the final animation frame
            if (sprSheetX > MaxSheetX)
                sprSheetX = 0;

            //
            sprite.Position = new Vector2(sprite.Position.X + dirX * scale, sprite.Position.Y + dirY * scale);
            sprite.SprSheetX = sprSheetX;
            sprite.SprSheetY = (int)sprSheetY;
        }
    }
}
