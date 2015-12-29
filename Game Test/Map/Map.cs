using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Map
    {
        //Fields
        //Vector2 tileDimensions;
        //string orientation;
        MapLoader mapLoader = new MapLoader();
        public List<Layer> Layers = new List<Layer>();
        List<string> spriteSheets = new List<string>();

        Player player;

        //Property's
        public Vector2 mapDimensions { get; private set; }
        public int NumberLayers { get; private set; }

        public Map(string mapName)
        {
            mapLoader.LoadMap(mapName);

            Layers = mapLoader.GetLayers();
            mapDimensions = mapLoader.GetMapDimensions();
            NumberLayers = mapLoader.GetNumLayers();
            spriteSheets = mapLoader.GetSpritesheetList();

            player = new Player();
            GetLayer("Collision", 0);
            GetLayer("Tree", 1);

            foreach (var layer in Layers)
            {
                layer.GiveSpriteSheetList(spriteSheets);
            }

        }

        public virtual void LoadContent()
        {
            player.LoadContent(32, 32);

            foreach (var layer in Layers)
            {
                layer.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            player.UnloadContent();

            foreach (var layer in Layers)
            {
                layer.UnloadContent();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            //foreach (var layer in Layers)
            //{
                //layer.Update(gameTime);
            //}
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < mapDimensions.Y; y++)
            {
                for (int x = 0; x < mapDimensions.X; x++)
                {
                    for (int l = 0; l < Layers.Count; l++)
                    {
                        if (Layers[l].Layername != "Collision")
                            Layers[l].DrawTile(spriteBatch, x, y);
                    }
                }
            }
            player.Draw(spriteBatch);
        }

        public void GetLayer(string Name, int number)
        {
            for (int l = 0; l < Layers.Count; l++)
            {
                if (Layers[l].Layername == Name)
                {
                    player.SendLayer(Layers[l], number);
                }
            }
        }
    }
}
