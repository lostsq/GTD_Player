using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gameplay.Level_Items
{
    class Map_Temple : Map_Space 
    {
        public int i_HP = 0;

        public void Map_Temple_Setup(string p_Name, int px, int py)
        {
            X_Spot = px;
            Y_Spot = py;
            Name = p_Name;

            b_Contains_Object = true;
        }
    }
}
