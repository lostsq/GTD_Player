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
        //this is used for price and point/level increases.
        float f_Level_Attack_Percent = .05f;
        float f_Level_Range_Percent = .05f;
        float f_Level_Cooldown_Percent = .03f;
        //the fuse gem amounts.
        float f_Fuse_Attack_Percent = .15f;
        float f_Fuse_Range_Percent = .15f;
        float f_Fuse_Cooldown_Percent = .1f;
        //cost increases.
        float f_Cost_Increase = .1f;


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

            Locked_Gem_List.Add(new Pearl_1());
            Locked_Gem_List.Add(new Pearl_2());
            Locked_Gem_List.Add(new Pearl_3());
            Locked_Gem_List.Add(new Pearl_4());
            Locked_Gem_List.Add(new Pearl_5());

            Locked_Gem_List.Add(new Amethyst_1());
            Locked_Gem_List.Add(new Amethyst_2());
            Locked_Gem_List.Add(new Amethyst_3());
            Locked_Gem_List.Add(new Amethyst_4());
            Locked_Gem_List.Add(new Amethyst_5());

            Locked_Gem_List.Add(new Opal_1());
            Locked_Gem_List.Add(new Opal_2());
            Locked_Gem_List.Add(new Opal_3());
            Locked_Gem_List.Add(new Opal_4());
            Locked_Gem_List.Add(new Opal_5());

            Locked_Gem_List.Add(new Garnet_1());
            Locked_Gem_List.Add(new Garnet_2());
            Locked_Gem_List.Add(new Garnet_3());
            Locked_Gem_List.Add(new Garnet_4());
            Locked_Gem_List.Add(new Garnet_5());

            Locked_Gem_List.Add(new Sardonyx_1());
            Locked_Gem_List.Add(new Sardonyx_2());
            Locked_Gem_List.Add(new Sardonyx_3());
            Locked_Gem_List.Add(new Sardonyx_4());
            Locked_Gem_List.Add(new Sardonyx_5());

            Locked_Gem_List.Add(new Sugilite_1());
            Locked_Gem_List.Add(new Sugilite_2());
            Locked_Gem_List.Add(new Sugilite_3());
            Locked_Gem_List.Add(new Sugilite_4());
            Locked_Gem_List.Add(new Sugilite_5());


            //after adding all the gems we set up the variables
            Set_Up_Variables();

        }


        //this sets up the variables like attack,range,cost,ect based on the fusions.
        public void Set_Up_Variables()
        {
            bool b_Finished_Setup = false;

            while (!b_Finished_Setup)
            {
                b_Finished_Setup = true;

                for (int i = 0; i < Locked_Gem_List.Count; i++)
                {

                    //first we split up the name.
                    string Gem_Name = Locked_Gem_List[i].Name.Split(' ')[0];
                    int Gem_Number = int.Parse(Locked_Gem_List[i].Name.Split(' ')[1]);

                    //make sure that all the ones prior have been set up, if not we skip until 1 is found.
                    if (Gem_Number == 1)
                    {
                        //for the level 1 gem we only set up the levels amount.
                        Locked_Gem_List[i].f_Range_Levels = Locked_Gem_List[i].f_Range_Amount * f_Level_Range_Percent;

                        Locked_Gem_List[i].f_Speed_Levels = -1 * (Locked_Gem_List[i].f_Speed_Amount * f_Level_Cooldown_Percent);

                        Locked_Gem_List[i].f_Power_Levels = Locked_Gem_List[i].f_Power_Amount * f_Level_Attack_Percent;

                        //it is now set up so we set it up to true.
                        Locked_Gem_List[i].b_Is_Set_Up = true;
                    }
                    else
                    {
                        //need to make sure the one before it has been set up.
                        for (int j = 0; j < Locked_Gem_List.Count; j++)
                        {
                            //first we split up the name.
                            string Gem_Name2 = Locked_Gem_List[j].Name.Split(' ')[0];
                            int Gem_Number2 = int.Parse(Locked_Gem_List[j].Name.Split(' ')[1]);

                            if (Gem_Name2 == Gem_Name && Gem_Number2 == (Gem_Number - 1))
                            {
                                //now check the set up then go from there.
                                if (Locked_Gem_List[j].b_Is_Set_Up)
                                {

                                    Locked_Gem_List[i].f_Range_Amount = Locked_Gem_List[j].f_Range_Amount + (Locked_Gem_List[j].f_Range_Amount * f_Fuse_Range_Percent);
                                    Locked_Gem_List[i].f_Range_Levels = Locked_Gem_List[i].f_Range_Amount * f_Level_Range_Percent;

                                    Locked_Gem_List[i].f_Speed_Amount = Locked_Gem_List[j].f_Speed_Amount - (Locked_Gem_List[j].f_Speed_Amount * f_Fuse_Cooldown_Percent);
                                    Locked_Gem_List[i].f_Speed_Levels = -1 * (Locked_Gem_List[i].f_Speed_Amount * f_Level_Cooldown_Percent);

                                    Locked_Gem_List[i].f_Power_Amount = Locked_Gem_List[j].f_Power_Amount + (Locked_Gem_List[j].f_Power_Amount * f_Fuse_Attack_Percent);
                                    Locked_Gem_List[i].f_Power_Levels = Locked_Gem_List[i].f_Power_Amount * f_Level_Attack_Percent;

                                    //cost
                                    Locked_Gem_List[i].i_Cost = Convert.ToInt32(((Locked_Gem_List[j].i_Cost / Gem_Number2) * Gem_Number) + ((Locked_Gem_List[j].i_Cost / Gem_Number2) * f_Cost_Increase));

                                    //it is now set up so we set it up to true.
                                    Locked_Gem_List[i].b_Is_Set_Up = true;
                                }

                            }
                        }
                    }




                    //check if it was set up, if not set to false.
                    if (!Locked_Gem_List[i].b_Is_Set_Up)
                    {
                        b_Finished_Setup = false;
                    }

                }
            }
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


        public bool b_Is_Set_Up = false;
        

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
            b_Locked = false;



            f_Range_Amount = 1.5f;


            f_Speed_Amount = 8;

            f_Power_Amount = 5;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };



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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            i_Cost = 80;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sapphire;
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sapphire";

            //this is for the future sight to see what waves will be coming.
            f_Future_Sight_Amount = 1.5f;
            b_Locked = false;


            f_Range_Amount = 2;
            //seconds before an attack.
            f_Speed_Amount = 6.5f;
            f_Power_Amount = 5;

            //how much range is added. Remember this is based off of scale.
            f_Range_Levels = 3;
            //minus this much for speed per level. (10 max)
            f_Speed_Levels = -.5f;
            f_Power_Levels = 1.2f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

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

            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };


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


            i_Cost = 70;
            f_Scale_Amount = .2f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Pearl;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Pearl";


            f_Range_Amount = 3f;


            f_Speed_Amount = 7;

            f_Power_Amount = 7f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Pearl 2","Pearl 3"},
                {"Pearl 3","Pearl 4"},
                {"Pearl 4","Pearl 5"},
                {"Amethyst 1","Opal 1"},
                {"Garnet 1","Sardonyx 1"}
            };
        }
    }
    class Pearl_2 : Locked_Gems
    {
        public Pearl_2()
        {
            Name = "Pearl 2";
            Attack_Name = "Pearl";

            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Pearl 1","Pearl 3"},
                {"Pearl 2","Pearl 4"},
                {"Pearl 3","Pearl 5"},
                {"Amethyst 2","Opal 2"},
                {"Garnet 2","Sardonyx 2"}
            };
        }
    }
    class Pearl_3 : Locked_Gems
    {
        public Pearl_3()
        {
            Name = "Pearl 3";
            Attack_Name = "Pearl";
            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Pearl 1","Pearl 4"},
                {"Pearl 2","Pearl 5"},
                {"Amethyst 3","Opal 3"},
                {"Garnet 3","Sardonyx 3"}
            };
        }
    }
    class Pearl_4 : Locked_Gems
    {
        public Pearl_4()
        {
            Name = "Pearl 4";
            Attack_Name = "Pearl";
            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Pearl 1","Pearl 5"},
                {"Amethyst 4","Opal 4"},
                {"Garnet 4","Sardonyx 4"}
            };
        }
    }
    class Pearl_5 : Locked_Gems
    {
        public Pearl_5()
        {
            Name = "Pearl 5";
            Attack_Name = "Pearl";
            b_Locked = true;

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

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "A Fast Ranged Attack With A Spear.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Amethyst 5","Opal 5"},
                {"Garnet 5","Sardonyx 5"}
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


            i_Cost = 100;
            f_Scale_Amount = .15f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 2.5f;
            f_Speed_Amount = 6;
            f_Power_Amount = 5f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Performs A Spin AOE Attack From Center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[6, 2] {
                {"Amethyst 1","Amethyst 2"},
                {"Amethyst 2","Amethyst 3"},
                {"Amethyst 3","Amethyst 4"},
                {"Amethyst 4","Amethyst 5"},
                {"Garnet 1","Sugilite 1"},
                {"Pearl 1","Opal 1"}
            };
        }
    }
    class Amethyst_2 : Locked_Gems
    {
        public Amethyst_2()
        {
            Name = "Amethyst 2";
            Attack_Name = "Amethyst";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .3f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Performs A Spin AOE Attack From Center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Amethyst 1","Amethyst 3"},
                {"Amethyst 2","Amethyst 4"},
                {"Amethyst 3","Amethyst 5"},
                {"Garnet 2","Sugilite 2"},
                {"Pearl 2","Opal 2"}
            };
        }
    }
    class Amethyst_3 : Locked_Gems
    {
        public Amethyst_3()
        {
            Name = "Amethyst 3";
            Attack_Name = "Amethyst";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .45f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Performs A Spin AOE Attack From Center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Amethyst 1","Amethyst 4"},
                {"Amethyst 2","Amethyst 5"},
                {"Garnet 3","Sugilite 3"},
                {"Pearl 3","Opal 3"}
            };
        }
    }
    class Amethyst_4 : Locked_Gems
    {
        public Amethyst_4()
        {
            Name = "Amethyst 4";
            Attack_Name = "Amethyst";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .6f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Performs A Spin AOE Attack From Center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Amethyst 1","Amethyst 5"},
                {"Garnet 4","Sugilite 4"},
                {"Pearl 4","Opal 4"}
            };
        }
    }
    class Amethyst_5 : Locked_Gems
    {
        public Amethyst_5()
        {
            Name = "Amethyst 5";
            Attack_Name = "Amethyst";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .75f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Amethyst;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Amethyst";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Performs A Spin AOE Attack From Center.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Garnet 5","Sugilite 5"},
                {"Pearl 5","Opal 5"}
            };
        }
    }

    class Opal_1 : Locked_Gems
    {
        public Opal_1()
        {
            Name = "Opal 1";
            Attack_Name = "Opal";
            b_Locked = true;


            i_Cost = 200;
            f_Scale_Amount = .2f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 3.5f;
            f_Speed_Amount = 5;
            f_Power_Amount = 5f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires On Multiple Enemies At Once.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Opal 1","Opal 2"},
                {"Opal 2","Opal 3"},
                {"Opal 3","Opal 4"},
                {"Opal 4","Opal 5"}
            };
        }
    }
    class Opal_2 : Locked_Gems
    {
        public Opal_2()
        {
            Name = "Opal 2";
            Attack_Name = "Opal";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .35f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires On Multiple Enemies At Once.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Opal 1","Opal 3"},
                {"Opal 2","Opal 4"},
                {"Opal 3","Opal 5"},
            };
        }
    }
    class Opal_3 : Locked_Gems
    {
        public Opal_3()
        {
            Name = "Opal 3";
            Attack_Name = "Opal";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .5f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires On Multiple Enemies At Once.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Opal 1","Opal 4"},
                {"Opal 2","Opal 5"}
            };
        }
    }
    class Opal_4 : Locked_Gems
    {
        public Opal_4()
        {
            Name = "Opal 4";
            Attack_Name = "Opal";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .65f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires On Multiple Enemies At Once.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[1, 2] {
                {"Opal 1","Opal 5"}
            };
        }
    }
    class Opal_5 : Locked_Gems
    {
        public Opal_5()
        {
            Name = "Opal 5";
            Attack_Name = "Opal";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .8f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Opal;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Opal";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 3;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires On Multiple Enemies At Once.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[0, 2] {
            };
        }
    }

    class Garnet_1 : Locked_Gems
    {
        public Garnet_1()
        {
            Name = "Garnet 1";
            Attack_Name = "Garnet";
            b_Locked = true;


            i_Cost = 150;
            f_Scale_Amount = .2f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 2.5f;
            f_Speed_Amount = 6;
            f_Power_Amount = 7f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Attacks One Ranged And Stuns Them.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[6, 2] {
                {"Garnet 1","Garnet 2"},
                {"Garnet 2","Garnet 3"},
                {"Garnet 3","Garnet 4"},
                {"Garnet 4","Garnet 5"},
                {"Pearl 1","Sardonyx 1"},
                {"Amethyst 1","Sugilite 1"}
            };
        }
    }
    class Garnet_2 : Locked_Gems
    {
        public Garnet_2()
        {
            Name = "Garnet 2";
            Attack_Name = "Garnet";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .35f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Attacks One Ranged And Stuns Them.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[5, 2] {
                {"Garnet 1","Garnet 3"},
                {"Garnet 2","Garnet 4"},
                {"Garnet 3","Garnet 5"},
                {"Pearl 2","Sardonyx 2"},
                {"Amethyst 2","Sugilite 2"}
            };
        }
    }
    class Garnet_3 : Locked_Gems
    {
        public Garnet_3()
        {
            Name = "Garnet 3";
            Attack_Name = "Garnet";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .5f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Attacks One Ranged And Stuns Them.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Garnet 1","Garnet 4"},
                {"Garnet 2","Garnet 5"},
                {"Pearl 3","Sardonyx 3"},
                {"Amethyst 3","Sugilite 3"}
            };
        }
    }
    class Garnet_4 : Locked_Gems
    {
        public Garnet_4()
        {
            Name = "Garnet 4";
            Attack_Name = "Garnet";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .65f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Attacks One Ranged And Stuns Them.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Garnet 1","Garnet 5"},
                {"Pearl 4","Sardonyx 4"},
                {"Amethyst 4","Sugilite 4"}
            };
        }
    }
    class Garnet_5 : Locked_Gems
    {
        public Garnet_5()
        {
            Name = "Garnet 5";
            Attack_Name = "Garnet";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .8f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Garnet;
            //use the same attack for ruby.
            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Garnet";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Attacks One Ranged And Stuns Them.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Pearl 5","Sardonyx 5"},
                {"Amethyst 5","Sugilite 5"}
            };
        }
    }

    class Sardonyx_1 : Locked_Gems
    {
        public Sardonyx_1()
        {
            Name = "Sardonyx 1";
            Attack_Name = "Sardonyx";
            b_Locked = true;


            i_Cost = 250;
            f_Scale_Amount = .25f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4f;
            f_Speed_Amount = 6;
            f_Power_Amount = 7f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires Ranged To Target Then AOE From That Location.\nWill Cause First Target To Stun.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Sardonyx 1","Sardonyx 2"},
                {"Sardonyx 2","Sardonyx 3"},
                {"Sardonyx 3","Sardonyx 4"},
                {"Sardonyx 4","Sardonyx 5"}
            };
        }
    }
    class Sardonyx_2 : Locked_Gems
    {
        public Sardonyx_2()
        {
            Name = "Sardonyx 2";
            Attack_Name = "Sardonyx";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .5f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires Ranged To Target Then AOE From That Location.\nWill Cause First Target To Stun.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Sardonyx 1","Sardonyx 3"},
                {"Sardonyx 2","Sardonyx 4"},
                {"Sardonyx 3","Sardonyx 5"}
            };
        }
    }
    class Sardonyx_3 : Locked_Gems
    {
        public Sardonyx_3()
        {
            Name = "Sardonyx 3";
            Attack_Name = "Sardonyx";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .55f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires Ranged To Target Then AOE From That Location.\nWill Cause First Target To Stun.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Sardonyx 1","Sardonyx 4"},
                {"Sardonyx 2","Sardonyx 5"}
            };
        }
    }
    class Sardonyx_4 : Locked_Gems
    {
        public Sardonyx_4()
        {
            Name = "Sardonyx 4";
            Attack_Name = "Sardonyx";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .7f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires Ranged To Target Then AOE From That Location.\nWill Cause First Target To Stun.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[1, 2] {
                {"Sardonyx 1","Sardonyx 5"}
            };
        }
    }
    class Sardonyx_5 : Locked_Gems
    {
        public Sardonyx_5()
        {
            Name = "Sardonyx 5";
            Attack_Name = "Sardonyx";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .85f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sardonyx;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sardonyx";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Fires Ranged To Target Then AOE From That Location.\nWill Cause First Target To Stun.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[0, 2] {
            };
        }
    }

    class Sugilite_1 : Locked_Gems
    {
        public Sugilite_1()
        {
            Name = "Sugilite 1";
            Attack_Name = "Sugilite";
            b_Locked = true;


            i_Cost = 280;
            f_Scale_Amount = .25f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Speed_Amount = 10;
            f_Power_Amount = 11f;


            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Slow Yet Powerful AOE Attack That Stuns All.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[4, 2] {
                {"Sugilite 1","Sugilite 2"},
                {"Sugilite 2","Sugilite 3"},
                {"Sugilite 3","Sugilite 4"},
                {"Sugilite 4","Sugilite 5"}
            };
        }
    }
    class Sugilite_2 : Locked_Gems
    {
        public Sugilite_2()
        {
            Name = "Sugilite 2";
            Attack_Name = "Sugilite";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .4f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Slow Yet Powerful AOE Attack That Stuns All.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[3, 2] {
                {"Sugilite 1","Sugilite 3"},
                {"Sugilite 2","Sugilite 4"},
                {"Sugilite 3","Sugilite 5"}
            };
        }
    }
    class Sugilite_3 : Locked_Gems
    {
        public Sugilite_3()
        {
            Name = "Sugilite 3";
            Attack_Name = "Sugilite";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .55f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };

            s_Desc = "Slow Yet Powerful AOE Attack That Stuns All.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[2, 2] {
                {"Sugilite 1","Sugilite 4"},
                {"Sugilite 2","Sugilite 5"}
            };
        }
    }
    class Sugilite_4 : Locked_Gems
    {
        public Sugilite_4()
        {
            Name = "Sugilite 4";
            Attack_Name = "Sugilite";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .7f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };


            s_Desc = "Slow Yet Powerful AOE Attack That Stuns All.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[1, 2] {
                {"Sugilite 1","Sugilite 5"}
            };
        }
    }
    class Sugilite_5 : Locked_Gems
    {
        public Sugilite_5()
        {
            Name = "Sugilite 5";
            Attack_Name = "Sugilite";
            b_Locked = true;


            i_Cost = 300;
            f_Scale_Amount = .85f;
            s_Prefab_Location = Current_Strings.Prefab_Tower_Sugilite;


            s_Bullet_Prefab_Location = Current_Strings.Prefab_Attacks_Location + "Sugilite";


            f_Range_Amount = 4.5f;
            f_Range_Levels = .15f;


            f_Speed_Amount = 7;
            f_Speed_Levels = .08f;

            f_Power_Amount = 6.8f;
            f_Power_Levels = .3f;

            i_Exp_Level = new int[] { (int)(i_Cost * .1f), (int)(i_Cost * .2f), (int)(i_Cost * .3f), (int)(i_Cost * .4f), (int)(i_Cost * .5f), (int)(i_Cost * .6f), (int)(i_Cost * .7f), (int)(i_Cost * .8f), (int)(i_Cost * .9f), (int)(i_Cost * 1f), 0 };


            s_Desc = "Slow Yet Powerful AOE Attack That Stuns All.";

            //how this works is the first name is if this gem is fused with it then the second is what it makes.
            Fuse_Tables = new string[0, 2] {
            };
        }
    }


}
