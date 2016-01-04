using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Game_Test
{
    public class Testenemy
    {
        //Playerstats player;

        private float SpeedScale; //Scales up the movementspeed

        private float sprSheetX;
        
        private Vector2 direction;

        //Slow down animation speed
        private const float Interval = 0.25f;

        //Collisionlayer and Tree layer(s)
        Layer[] layer;

        /// <summary>
        /// Spell: 0-3:
        /// Spear: 4-7
        /// Walk: 8-11
        /// Slash: 12-15
        /// Shoot: 16-19
        /// </summary>
        private enum Action
        {
            SpellUp,
            SpellLeft,
            SpellDown,
            SpellRight,
            SpearUp,
            SpearLeft,
            SpearDown,
            SpearRight,
            WalkUp,
            WalkLeft,
            WalkDown,
            WalkRight,
            SlashUp,
            SlashLeft,
            SlashDown,
            SlashRight,
            ShootUp,
            ShootLeft,
            ShootDown,
            ShootRight,
            None
        };
        Action sprSheetY;

        private enum ActionState
        {
            None,
            Spell = 7,
            Thrust = 8,
            Walk = 9,
            Slash = 6,
            Shoot = 13
        };
        ActionState State, PlayerState;

        private enum LookDirection
        {
            Up,
            left,
            Down,
            Right
        };
        LookDirection lookDirection, PlayerLookDirection;

        private SprSheetImage sprite;

        private Weapon weapon;
        
        int dir = 0;
        double duration = 0;

        public Testenemy()
        {
            //TODO add playerstats
            //this.player = player;
            State = ActionState.None;
            PlayerState = ActionState.None;
            lookDirection = LookDirection.Down;
            sprSheetY = Action.WalkDown;
            sprSheetX = 0;

            direction = new Vector2(0, 1);

            sprite = new SprSheetImage("Character/red_orc");

            SpeedScale = 1.5f;

            weapon = new Weapon();
        }

        public void LoadContent(int X, int Y)
        {
            sprite.LoadContent(X, Y, false, new Vector2(64 / (GameSettings.Instance.Tilescale.X * 2), 64 / (GameSettings.Instance.Tilescale.Y * 2)));
            weapon.LoadContent(X, Y);
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
            weapon.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Random rnd = new Random(), rnd2 = new Random();

            if (duration <= 0)
            {
                duration = rnd2.Next(1, 3);
                dir = rnd.Next(9);
            }
            else
                duration -= gameTime.ElapsedGameTime.TotalSeconds;

            switch (dir)
            {
                case 0://no movement
                    State = ActionState.None;
                    sprSheetY = Action.None;
                    sprSheetX = 0;
                    sprite.SprSheetX = 0;
                    direction = new Vector2(0, 0);
                    break;
                case 1://up
                    State = ActionState.Walk;
                    lookDirection = LookDirection.Up;
                    direction = new Vector2(0, -1);
                    break;
                case 2://left
                    State = ActionState.Walk;
                    lookDirection = LookDirection.left;
                    direction = new Vector2(-1, 0);
                    break;
                case 3://down
                    State = ActionState.Walk;
                    lookDirection = LookDirection.Down;
                    direction = new Vector2(0, 1);
                    break;
                case 4://right
                    State = ActionState.Walk;
                    lookDirection = LookDirection.Right;
                    direction = new Vector2(1, 0);
                    break;
                case 5://left up
                    State = ActionState.Walk;
                    lookDirection = LookDirection.left;
                    direction = new Vector2(-1, -1);
                    break;
                case 6://left down
                    State = ActionState.Walk;
                    lookDirection = LookDirection.left;
                    direction = new Vector2(-1, 1);
                    break;
                case 7://right up
                    State = ActionState.Walk;
                    lookDirection = LookDirection.Right;
                    direction = new Vector2(1, -1);
                    break;
                case 8://right down
                    State = ActionState.Walk;
                    lookDirection = LookDirection.Right;
                    direction = new Vector2(1, 1);
                    break;
            }

            Move(direction.X, direction.Y, direction, gameTime);

            SetAnimationFrame();
            sprite.Update(gameTime);
            weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            weapon.Draw(spriteBatch);
        }

        private void Attack(GameTime gameTime)
        {
            switch (lookDirection)
            {
                case LookDirection.Up:
                    if (sprSheetY == Action.SpearUp)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = Action.SpearUp;
                    }
                    break;
                case LookDirection.left:
                    if (sprSheetY == Action.SpearLeft)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = Action.SpearLeft;
                    }
                    break;
                case LookDirection.Down:
                    if (sprSheetY == Action.SpearDown)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = Action.SpearDown;
                    }
                    break;
                case LookDirection.Right:
                    if (sprSheetY == Action.SpearRight)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = Action.SpearRight;
                    }
                    break;
            }
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
                if (sprSheetY == Action.WalkUp)
                    UpdateAnimationFrame(gameTime);
                else if (direction.X == 0)
                {
                    sprSheetY = Action.WalkUp;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.Y == 1)//down
            {
                if (sprSheetY == Action.WalkDown)
                    UpdateAnimationFrame(gameTime);
                else if (direction.X == 0)
                {
                    sprSheetY = Action.WalkDown;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.X == -1)//left
            {
                if (sprSheetY == Action.WalkLeft)
                    UpdateAnimationFrame(gameTime);
                else
                {
                    sprSheetY = Action.WalkLeft;
                }
                if (CollisionX)
                    dirX = 0;
            }
            if (direction.X == 1)//right
            {
                if (sprSheetY == Action.WalkRight)
                    UpdateAnimationFrame(gameTime);
                else
                {
                    sprSheetY = Action.WalkRight;
                }
                if (CollisionX)
                    dirX = 0;
            }

            for (int l = 1; l < layer.Length; l++)
                ChangeAlpha(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), l);
            sprite.Position = new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY); //Set new position
            weapon.setPosition(new Vector2(weapon.getPosition().X + dirX, weapon.getPosition().Y + dirY)); //Move weapon with you
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

        public void SendLayer(Layer[] layer)
        {
            this.layer = layer;
        }

        public void SetLayernumber(int number)
        {
            layer = new Layer[number];
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;

            //Reset X at the final animation frame
            if ((int)sprSheetX >= (int)State)
                sprSheetX = 0;
        }

        private void SetAnimationFrame()
        {
            sprite.SprSheetX = (int)sprSheetX;
            weapon.SprSheetX = (int)sprSheetX;

            if (sprSheetY != Action.None)
            {
                sprite.SprSheetY = (int)sprSheetY;
                weapon.SprSheetY = (int)sprSheetY;
            }
        }
    }
}
