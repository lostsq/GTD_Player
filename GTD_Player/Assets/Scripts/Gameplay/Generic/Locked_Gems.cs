using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Generic
{
    public class All_Locked_Gems
    {
        public List<Locked_Gems> Locked_Gem_List = new List<Locked_Gems>();
        public All_Locked_Gems()
        {
            //This will create all the gems.
            Locked_Gem_List.Add(new Ruby());
        }
    }


    public class Locked_Gems
    {
        //The string tracker.
        public String_Tracker Current_Strings = new String_Tracker();

        //the stats.
        public bool b_Locked = true;
        public string Name;
        public int i_Cost = 0;
        //how much exp is needed to reach the next level.
        public int[] i_Exp_Level = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 0 };
        //10 levels is MAX unless it's set manually.
        public int[] i_Power_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Power_Amount = 1;
        public int[] i_Range_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Range_Amount = 1;
        public int[] i_Speed_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Speed_Amount = 1;

        //this is the scale of the tower, .4 for 1gem, .6 for 2gem, .8 for 3gem, 1 for 4gem.
        public float f_Scale_Amount = 1;

        public string s_Prefab_Location;


        //This is the description of the tower. Shows up in the purchase area.
        public string s_Desc = "None";

    }



    class Ruby :Locked_Gems
    {
        public Ruby()
        {
            Name = "Ruby";
            i_Cost = 50;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            i_Range_Amount = 4;

            s_Desc = "Rubies are often short fused gems.\nThey emit a flame based AOE attack.\nThis attack is centered at their location and spreads out.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;
        }
    }

}
