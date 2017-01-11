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
            Locked_Gem_List.Add(new Ruby_1());
            Locked_Gem_List.Add(new Ruby_2());
            Locked_Gem_List.Add(new Ruby_3());
            Locked_Gem_List.Add(new Ruby_4());
            Locked_Gem_List.Add(new Ruby_5());
            Locked_Gem_List.Add(new Sapphire_1());
            Locked_Gem_List.Add(new Sapphire_2());
            Locked_Gem_List.Add(new Sapphire_3());
            Locked_Gem_List.Add(new Sapphire_4());
            Locked_Gem_List.Add(new Sapphire_5());
            Locked_Gem_List.Add(new Garnet_1());
            Locked_Gem_List.Add(new Pearl_1());
            Locked_Gem_List.Add(new Amethyst_1());
            Locked_Gem_List.Add(new Opal_1());
            Locked_Gem_List.Add(new Sardonyx_1());
            Locked_Gem_List.Add(new Sugilite_1());


            
    
    
        }
    }


    public class Locked_Gems
    {
        //The string tracker.
        public String_Tracker Current_Strings = new String_Tracker();

        //the stats.
        public bool b_Locked = false;
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

        public float f_Future_Sight_Amount = 0;

        //this is the scale of the tower, .4 for 1gem, .6 for 2gem, .8 for 3gem, 1 for 4gem.
        public float f_Scale_Amount = 1;

        public string s_Prefab_Location;

        public string s_Bullet_Prefab_Location;


        //This is the description of the tower. Shows up in the purchase area.
        public string s_Desc = "None";


        //this is a 2d array of what is required to make a new gem for fusing.
        public string[,] Fuse_Tables;

    }



    class Ruby_1 : Locked_Gems
    {
        public Ruby_1()
        {
            Name = "Ruby 1";
            Attack_Name = "Ruby";
            i_Cost = 50;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 2;
            f_Range_Levels = .1f;


            f_Speed_Amount = 6;
            f_Speed_Levels = .05f;

            f_Power_Amount = 6;
            f_Power_Levels = .2f;


            s_Desc = "Flame AOE Attack from center.";


            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Ruby 1","Ruby 2"},
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 1","Garnet 1"}
            };
        }
    }

    class Ruby_2 : Locked_Gems
    {
        public Ruby_2()
        {
            Name = "Ruby 2";
            Attack_Name = "Ruby";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .3f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Flame AOE Attack from center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 1","Ruby 3"},
                {"Ruby 2","Ruby 4"},
                {"Ruby 3","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }
    class Ruby_3 : Locked_Gems
    {
        public Ruby_3()
        {
            Name = "Ruby 3";
            Attack_Name = "Ruby";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .45f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Flame AOE Attack from center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Ruby 1","Ruby 4"},
                {"Ruby 2","Ruby 5"},
                {"Sapphire 3","Garnet 3"}
            };
        }
    }
    class Ruby_4 : Locked_Gems
    {
        public Ruby_4()
        {
            Name = "Ruby 4";
            Attack_Name = "Ruby";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .6f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Flame AOE Attack from center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Ruby 1","Ruby 5"},
                {"Sapphire 4","Garnet 4"}
            };
        }
    }
    class Ruby_5 : Locked_Gems
    {
        public Ruby_5()
        {
            Name = "Ruby 5";
            Attack_Name = "Ruby";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .75f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Ruby;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Ruby";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Flame AOE Attack from center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[1, 2] {
                {"Sapphire 5","Garnet 5"}
            };
        }
    }

   


    class Sapphire_1 : Locked_Gems
    {
        public Sapphire_1()
        {
            Name = "Sapphire 1";
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Ice AOE Attack From Center.\nSlows Hit Enemies.\nCan See What Waves Will Be Attacking.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Sapphire 1","Sapphire 2"},
                {"Sapphire 2","Sapphire 3"},
                {"Sapphire 3","Sapphire 4"},
                {"Sapphire 4","Sapphire 5"},
                {"Ruby 1","Garnet 1"}
            };

        }
    }

    class Sapphire_2 : Locked_Gems
    {
        public Sapphire_2()
        {
            Name = "Sapphire 2";
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .3f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Ice AOE Attack From Center.\nSlows Hit Enemies.\nCan See What Waves Will Be Attacking.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Sapphire 1","Sapphire 3"},
                {"Sapphire 2","Sapphire 4"},
                {"Sapphire 3","Sapphire 5"},
                {"Ruby 2","Garnet 2"}
            };

        }
    }

    class Sapphire_3 : Locked_Gems
    {
        public Sapphire_3()
        {
            Name = "Sapphire 3";
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .45f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Ice AOE Attack From Center.\nSlows Hit Enemies.\nCan See What Waves Will Be Attacking.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Sapphire 1","Sapphire 4"},
                {"Sapphire 2","Sapphire 5"},
                {"Ruby 3","Garnet 3"}
            };

        }
    }

    class Sapphire_4 : Locked_Gems
    {
        public Sapphire_4()
        {
            Name = "Sapphire 4";
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .6f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Ice AOE Attack From Center.\nSlows Hit Enemies.\nCan See What Waves Will Be Attacking.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Sapphire 1","Sapphire 5"},
                {"Ruby 4","Garnet 4"}
            };

        }
    }

    class Sapphire_5 : Locked_Gems
    {
        public Sapphire_5()
        {
            Name = "Sapphire 5";
            Attack_Name = "Sapphire";

            i_Cost = 50;
            f_Scale_Amount = .75f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;


            f_Range_Amount = 4;
            //seconds before an attack.
            f_Speed_Amount = 6;
            f_Power_Amount = 6;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            s_Desc = "Ice AOE Attack From Center.\nSlows Hit Enemies.\nCan See What Waves Will Be Attacking.";

            //sp_Tower_Sprite = UnityEngine.Object.Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[1, 2] {
                {"Ruby 5","Garnet 5"}
            };

        }
    }

    class Pearl_1 : Locked_Gems
    {
        public Pearl_1()
        {
            Name = "Pearl 1";
            Attack_Name = "Pearl";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .2f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Pearl;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Pearl";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }

    class Amethyst_1 : Locked_Gems
    {
        public Amethyst_1()
        {
            Name = "Amethyst 1";
            Attack_Name = "Amethyst";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Amethyst Desc";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }

    class Garnet_1 : Locked_Gems
    {
        public Garnet_1()
        {
            Name = "Garnet 1";
            Attack_Name = "Garnet";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .3f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Rubies are often short fused gems.\nThey emit a flame based AOE attack.\nThis attack is centered at their location and spreads out.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }

    

    class Opal_1 : Locked_Gems
    {
        public Opal_1()
        {
            Name = "Opal 1";
            Attack_Name = "Opal";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Opal Desc";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }
    
    class Sardonyx_1 : Locked_Gems
    {
        public Sardonyx_1()
        {
            Name = "Sardonyx 1";
            Attack_Name = "Sardonyx";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Sardonyx Desc";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }

    class Sugilite_1 : Locked_Gems
    {
        public Sugilite_1()
        {
            Name = "Sugilite 1";
            Attack_Name = "Sugilite";
            b_Locked = false;


            i_Cost = 300;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;


            s_Desc = "Sugilite Desc";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Ruby 2","Ruby 3"},
                {"Ruby 3","Ruby 4"},
                {"Ruby 4","Ruby 5"},
                {"Sapphire 2","Garnet 2"}
            };
        }
    }


}
