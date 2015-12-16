using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Map
    {

        Vector2 mapDimensions;
        Vector2 tileDimensions;
        string orientation;

        MapLoader mapLoader = new MapLoader();
        List<Layer> Layers;

        public Map()
        {

        }
    }
}
