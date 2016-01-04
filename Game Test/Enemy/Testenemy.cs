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

        private Vector2 Position;
        private Vector2 PlayerPosition;

        private float SpeedScale; //Scales up the movementspeed

        private float sprSheetX;

        private Vector2 direction;

        //Slow down animation speed
        private const float Interval = 0.25f;

        //Collisionlayer and Tree layer(s)
        Layer[] layer;

        private PlayerEnums.Action sprSheetY { get; set; }
        private PlayerEnums.ActionState State { get; set; }
        public PlayerEnums.ActionState PlayerState { get; set; }
        private PlayerEnums.LookDirection lookDirection { get; set; }
        public PlayerEnums.LookDirection PlayerLookDirection { get; set; }

        private SprSheetImage sprite;

        private Weapon weapon;

        private bool knockback;
        private double knockbacktimer;
        private Vector2 knockbackdirection;

        int dir = 0;
        double duration = 0;

        public Testenemy(int X, int Y)
        {
            //TODO add playerstats
            //this.player = player;

            Position = new Vector2(X, Y);

            State = PlayerEnums.ActionState.None;
            PlayerState = PlayerEnums.ActionState.None;
            lookDirection = PlayerEnums.LookDirection.Down;
            sprSheetY = PlayerEnums.Action.WalkDown;
            sprSheetX = 0;

            direction = new Vector2(0, 1);

            sprite = new SprSheetImage("Character/red_orc");

            SpeedScale = 1.5f;

            weapon = new Weapon();
        }

        public void LoadContent()
        {
            sprite.LoadContent(Position.X, Position.Y, false, new Vector2(64 / (GameSettings.Instance.Tilescale.X * 2), 64 / (GameSettings.Instance.Tilescale.Y * 2)));
            weapon.LoadContent((int)Position.X, (int)Position.Y);
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
            weapon.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Vector2 temp = CheckHit();
            if (temp.X == 1)
            {
                //TODO
                //Lose health
                knockback = true;
                knockbacktimer = 0.2f;
                duration = 0;
                knockbackdirection.X = 0;
                sprSheetY = PlayerEnums.Action.Hit;
                sprSheetX = 4;
                knockbackdirection = temp;
            }

            if (knockback)
            {
                switch ((int)knockbackdirection.Y)
                {
                    case 1:
                        Move(direction = new Vector2(0, -1), gameTime);
                        break;
                    case 2:
                        Move(direction = new Vector2(-1, 0), gameTime);
                        break;
                    case 3:
                        Move(direction = new Vector2(0, 1), gameTime);
                        break;
                    case 4:
                        Move(direction = new Vector2(1, 0), gameTime);
                        break;
                }

                knockbacktimer -= gameTime.ElapsedGameTime.TotalSeconds;

                if (knockbacktimer <= 0)
                {
                    knockback = false;
                    knockbackdirection.Y = 0;
                }
                else
                {
                    SetAnimationFrame();
                    sprite.Update(gameTime);
                    weapon.Update(gameTime);
                    return;
                }
            }

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
                    State = PlayerEnums.ActionState.None;
                    sprSheetY = PlayerEnums.Action.None;
                    sprSheetX = 0;
                    sprite.SprSheetX = 0;
                    direction = new Vector2(0, 0);
                    break;
                case 1://up
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.Up;
                    direction = new Vector2(0, -1);
                    break;
                case 2://left
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.left;
                    direction = new Vector2(-1, 0);
                    break;
                case 3://down
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.Down;
                    direction = new Vector2(0, 1);
                    break;
                case 4://right
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.Right;
                    direction = new Vector2(1, 0);
                    break;
                case 5://left up
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.left;
                    direction = new Vector2(-1, -1);
                    break;
                case 6://left down
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.left;
                    direction = new Vector2(-1, 1);
                    break;
                case 7://right up
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.Right;
                    direction = new Vector2(1, -1);
                    break;
                case 8://right down
                    State = PlayerEnums.ActionState.Walk;
                    lookDirection = PlayerEnums.LookDirection.Right;
                    direction = new Vector2(1, 1);
                    break;
            }

            //Move(direction, gameTime);

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
                case PlayerEnums.LookDirection.Up:
                    if (sprSheetY == PlayerEnums.Action.SpearUp)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = PlayerEnums.Action.SpearUp;
                    }
                    break;
                case PlayerEnums.LookDirection.left:
                    if (sprSheetY == PlayerEnums.Action.SpearLeft)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = PlayerEnums.Action.SpearLeft;
                    }
                    break;
                case PlayerEnums.LookDirection.Down:
                    if (sprSheetY == PlayerEnums.Action.SpearDown)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = PlayerEnums.Action.SpearDown;
                    }
                    break;
                case PlayerEnums.LookDirection.Right:
                    if (sprSheetY == PlayerEnums.Action.SpearRight)
                        UpdateAnimationFrame(gameTime);
                    else
                    {
                        sprSheetY = PlayerEnums.Action.SpearRight;
                    }
                    break;
            }
        }

        private void Move(Vector2 direction, GameTime gameTime)
        {
            float dirX = direction.X,
            dirY = direction.Y;
            

            //Scale the movement
            dirX *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);
            dirY *= SpeedScale * (32 / GameSettings.Instance.Tilescale.X);

            bool CollisionY = CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), sprite.Position, (int)direction.Y),
            CollisionX = CheckCollision(new Vector2(sprite.Position.X + dirX, sprite.Position.Y + dirY), sprite.Position, (int)direction.X + 1);

            //change sprSheetX and sprSheetY based on previous movement direction
            if (direction.Y == -1)//up
            {
                if (sprSheetY == PlayerEnums.Action.WalkUp)
                    UpdateAnimationFrame(gameTime);
                else if (direction.X == 0)
                {
                    sprSheetY = PlayerEnums.Action.WalkUp;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.Y == 1)//down
            {
                if (sprSheetY == PlayerEnums.Action.WalkDown)
                    UpdateAnimationFrame(gameTime);
                else if (direction.X == 0)
                {
                    sprSheetY = PlayerEnums.Action.WalkDown;
                }
                if (CollisionY)
                    dirY = 0;
            }
            if (direction.X == -1)//left
            {
                if (sprSheetY == PlayerEnums.Action.WalkLeft)
                    UpdateAnimationFrame(gameTime);
                else
                {
                    sprSheetY = PlayerEnums.Action.WalkLeft;
                }
                if (CollisionX)
                    dirX = 0;
            }
            if (direction.X == 1)//right
            {
                if (sprSheetY == PlayerEnums.Action.WalkRight)
                    UpdateAnimationFrame(gameTime);
                else
                {
                    sprSheetY = PlayerEnums.Action.WalkRight;
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

            Rectangle Enemyrect = new Rectangle(new Point((int)(PositionNew.X + 0.5 * tilescale_x), (int)(PositionNew.Y + tilescale_y)), new Point((int)tilescale_x, (int)(tilescale_y)));

            int TileID;

            TileID = layer[0].getTileID(x, y);
            Rectangle rect;
            if (TileID != 0)
            {
                rect = new Rectangle(x * (int)tilescale_x, y * (int)tilescale_y, (int)tilescale_x, (int)tilescale_y);
                if (rect.Intersects(Enemyrect))
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

            Rectangle Enemyrect = new Rectangle(new Point((int)(position.X + 0.5 * tilescale_x), (int)(position.Y + tilescale_y)), new Point((int)tilescale_x, (int)(tilescale_y)));

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
                        if (rect.Intersects(Enemyrect))
                            layer[number].ChangeTileAlpha(j, i, 0.5f);
                        else if (layer[number].GetTileAlpha(j, i) != 1.0f)
                            layer[number].ChangeTileAlpha(j, i, 1.0f);
                    }
                }
            }
        }

        public void SendLayer(Layer layer, int number)
        {
            this.layer[number] = layer;
        }

        public void SetLayernumber(int number)
        {
            layer = new Layer[number];
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            if (!(knockback))
                sprSheetX += (float)gameTime.ElapsedGameTime.TotalMilliseconds / gameTime.ElapsedGameTime.Milliseconds * Interval;

            //Reset X at the final animation frame
            if ((int)sprSheetX >= (int)State)
                sprSheetX = 0;
        }

        private void SetAnimationFrame()
        {
            sprite.SprSheetX = (int)sprSheetX;
            weapon.SprSheetX = (int)sprSheetX;

            if (sprSheetY != PlayerEnums.Action.None)
            {
                sprite.SprSheetY = (int)sprSheetY;
                weapon.SprSheetY = (int)sprSheetY;
            }
        }

        public void SendPosition(Vector2 position)
        {
            PlayerPosition = position;
        }

        public Vector2 CheckHit()
        {
            Vector2 returnvalue = new Vector2(0, 0);
            Rectangle Enemyrect = new Rectangle(new Point((int)(sprite.Position.X + 0.5 * GameSettings.Instance.Tilescale.X), (int)(sprite.Position.Y + GameSettings.Instance.Tilescale.Y)), new Point((int)GameSettings.Instance.Tilescale.X, (int)(GameSettings.Instance.Tilescale.Y))),
                Playerrect = new Rectangle(new Point((int)(PlayerPosition.X + 0.5 * GameSettings.Instance.Tilescale.X), (int)(PlayerPosition.Y + GameSettings.Instance.Tilescale.Y)), new Point((int)GameSettings.Instance.Tilescale.X, (int)(GameSettings.Instance.Tilescale.Y)));

            if (PlayerState == PlayerEnums.ActionState.Thrust)
            {
                switch (PlayerLookDirection)
                {
                    case PlayerEnums.LookDirection.Up:
                        if (Enemyrect.Intersects(Playerrect))
                            returnvalue = new Vector2(1, 1);
                        break;
                    case PlayerEnums.LookDirection.left:
                        if (Enemyrect.Intersects(Playerrect))
                            returnvalue = new Vector2(1, 2);
                        break;
                    case PlayerEnums.LookDirection.Down:
                        if (Enemyrect.Intersects(Playerrect))
                            returnvalue = new Vector2(1, 3);
                        break;
                    case PlayerEnums.LookDirection.Right:
                        if (Enemyrect.Intersects(Playerrect))
                            returnvalue = new Vector2(1, 4);
                        break;
                }
            }
            return returnvalue;
        }
    }
}
