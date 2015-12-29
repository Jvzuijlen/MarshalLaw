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

        //Collisionlayer and Tree layer(s)
        Layer[] layer;

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
            
            SpeedScale = 1.5f;
        }

        public void LoadContent(int X, int Y)
        {
            sprite.LoadContent(X, Y, false, new Vector2(64 / (GameSettings.Instance.Tilescale.X * 2), 64 / (GameSettings.Instance.Tilescale.Y * 2)));
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
            dirX *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);
            dirY *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);

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
                CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY))
                ))

            /*if (sprite.Position.X + dirX >= 0 &&
                sprite.Position.X + dirX <= GameSettings.Instance.Dimensions.X - 64 &&
                sprite.Position.Y + dirY >= 0 &&
                sprite.Position.Y + dirY <= GameSettings.Instance.Dimensions.Y - 64)*/
            {
                for (int l = 1; l < layer.Length; l++)
                    ChangeAlpha(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), l);
                sprite.Position = new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY); //Set new position
            }


            sprite.SprSheetX = (int)sprSheetX;
            sprite.SprSheetY = (int)sprSheetY;
        }

        private bool CheckCollision(Vector2 position)
        {
            float tilescale_x = GameSettings.Instance.Tilescale.X, tilescale_y = GameSettings.Instance.Tilescale.Y;

            int x1 = (int)(position.X / tilescale_x),
            y1 = (int)(position.Y / tilescale_y),
            x2 = (int)((position.X + 2 * tilescale_x) / tilescale_x),
            y2 = (int)((position.Y + 2 * tilescale_y) / tilescale_y);

            Rectangle playerRect = new Rectangle(new Point((int)(position.X) + 12, (int)(position.Y + tilescale_y)), new Point((int)tilescale_x, (int)(tilescale_y)));

            int TileID;

            for (int i = y1; i < y2 + 1; i++)
            {
                for (int j = x1; j < x2 + 1; j++)
                {
                    TileID = layer[0].getTileID(j, i);
                    Rectangle rect;
                    if (TileID != 0)
                    {
                        rect = new Rectangle(j * (int)tilescale_x, i * (int)tilescale_y, (int)tilescale_x, (int)tilescale_y);
                        if (rect.Intersects(playerRect))
                            return true;
                    }
                }
            }
            return false;
        }

        private void ChangeAlpha(Vector2 position, int number)
        {
            float tilescale_x = GameSettings.Instance.Tilescale.X, tilescale_y = GameSettings.Instance.Tilescale.Y;

            int x1 = (int)(position.X / tilescale_x),
            y1 = (int)(position.Y / tilescale_y),
            x2 = (int)((position.X + 2 * tilescale_x) / tilescale_x),
            y2 = (int)((position.Y + 2 * tilescale_y) / tilescale_y);

            Rectangle playerRect = new Rectangle(new Point((int)(position.X) + 12, (int)(position.Y + 10)), new Point((int)tilescale_x, (int)(tilescale_y + 12)));

            int TileID;

            for (int i = y1; i < y2 + 1; i++)
            {
                for (int j = x1; j < x2 + 1; j++)
                {
                    TileID = layer[number].getTileID(j, i);
                    Rectangle rect;
                    if (TileID != 0)
                    {
                        rect = new Rectangle(j * (int)tilescale_x, i * (int)tilescale_y, (int)tilescale_x, (int)tilescale_y);
                        if (rect.Intersects(playerRect))
                            layer[number].ChangeTileAlpha(j, i, 0.5f);
                        else if (layer[number].GetTileAlpha(j, i) != 1.0f)
                            layer[number].ChangeTileAlpha(j, i, 1.0f);
                    }
                }
            }
        }

        public void SendLayer(Layer layer, int number, int numlayers)
        {
            this.layer[number] = layer;
        }

        public void SetLayernumber(int number)
        {
            layer = new Layer[number];
        }
    }
}
