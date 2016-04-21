using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
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
        public int Cost = 0;
        //how much exp is needed to reach the next level.
        public int[] i_Exp_Level = new int[] { 10, 100, 100, 100, 100, 100, 100, 100, 10, 10 };
        //10 levels is MAX unless it's set manually.
        public int[] i_Power_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Power_Amount = 1;
        public int[] i_Range_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Range_Amount = 1;
        public int[] i_Speed_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Speed_Amount = 1;
        public string s_Prefab_Location;
    }



    class Ruby :Locked_Gems
    {
        public Ruby()
        {
            Name = "Ruby";
            Cost = 50;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;
        }
    }

}
