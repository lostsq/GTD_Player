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
            Locked_Gem_List.Add(new Sapphire());
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
        
        //This is the amount for each item.
        //The levels is how much is added per point spent. Each gem will have it's own values for all.
        public float f_Power_Levels = 1;
        public float f_Power_Amount = 1;
        public float f_Range_Levels = 1;
        public float f_Range_Amount = 1;
        public float f_Speed_Levels = 1;
        public float f_Speed_Amount = 1;

        //this is the scale of the tower, .4 for 1gem, .6 for 2gem, .8 for 3gem, 1 for 4gem.
        public float f_Scale_Amount = 1;

        public string s_Prefab_Location;


        //This is the description of the tower. Shows up in the purchase area.
        public string s_Desc = "None";

    }



    class Ruby : Locked_Gems
    {
        public Ruby()
        {
            Name = "Ruby";
            i_Cost = 50;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = .5f;
            f_Power_Levels = 1.2f;
            

            s_Desc = "Rubies are often short fused gems.\nThey emit a flame based AOE attack.\nThis attack is centered at their location and spreads out.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;
        }
    }
    class Sapphire : Locked_Gems
    {
        public Sapphire()
        {
            Name = "Sapphire";
            i_Cost = 50;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Sapphire's are calm cool collected gems.\nThey chill enemies near them and can see a bit into the future.\nThis attack is centered at their location and spreads out.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;
        }
    }

}
