using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class MapLoader
    {
        Vector2 mapDimensions;
        Vector2 tileDimensions;
        string orientation;

        List<Layer> layers;

        int numberOfLayers;

        enum state
        {
            header, layer, end
        }

        enum headerstate
        {
            header, width, height, tilewidth, tileheight, orientation, tileset
        }

        state currentState;
        headerstate currentHeaderState;
        bool firstLayer;

        public MapLoader()
        {
            firstLayer = true;
            layers = new List<Layer>();
        }

        public String LoadMap(string filename)
        {

            var path = @"Content\" + filename + ".txt";

            String line = "";
            StreamReader sr = new StreamReader(path);

            string text = "";
            String buffer = "";

            while (true)
            {
                switch (currentState)
                {
                    case state.header:
                        #region "Read Header"
                        switch (currentHeaderState)
                        {
                            case headerstate.header:
                                buffer = sr.ReadLine();
                                if (buffer != "[header]")
                                {
                                    return "Error1";
                                }
                                currentHeaderState++;
                                break;
                            case headerstate.width:
                                buffer = sr.ReadLine();
                                buffer = buffer.Remove(0, 6);
                                if (Convert.ToInt32(buffer) <= 0 )
                                {
                                    return "Error2";
                                }
                                mapDimensions.X = Convert.ToInt32(buffer);

                                currentHeaderState++;
                                break;
                            case headerstate.height:
                                buffer = sr.ReadLine();
                                buffer = buffer.Remove(0, 7);
                                if (Convert.ToInt32(buffer) <= 0)
                                {
                                    return "Error3";
                                }
                                mapDimensions.Y = Convert.ToInt32(buffer);

                                currentHeaderState++;
                                break;
                            case headerstate.tilewidth:
                                buffer = sr.ReadLine();
                                buffer = buffer.Remove(0, 10);
                                if (Convert.ToInt32(buffer) != 32)
                                {
                                    return "Error4";
                                }
                                tileDimensions.X = Convert.ToInt32(buffer);

                                currentHeaderState++;
                                break;
                            case headerstate.tileheight:
                                buffer = sr.ReadLine();
                                buffer = buffer.Remove(0, 11);
                                if (Convert.ToInt32(buffer) != 32)
                                {
                                    return "Error5";
                                }
                                tileDimensions.Y = Convert.ToInt32(buffer);

                                currentHeaderState++;
                                break;
                            case headerstate.orientation:
                                buffer = sr.ReadLine();
                                buffer = buffer.Remove(0, 12);
                                if (buffer != "orthogonal")
                                {
                                    return "Error6";
                                }
                                orientation = buffer;

                                currentHeaderState++;
                                break;
                            case headerstate.tileset:
                                buffer = sr.ReadLine();
                                if (buffer == "[layer]")
                                {
                                    currentState++;
                                }
                                break;
                        }
                        #endregion
                        break;
                    case state.layer:
                        #region "Read Layer"

                        if (firstLayer && buffer == "[layer]")
                        {
                            firstLayer = false;
                        }
                        else 
                        {
                            sr.ReadLine();
                        }

                        buffer = sr.ReadLine();
                        buffer = buffer.Remove(0, 5);

                        Layer tempLayer = new Layer(buffer, mapDimensions);

                        sr.ReadLine();
                        bool check = false;

                        for (int y = 0; y < mapDimensions.Y; y++)
                        {
                            for (int x = 0; x < mapDimensions.X; x++)
                            {
                                string temp_tileid = "";
                                do
                                {
                                    buffer = "";
                                    buffer += (char)sr.Read();
                                    if (x == mapDimensions.X - 1 && y == mapDimensions.Y - 1)
                                    {
                                        sr.Read();
                                        sr.Read();
                                        check = true;
                                        temp_tileid += buffer;
                                    }
                                    else if (buffer != ",")
                                    {
                                        temp_tileid += buffer;
                                    }
                                } while (buffer != "," && check == false);

                                if (temp_tileid.Contains("\r\n"))
                                {
                                    temp_tileid = temp_tileid.Remove(0, 2);
                                }

                                tempLayer.AddTile(x, y, Convert.ToInt32(temp_tileid));

                            }
                        }
                        //Lege regel nog effe lezen zodat de pointer verzet wordt
                        sr.ReadLine();

                        layers.Add(tempLayer);
                        numberOfLayers++;

                        if (sr.EndOfStream)
                        {
                            return "End of Stream";
                        }
                        #endregion
                        break;
                }
            }
        }
    }
    
}
