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

        private bool PlayerActive;

        //Property's
        public Vector2 mapDimensions { get; private set; }
        public int NumberLayers { get; private set; }

        int layer_player_num = 0;

        public Map(string mapName)
        {
            mapLoader.LoadMap(mapName);

            Layers = mapLoader.GetLayers();
            mapDimensions = mapLoader.GetMapDimensions();
            NumberLayers = mapLoader.GetNumLayers();
            spriteSheets = mapLoader.GetSpritesheetList();



            player = new Player();

            for (int l = 0; l < Layers.Count; l++)
            {
                if (Layers[l].Layername == "Player")
                {
                    layer_player_num = l;
                }
            }

            int temp = 0;

            player.SetLayernumber(NumberLayers - layer_player_num);
            GetLayer("Collision", temp++);

            for (int l = layer_player_num; l < Layers.Count - 1; l++)
            {
                GetLayer(Layers[l].Layername, temp++);
            }

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
                    for (int l = layer_player_num; l < Layers.Count; l++)
                    {
                        if (Layers[l].Layername != "Collision" && Layers[l].Layername != "Player")
                            Layers[l].DrawTile(spriteBatch, x, y);
                        if (Layers[l].Layername == "Player" && PlayerActive == false)
                        {
                            player.Draw(spriteBatch);
                            PlayerActive = true;
                        }
                    }
                }
            }
            //player.Draw(spriteBatch);
            PlayerActive = false;
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < mapDimensions.Y; y++)
            {
                for (int x = 0; x < mapDimensions.X; x++)
                {
                    for (int l = 0; l < layer_player_num; l++)
                    {
                            Layers[l].DrawTile(spriteBatch, x, y);
                    }
                }
            }
        }

        public void GetLayer(string Name, int number)
        {
            for (int l = 0; l < Layers.Count; l++)
            {
                if (Layers[l].Layername == Name)
                {
                    player.SendLayer(Layers[l], number, NumberLayers - layer_player_num);
                }
            }
        }
    }
}
