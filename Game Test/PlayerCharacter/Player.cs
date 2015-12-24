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
        //Playerstats player;

        private float SpeedScale; //Scales up the movementspeed

        private float sprSheetX;
        private int MaxSheetX;

        private float mHor, mVer;
        private string direction;
        private bool moveActive;

        //Slow down animation speed
        private const float Interval = 0.25f;

        //Collisionlayer
        Layer layer;

        private enum Movestate
        {
            none,
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

            mHor = 0.0f;
            mVer = 0.0f;
            direction = "down";
            
            sprite = new SprSheetImage("OptionsScreen/light");
            
            SpeedScale = 3.0f;
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
            if (InputManager.Instance.KeyDown(Keys.W))
            {
                moveActive = true;
                direction = "up";
                mVer = -1;
            }
            if (InputManager.Instance.KeyDown(Keys.S))
            {
                moveActive = true;
                direction = "down";
                mVer = 1;
            }
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                moveActive = true;
                direction = "left";
                mHor = -1;
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                moveActive = true;
                direction = "right";
                mHor = 1;
            }

            //Check if keys are released
            if (InputManager.Instance.KeyReleased(Keys.W) || InputManager.Instance.KeyReleased(Keys.A) || InputManager.Instance.KeyReleased(Keys.S) || InputManager.Instance.KeyReleased(Keys.D))
            {
                moveActive = false;
                sprSheetY = Movestate.none;
                sprite.SprSheetX = 0;
                mHor = 0;
                mVer = 0;
            }
            else if (moveActive)
                Move(mHor, mVer, direction, gameTime);

            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        private void Move(float dirX, float dirY, string direction, GameTime gameTime)
        {
            //Scale the movement
            dirX *= SpeedScale;
            dirY *= SpeedScale;

            //change sprSheetX and sprSheetY based on previous movement direction
            switch (direction)
            {
                case "up":
                    if (sprSheetY == Movestate.up)
                        sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                    else
                    { 
                        sprSheetY = Movestate.up;
                        sprSheetX = 0;
                    }
                    break;
                case "down":
                    if (sprSheetY == Movestate.down)
                        sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                    else
                    {
                        sprSheetY = Movestate.down;
                        sprSheetX = 0;
                    }
                    break;
                case "left":
                    if (sprSheetY == Movestate.left)
                        sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                    else
                    {
                        sprSheetY = Movestate.left;
                        sprSheetX = 0;
                    }
                    break;
                case "right":
                    if (sprSheetY == Movestate.right)
                        sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
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

            if (!(
                CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY)) &&
                CheckCollision(new Vector2(sprite.Position.X + 64 + dirX, sprite.Position.Y  + dirY)) &&
                CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + 64 + dirY)) &&
                CheckCollision(new Vector2(sprite.Position.X + 64 + dirX, sprite.Position.Y + 64 + dirY))
                ))

            /*if (sprite.Position.X + dirX >= 0 &&
                sprite.Position.X + dirX <= GameSettings.Instance.Dimensions.X - 64 &&
                sprite.Position.Y + dirY >= 0 &&
                sprite.Position.Y + dirY <= GameSettings.Instance.Dimensions.Y - 64)*/
            {
                sprite.Position = new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY); //Set new position
            }
            sprite.SprSheetX = (int)sprSheetX;
            sprite.SprSheetY = (int)sprSheetY;
        }

        private bool CheckCollision(Vector2 position)
        {
            float tilescale_x = GameSettings.Instance.Dimensions.X / 80, tilescale_y = GameSettings.Instance.Dimensions.Y / 60;

            int x = (int)(position.X / tilescale_x);
            int y = (int)(position.Y / tilescale_y);
            int TileID = layer.getTileID(x, y);
            if (TileID != 0)
                return true;
            else
                return false;
        }

        public void SendLayer(Layer layer)
        {
            this.layer = layer;
        }
    }
}
