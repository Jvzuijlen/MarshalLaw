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
        private Vector2 direction;
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
            direction = new Vector2(0, 1);
            
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
                direction.Y = -1;
                mVer = -1;
            }
            if (InputManager.Instance.KeyDown(Keys.S))
            {
                moveActive = true;
                direction.Y = 1;
                mVer = 1;
            }
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                moveActive = true;
                direction.X = -1;
                mHor = -1;
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                moveActive = true;
                direction.X = 1;
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
                direction = new Vector2(0, 0);
            }
            else if (moveActive)
                Move(mHor, mVer, direction, gameTime);

            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        private void Move(float dirX, float dirY, Vector2 direction, GameTime gameTime)
        {
            //Scale the movement
            dirX *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);
            dirY *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);

            bool CollisionY = CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), sprite.Position, (int)direction.Y),
            CollisionX = CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), sprite.Position, (int)direction.X + 1);
            
            //change sprSheetX and sprSheetY based on previous movement direction
            if (direction.Y == -1)//up
            {
                if (sprSheetY == Movestate.up)
                    sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                else if (direction.X == 0)
                {
                    sprSheetY = Movestate.up;
                    sprSheetX = 0;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.Y == 1)//down
            {
                if (sprSheetY == Movestate.down)
                    sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                else if(direction.X == 0)
                {
                    sprSheetY = Movestate.down;
                    sprSheetX = 0;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.X == -1)//left
            {
                if (sprSheetY == Movestate.left)
                    sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                else
                {
                    sprSheetY = Movestate.left;
                    sprSheetX = 0;
                }
                if (CollisionX)
                    dirX = 0;
            }
            if (direction.X == 1)//right
            {
                if (sprSheetY == Movestate.right)
                    sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;
                else
                {
                    sprSheetY = Movestate.right;
                    sprSheetX = 0;
                }
                if (CollisionX)
                    dirX = 0;
            }

            //Reset X at the final animation frame
            if (sprSheetX > MaxSheetX)
                sprSheetX = 0;
           
            for (int l = 1; l < layer.Length; l++)
                ChangeAlpha(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), l);
            sprite.Position = new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY); //Set new position

            sprite.SprSheetX = (int)sprSheetX;
            sprite.SprSheetY = (int)sprSheetY;
        }

        private bool CheckCollision(Vector2 PositionNew, Vector2 PositionOld, int direction)
        {
            float tilescale_x = GameSettings.Instance.Tilescale.X, tilescale_y = GameSettings.Instance.Tilescale.Y;
            
            int x = (int)((PositionOld.X + tilescale_x) / tilescale_x),
            y = (int)((PositionOld.Y + tilescale_y) / tilescale_y);

            switch (direction)
            {
                case -1://up
                    y--;
                    break;
                case 1://down
                    y++;
                    break;
                case 0://left
                    x--;
                    break;
                case 2://right
                    x++;
                    break;
            }

            Rectangle playerRect = new Rectangle(new Point((int)(PositionNew.X + 0.5 * tilescale_x), (int)(PositionNew.Y + tilescale_y)), new Point((int)tilescale_x, (int)(tilescale_y)));

            int TileID;
            
            TileID = layer[0].getTileID(x, y);
            Rectangle rect;
            if (TileID != 0)
            {
                rect = new Rectangle(x * (int)tilescale_x, y * (int)tilescale_y, (int)tilescale_x, (int)tilescale_y);
                if (rect.Intersects(playerRect))
                {
                    return true;
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
