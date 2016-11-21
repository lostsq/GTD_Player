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
            Locked_Gem_List.Add(new Ruby_2());
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
        public string Attack_Name;
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

        public string s_Bullet_Prefab_Location;


        //This is the description of the tower. Shows up in the purchase area.
        public string s_Desc = "None";


        //this is a 2d array of what is required to make a new gem for fusing.
        public string[,] Fuse_Tables;

    }



    class Ruby : Locked_Gems
    {
        public Ruby()
        {
            Name = "Ruby";
            Attack_Name = "Ruby";
            i_Cost = 50;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


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

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Ruby","Ruby 2"},
                {"Ruby 2","Ruby 3"},
                {"Sapphire","Garnet"}
            };
        }
    }

    class Ruby_2 : Locked_Gems
    {
        public Ruby_2()
        {
            Name = "Ruby 2";
            Attack_Name = "Ruby";

            i_Cost = 300;
            f_Scale_Amount = .5f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 6;
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
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";


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
