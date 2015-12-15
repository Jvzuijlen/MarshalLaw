using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Options_Members
    {
        public enum Actions
        {
            changeResolution,
        };

        public void ExcuteAction(Actions action, int index)
        {
            switch(action)
            {
                case Actions.changeResolution:

                    break;
            }
        }


        List<string> Resolutions = new List<string>();

        public void Create_Lists()
        {
            #region "Resolutions"
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                string temp;
                if (mode.AspectRatio >= 1.7f && mode.AspectRatio <= 1.8f)
                {
                    temp = mode.Width.ToString() + "x" + mode.Height;
                    Resolutions.Add(temp);
                }
            }
            #endregion
        }

        public List<string> GetList(int ID)
        {
            switch(ID)
            {
                case 1:
                    return Resolutions;

            }
            return null;
        }

        public string GetString(int ID, int index)
        {
            switch(ID)
            {
                case 1:
                    return Resolutions[index];
            }
            return "error";
        }
    }
}
