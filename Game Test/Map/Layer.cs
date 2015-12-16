using Microsoft.Xna.Framework;
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

        public Layer(string layerName, Vector2 mapDimensions)
        {
            this.layerName = layerName;
            tiles = new Tile[(int)mapDimensions.X,(int)mapDimensions.Y];
        }

        public void AddTile(int index_x, int index_y, int tileID)
        {
            tiles[index_x, index_y] = new Tile(tileID);
        }
    }
}
