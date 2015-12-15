using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Test
{
    public class Control_Members
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


        List<string> Resolutions;

        public void Create_Lists()
        {
            #region "Resolutions"
            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {

            }
            #endregion
        }

        /*public List<string> GetList(int ID)
        {
            switch(ID)
            {
                case 1:
                    return Resolutions;
                    break;
            }
        }*/
    }
}
