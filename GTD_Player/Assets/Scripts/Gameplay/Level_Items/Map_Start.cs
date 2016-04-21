using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gameplay.Level_Items
{
    class Map_Start : Map_Space
    {
        public void Map_Start_Setup(string p_Name, int px, int py)
        {
            X_Spot = px;
            Y_Spot = py;
            Name = p_Name;

            b_Contains_Object = true;
        }
    }
}
