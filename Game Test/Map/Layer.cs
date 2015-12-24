using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Layer
    {
        Tile[,] tiles;
        
        string layerName;

        List<string> spriteSheets = new List<string>();

        List<Texture2D> textures = new List<Texture2D>();

        ContentManager content;

        public Layer(string layerName, Vector2 mapDimensions)
        {
            this.layerName = layerName;
            tiles = new Tile[(int)mapDimensions.X,(int)mapDimensions.Y];
        }

        public void AddTile(int index_x, int index_y, int tileID)
        {
            tiles[index_x, index_y] = new Tile(tileID);
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            //this.path = "SpriteSheets/";

            foreach (var tile in tiles)
            {
                tile.LoadContent(GetTileSheet(tile.TileID));
            }

            for (int i = 0; i < this.spriteSheets.Count; i++)
            {
                Texture2D temp_texture = content.Load<Texture2D>("SpriteSheets/" + spriteSheets[i]);
                textures.Add(temp_texture);
            }
        }

        public void UnloadContent()
        {
            foreach (var tile in tiles)
            {
                tile.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            //foreach (var tile in tiles)
            //{
                //tile.Update(gameTime);
            //}
        }

        public void DrawTile(SpriteBatch spriteBatch, int x, int y)
        {
            int tile_scale = 24;

            if (tiles[x, y].TileID != 0)
            {
                var random = spriteSheets.IndexOf(tiles[x, y].TextureName);

                Vector2 tileOnSheetPosition = SetTileOnSheetPosition(tiles[x, y].TileID);
                Rectangle Source = new Rectangle((int)tileOnSheetPosition.X * 32, (int)tileOnSheetPosition.Y * 32, 32, 32);

                spriteBatch.Draw(textures[spriteSheets.IndexOf(tiles[x, y].TextureName)], new Vector2((int)x * tile_scale, (int)y * tile_scale), Source, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0.5f);
            }
        }

        public virtual void GiveSpriteSheetList(List<string> spriteSheets)
        {
            this.spriteSheets = spriteSheets;
        }

        public string GetTileSheet(int tileID)
        {
            int temp_int = 0;

            if (tileID > 0 && tileID <= 483)
            {
                temp_int = 0;
            }
            else if(tileID > 483 && tileID <= 653)
            {
                temp_int = 1;
            }
            return spriteSheets[temp_int];
        }

        public Vector2 SetTileOnSheetPosition(int tileID)
        {
            if (tileID > 0 && tileID <= 483)
            {
                int temp_tileID = 1;
                Vector2 tileSheetDimension = new Vector2( 21, 23); //672 / 32,  736 / 32
                for (int y = 0; y < tileSheetDimension.Y; y++)
                {
                    for (int x = 0; x < tileSheetDimension.X; x++)
                    {
                        if (tileID == temp_tileID)
                        {
                            return new Vector2(x, y);
                        }
                        else
                            temp_tileID++;
                    }
                }
            }
            else if (tileID > 483 && tileID <= 653)
            {
                int temp_tileID = 1;
                Vector2 tileSheetDimension = new Vector2(13, 13);
                for (int y = 0; y < tileSheetDimension.Y; y++)
                {
                    for (int x = 0; x < tileSheetDimension.X; x++)
                    {
                        if (tileID - 483 == temp_tileID)
                        {
                            return new Vector2(x, y);
                        }
                        else
                            temp_tileID++;
                    }
                }
            }

            return new Vector2(0, 1);
        }
    }
}
