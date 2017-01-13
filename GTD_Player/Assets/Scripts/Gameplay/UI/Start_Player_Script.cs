﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using Assets.Scripts.Gameplay.Level_Items;

public class Start_Player_Script : MonoBehaviour {
    //The string tracker.
    String_Tracker Current_Strings = new String_Tracker();
    //This is the scale of the invintory and such to start with.
    float f_Default_Scale = 1;
    float f_Hotbox_Size = 2.66f;
    float f_Default_Box_Size = 2.56f;

    //This is set to false if the level fails to load in any way. it is then aborted.
    bool b_Level_Fail = false;

    //This is the main script that holds all the information.
    Player_Main_Script Main_Script;

    //This is what level we will be loading, for now it's not here just because we are going to generate out the hotboxes.

    // Use this for initialization
    void Start() {

        //get the main script to connect to it/start calls in it when we are finished setting things up.
        Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();

        //First we will create the hotbars.
        Setup_Hotbar();
        //set up the inventory boxes.
        Setup_Inventory_Boxes();
        //we set up the fuse menu
        Setup_Fuse_Menu();
        //loads the level we are playing.
        Load_Level_Save();

        //after the level is loaded or default we then create/set up the purchase menu with the unlocked gems.
        Setup_Purchase_Menu();

        //This will set up the enemy UI screen.
        Setup_Enemy_Viewer();
    }

    // Update is called once per frame
    void Update() {

    }


    //This will load the saved level.
    void Load_Level_Save()
    {
        //load the test level. This will be changed when the game is final and we have multiple levels.
        TextAsset Level = Resources.Load("Made_Levels/Test_Level") as TextAsset;
        //Debug.Log(Level.text);

        //test
        //GameObject.Find("Test").GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load(Current_Strings.Texture_Tower_Ruby)) as Sprite;//Main_Script.Locked_Gems.Locked_Gem_List[0].sp_Tower_Sprite;
        //Tower Test_Tower = new Tower();
        //GameObject New_Test = Main_Script.Create_New_Tower("Ruby");
        //New_Test.transform.position = new Vector3(0, 0);

        //height/width and spaces stored here to create the map after we have all the information.
        int t_Width = 0;
        int t_Height = 0;
        bool b_Shared_Energy = false;
        bool b_Shared_Allies = false;

        //This is the theme for the background of the map.
        string s_Theme = "Default";


        //All the level string items.
        string[] Level_Items = Level.text.Split('\n');
        //Going through all the level items.
        for (int i = 0; i < Level_Items.Length; i++)
        {
            //THE VARIABLES THAT NEED TO BE SET.
            if (Level_Items[i].Contains("Name("))
            {
                Main_Script.s_Level_Name = Level_Items[i].Split('(')[1];
            }
            else if (Level_Items[i].Contains("Starting Energy("))
            {
                Main_Script.f_Energy = float.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Shared Enery("))
            {
                b_Shared_Energy = bool.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Starting HP("))
            {
                Main_Script.f_HP = int.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Max HP("))
            {
                Main_Script.i_Max_HP = int.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Shared Allies("))
            {
                b_Shared_Allies = bool.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Grid X("))
            {
                t_Width = int.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Grid Y("))
            {
                t_Height = int.Parse(Level_Items[i].Split('(')[1]);
            }
            else
            {
                //OBJECTS THAT NEED TO BE MADE/SET
                if (Level_Items[i].Contains("Lock Gem("))
                {
                    //name,locked,cost
                    string Arg1 = Level_Items[i].Split('(')[1].Split(',')[0];
                    string Arg2 = Level_Items[i].Split('(')[1].Split(',')[1];
                    string Arg3 = Level_Items[i].Split('(')[1].Split(',')[2];

                    //search current locked gem list and set it up.
                    for (int j = 0; j < Main_Script.Locked_Gems.Locked_Gem_List.Count; j++)
                    {
                        if (Main_Script.Locked_Gems.Locked_Gem_List[j].Name == Arg1)
                        {
                            Main_Script.Locked_Gems.Locked_Gem_List[j].i_Cost = int.Parse(Arg3);
                            Main_Script.Locked_Gems.Locked_Gem_List[j].b_Locked = bool.Parse(Arg2);
                        }
                    }

                }
                if (Level_Items[i].Contains("Tower("))
                {
                    // Tower_List[i].Tower_Name + "," + Tower_List[i].Tower_Level + "," + Tower_List[i].Tower_Power + "," + Tower_List[i].Tower_Speed + "," + Tower_List[i].Tower_Range + "," + Tower_List[i].Tower_Points;
                    string Arg1 = Level_Items[i].Split('(')[1].Split(',')[0];
                    int Arg2 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[1]);
                    int Arg3 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[2]);
                    int Arg4 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[3]);
                    int Arg5 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[4]);
                    int Arg6 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[5]);

                    //create the tower, update the stats and move it.
                    GameObject go_New_Tower = Main_Script.Create_New_Tower(Arg1);

                    

                    //tower not null, we add it to the list and move it to invintory.
                    if (go_New_Tower != null)
                    {
                        Tower New_Tower = go_New_Tower.GetComponent<Tower>();
                        //Sets up the tower.
                        New_Tower.i_Level = Arg2;
                        New_Tower.f_Power_Amount = Arg3;
                        New_Tower.f_Speed_Amount = Arg4;
                        New_Tower.f_Range_Amount = Arg5;
                        New_Tower.i_Spending_Points = Arg6;

                        Main_Script.Tower_List.Add(go_New_Tower);
                        go_New_Tower.GetComponent<Tower>().Move_To_Invintory(true);
                    }

                }

                //Enemy_List[i].Enemy_Name + "," + Enemy_List[i].Enemy_Wave_Number + "," + Enemy_List[i].Enemy_HP + "," + Enemy_List[i].Enemy_Speed + "," + Enemy_List[i].Enemy_Power + "," + Enemy_List[i].Enemy_Amount + "," + Enemy_List[i].Enemy_Start_After + "," + Enemy_List[i].Enemy_Reward_Single + "," + Enemy_List[i].Enemy_Reward_Wave + "," + Enemy_List[i].Enemy_Mod;
                if (Level_Items[i].Contains("Enemies("))
                {
                    string Arg1 = Level_Items[i].Split('(')[1].Split(',')[0];
                    int Arg2 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[1]);
                    int Arg3 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[2]);
                    int Arg4 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[3]);
                    int Arg5 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[4]);
                    int Arg6 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[5]);
                    int Arg7 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[6]);
                    int Arg8 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[7]);
                    int Arg9 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[8]);
                    string Arg10 = Level_Items[i].Split('(')[1].Split(',')[9];

                    //create the enemy and add it to the main scripts list.
                    //GameObject New_Temp = new GameObject();
                    //New_Temp.transform.position = new Vector3(-500, -500);
                    //New_Temp.AddComponent<Enemy>();

                    //Enemy New_Enemy = New_Temp.GetComponent<Enemy>();
                    Enemy New_Enemy = new Enemy();
                    //set up the new enemy as a spawner.
                    New_Enemy.Set_Enemy(Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8, Arg9, Arg10,true);

                    //set up the bullet prefab location.
                    New_Enemy.s_Bullet_Prefab = Current_Strings.Prefab_Attacks_Location + Arg1.Split(' ')[0];
                    //Debug.Log(New_Enemy.s_Bullet_Prefab);

                    //might need to create the item so it's not null and have it off screen.
                    New_Enemy.i_Location_In_Spawn_Array = Main_Script.Add_To_Enemy_Spawns(New_Enemy);// Main_Script.Enemy_Spawns.Count;
                                                                                                     //Main_Script.Enemy_Spawns.Add(New_Enemy);

                    //this is for endless mode if it's used. since not sure at this point cause still loading the file we just assume it is.
                    if (New_Enemy.i_Wave_Number > Main_Script.f_Endless_Count)
                    {
                        Main_Script.f_Endless_Count = (float)New_Enemy.i_Wave_Number;
                        
                    }
                    Main_Script.Endless_Enemy_List.Add(New_Enemy);

                }
            }


            

        }

        //make the map with the items in it. We do it a longer way since only done once and will prevent out of index errors.
        GameObject[,] Space_Array = new GameObject[t_Width, t_Height];

        //Can add in the map items now.
        for (int i = 0; i < Level_Items.Length; i++)
        {
            //"Field Object X/Y(" + All_Field_Spots[x, y].transform.GetChild(i).name + "," + x + "," + y;
            if (Level_Items[i].Contains("Field Object X/Y("))
            {
                string Arg1 = Level_Items[i].Split('(')[1].Split(',')[0];
                int Arg2 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[1]);
                int Arg3 = int.Parse(Level_Items[i].Split('(')[1].Split(',')[2]);

                GameObject New_Item;

                //ensure the range is acceptable and the space is empty.
                if (Arg2 < t_Width && Arg2 > -1 && Arg3 < t_Height && Arg3 > -1)
                {
                    if (Space_Array[Arg2, Arg3] == null)
                    {
                        if (Arg1.Contains("Decoration"))
                        {
                            
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Decorations_Location + Arg1)) as GameObject;
                            New_Item.AddComponent<Map_Deco>();
                            New_Item.GetComponent<Map_Deco>().Deco_Setup(Arg1, Arg2, Arg3);
                            Space_Array[Arg2, Arg3] = New_Item;
                            New_Item.tag = Current_Strings.Tag_Decoration;
                        }
                        else if (Arg1.Contains("Start_Point"))
                        {
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Spots_Start)) as GameObject;
                            New_Item.name = Current_Strings.Name_Map_Start;
                            New_Item.tag = Current_Strings.Tag_Start_Spawn;
                            New_Item.AddComponent<Map_Start>();
                            New_Item.GetComponent<Map_Start>().Map_Start_Setup(Current_Strings.Name_Map_Start, Arg2, Arg3);
                            Space_Array[Arg2, Arg3] = New_Item;
                        }
                        else if (Arg1.Contains("Name_Path_Maker"))
                        {
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Spots_Temple)) as GameObject;
                            New_Item.name = Current_Strings.Name_Map_Temple;
                            New_Item.tag = Current_Strings.Tag_Finish_Temple;
                            New_Item.AddComponent<Map_Temple>();
                            New_Item.GetComponent<Map_Temple>().Map_Temple_Setup(Current_Strings.Name_Map_Temple, Arg2, Arg3);
                            Space_Array[Arg2, Arg3] = New_Item;
                        }
                        else if (Arg1.Contains("d_") || Arg1.Contains("w_") || Arg1.Contains("s_") || Arg1.Contains("a_"))
                        {
                            //Paths are special since going to be a few depending on what theme is picked.
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Path_Template)) as GameObject;
                            New_Item.AddComponent<Map_Path>();
                            New_Item.GetComponent<Map_Path>().Map_Path_Setup(Arg1, Arg2, Arg3);
                            Space_Array[Arg2, Arg3] = New_Item;
                            New_Item.tag = Current_Strings.Tag_Path;
                            //Might now work will need to check.
                            Main_Script.Path_List.Add(New_Item.GetComponent<Map_Path>());
                        }
                    }
                    //Map_Deco New_Deco = new Map_Deco();
                }
            }
        }

        //PATH CORNERS here we go through and set up the path corners as needed.
        for (int j = 0; j < Main_Script.Path_List.Count; j++)
        {
            for (int k = 0; k < Main_Script.Path_List.Count; k++)
            {
                if (Main_Script.Path_List[j].Path_Number == Main_Script.Path_List[k].Path_Number - 1)
                {
                    Main_Script.Path_List[k].Set_Corner_Sprite(Main_Script.Path_List[j].Name + Main_Script.Path_List[k].Name);
                }
            }
        }
        

        //Fill the rest of the spots with blanks.
        Transform Map_Parent_Trans = GameObject.Find(Current_Strings.Name_Map_Parent).transform;
        //parent x - (width * box size / 2) + .5 * box size
        float t_x_Offset = Map_Parent_Trans.transform.position.x - (t_Width * f_Default_Box_Size / 2) + (f_Default_Box_Size/2);
        float t_y_Offset = Map_Parent_Trans.transform.position.y - (t_Height * f_Default_Box_Size / 2) + (f_Default_Box_Size / 2);
        for (int x = 0; x < t_Width; x++)
        {
            for (int y = 0; y < t_Height; y++)
            {
                if (Space_Array[x, y] == null)
                {
                    GameObject New_Empty_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Map_Space)) as GameObject;
                    New_Empty_Spot.GetComponent<Map_Space>().X_Spot = x;
                    New_Empty_Spot.GetComponent<Map_Space>().Y_Spot = y;
                    Space_Array[x, y] = New_Empty_Spot;
                }

                //space/move them right here and set parent.
                Space_Array[x, y].transform.position = new Vector2(t_x_Offset + (x * f_Default_Box_Size), (t_y_Offset + (y * f_Default_Box_Size)) * -1);
                Space_Array[x, y].transform.parent = Map_Parent_Trans;
            }
        }

        //Set the array to the main script.
        Main_Script.Map_Spaces = Space_Array;



        //Add in the background to size for the theme used. added more just so it covers the whole map no matter what.
        int t_Total_W = (int)(t_Width * f_Default_Box_Size / 10.24f) + 16;
        int t_Total_H = (int)(t_Height * f_Default_Box_Size / 10.24f) + 16;

        int i_Number_For_Theme = 1;

        for (int i = 0; i < t_Total_W; i++)
        {
            for (int j = 0; j < t_Total_H; j++)
            {
                //create the background item.
                GameObject New_Background = Instantiate(Resources.Load(Current_Strings.Prefab_Theme_Location+s_Theme+"/"+s_Theme+"_"+ i_Number_For_Theme)) as GameObject;
                float Offset_x = (((t_Total_W * 10.24f) / 2) * -1) + 5.12f;
                float Offset_y = (((t_Total_H * 10.24f) / 2) * -1) + 5.12f;
                New_Background.transform.position = new Vector2(Offset_x + (10.24f * i), Offset_y + (10.24f * j));
                New_Background.transform.parent = Map_Parent_Trans;
                

            }
        }


        //Sets the shared items to use.
        if (b_Shared_Energy)
        {
            //WILL ADD IN AFTER WE HAVE THE MAIN MENU SAVING AND PROFILE SAVING DONE.else if (Level_Items[i].Contains("Starting Energy("))
        }
        if (b_Shared_Allies)
        {
            //WILL ADD IN AFTER WE HAVE THE MAIN MENU SAVING AND PROFILE SAVING DONE.else if (Level_Items[i].Contains("Starting Energy("))
        }


        //setting up the pan bounds
        Main_Script.f_x_Bound = (t_Width * f_Default_Box_Size) / 2;
        Main_Script.f_y_Bound = (t_Height * f_Default_Box_Size) / 2;


        //after loading the level we check if it's endless and if so then we load up like 30x endless waves.
        if (Main_Script.b_Endless_Mode)
        {
            for (int w = 0; w < 30; w++)
            {
                Main_Script.Add_For_Endless();
            }
        }

    }


    //creates a vertical menu for the unlocked gems.
    void Setup_Purchase_Menu()
    {
        //Display gem on left(will be idle animation, and have hover over information.) Hover info might include description since it's display.
        //On right will be a buy/confirm button.

        //the parent of the purchase menu.
        GameObject Parent_Object = GameObject.Find(Current_Strings.Name_Purchase_Parent);

        //Left/Right Spots
        GameObject New_Spot_Left;
        GameObject New_Spot_Right;

        bool b_Top_Part_Made = false;

        //used to keep track of how many gems are locked/unlocked.
        int i_Unlocked_Gem_Count = -1;

        //we will make the width 2 spots wide and it's height will be how ever many unlocked towers there are.
        //Towers will be sorted price low to high.
        for (int i = 0; i < Main_Script.Locked_Gems.Locked_Gem_List.Count; i++)
        {
             if (i == 0)
            { 
                //the top left/right.
                New_Spot_Left = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Left)) as GameObject;
                New_Spot_Right = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Right)) as GameObject;
                //make a pruchase box.
                GameObject New_Purchase_Button = Instantiate(Resources.Load(Current_Strings.Prefab_Purchase_Box)) as GameObject;

                New_Purchase_Button.GetComponent<SpriteRenderer>().sortingOrder = 11;
                New_Purchase_Button.transform.parent = Parent_Object.transform;

                New_Spot_Left.GetComponent<SpriteRenderer>().sortingOrder = 10;
                New_Spot_Right.GetComponent<SpriteRenderer>().sortingOrder = 10;

                New_Spot_Left.tag = Current_Strings.Tag_Purchase_Background;
                New_Spot_Right.tag = Current_Strings.Tag_Purchase_Background;

                New_Spot_Left.transform.parent = Parent_Object.transform;
                New_Spot_Right.transform.parent = Parent_Object.transform;

                //set the posistion
                float TxLz = (0 * (New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x)) - ((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float TxRz = (0 * (New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x)) + ((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float Tyz = (1 * (New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y)) + ((New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y) / 2);

                New_Spot_Left.transform.position = new Vector2(TxLz, Tyz);
                New_Spot_Right.transform.position = new Vector2(TxRz, Tyz);
                New_Purchase_Button.transform.position = new Vector2(((TxRz + TxLz)/2), Tyz);
            }

            GameObject New_Tower = null;

            //only make if the tower is not locked.
            if (!Main_Script.Locked_Gems.Locked_Gem_List[i].b_Locked)
            {


                //make a tower for the menu.
                New_Tower = Main_Script.Create_New_Tower(Main_Script.Locked_Gems.Locked_Gem_List[i].Name);
                New_Tower.GetComponent<SpriteRenderer>().sortingOrder = 11;
                New_Tower.tag = Current_Strings.Tag_Tower_Display;
                New_Tower.transform.parent = Parent_Object.transform;
                //set the scale.
                New_Tower.transform.localScale = new Vector2(New_Tower.GetComponent<Tower>().f_Scale_Amount, New_Tower.GetComponent<Tower>().f_Scale_Amount);


                i_Unlocked_Gem_Count++;

               
                New_Spot_Left = Instantiate(Resources.Load(Current_Strings.Prefab_I_Left)) as GameObject;
                New_Spot_Right = Instantiate(Resources.Load(Current_Strings.Prefab_I_Right)) as GameObject;



                //set the layer order.
                New_Spot_Left.GetComponent<SpriteRenderer>().sortingOrder = 10;
                New_Spot_Right.GetComponent<SpriteRenderer>().sortingOrder = 10;


                //set the tag.
                New_Spot_Left.tag = Current_Strings.Tag_Purchase_Background;
                New_Spot_Right.tag = Current_Strings.Tag_Purchase_Background;
                //set the parent.
                New_Spot_Left.transform.parent = Parent_Object.transform;
                New_Spot_Right.transform.parent = Parent_Object.transform;



                //set the posistion
                float TxL = -((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float TxR = ((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float Ty = (-1 * i_Unlocked_Gem_Count * (New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y)) + ((New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y) / 2);

                New_Spot_Left.transform.position = new Vector2(TxL, Ty);
                New_Spot_Right.transform.position = new Vector2(TxR, Ty);

                New_Tower.transform.position = new Vector2(((TxR + TxL) / 2), Ty);
            }

            //Make a set of boxes and since it's the first set we will start off with corner pieces.
            if (i == Main_Script.Locked_Gems.Locked_Gem_List.Count - 1)
            {
                New_Spot_Left = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Left)) as GameObject;
                New_Spot_Right = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Right)) as GameObject;

                //set the layer order.
                New_Spot_Left.GetComponent<SpriteRenderer>().sortingOrder = 10;
                New_Spot_Right.GetComponent<SpriteRenderer>().sortingOrder = 10;


                //set the tag.
                New_Spot_Left.tag = Current_Strings.Tag_Purchase_Background;
                New_Spot_Right.tag = Current_Strings.Tag_Purchase_Background;
                //set the parent.
                New_Spot_Left.transform.parent = Parent_Object.transform;
                New_Spot_Right.transform.parent = Parent_Object.transform;



                //set the posistion
                float TxL = -((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float TxR = ((New_Spot_Left.GetComponent<BoxCollider2D>().size.x * New_Spot_Left.transform.localScale.x) / 2);
                float Ty = (-1 * (i_Unlocked_Gem_Count + 1) * (New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y)) + ((New_Spot_Left.GetComponent<BoxCollider2D>().size.y * New_Spot_Left.transform.localScale.y) / 2);

                New_Spot_Left.transform.position = new Vector2(TxL, Ty);
                New_Spot_Right.transform.position = new Vector2(TxR, Ty);
            }


        }

        //move the parent off screen.
        Parent_Object.transform.position = new Vector2(500, 500);

    }

    void Setup_Fuse_Menu()
    {
        //Display gem on left(will be idle animation, and have hover over information.) Hover info might include description since it's display.
        //On right will be a buy/confirm button.

        //this is the number of tiles across, but double for the abs for the item to give us good x location.
        int Tiles_Across = -3;

        //the parent of the fuse menu.
        GameObject Parent_Object = GameObject.Find(Current_Strings.Name_Fuse_Parent);

        //Left/Right Spots
        GameObject New_Spot_Top;
        GameObject New_Spot_Bottom;

        //make the backgrou d 2x5 for the fuse menu.
        for (int i = Tiles_Across; i < System.Math.Abs(Tiles_Across); i++)
        {

            //for the far most left spot first since it will use different sprites.
            if (i == Tiles_Across)
            {
                //the top left/right.
                New_Spot_Top = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Left)) as GameObject;
                New_Spot_Bottom = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Left)) as GameObject;

                //we will move/place the combine/finish boxes here since it's only don't once.
                GameObject Left_Box = GameObject.Find(Current_Strings.Name_Fuse_Box_01);
                GameObject Right_Box = GameObject.Find(Current_Strings.Name_Fuse_Box_02);
                GameObject Center_Box = GameObject.Find(Current_Strings.Name_Fuse_Box_Combine);

                //place them in the parent and then layer them.
                Left_Box.transform.parent = Parent_Object.transform;
                Left_Box.GetComponent<SpriteRenderer>().sortingOrder = 11;
                Right_Box.transform.parent = Parent_Object.transform;
                Right_Box.GetComponent<SpriteRenderer>().sortingOrder = 11;
                Center_Box.transform.parent = Parent_Object.transform;
                Center_Box.GetComponent<SpriteRenderer>().sortingOrder = 11;

                //place the boxes on the left,right and center of the fuse area.
                float Box_X = (New_Spot_Top.GetComponent<BoxCollider2D>().size.x * New_Spot_Top.transform.localScale.x);
                float Y_Spot = Box_X / 1.3f;
                Box_X = Box_X * i + (Box_X);

                Left_Box.transform.position = new Vector2(Box_X, Y_Spot);
                Right_Box.transform.position = new Vector2(-Box_X, Y_Spot);
                Center_Box.transform.position = new Vector2(0, Y_Spot);

                //the fuse button.
                GameObject Fuse_Button = GameObject.Find(Current_Strings.Name_Fuse_Button_Complete);
                Fuse_Button.transform.parent = Parent_Object.transform;
                Fuse_Button.GetComponent<SpriteRenderer>().sortingOrder = 11;
                Fuse_Button.transform.position = new Vector2(0, 0);



            }

            //Make a set of boxes and since it's the first set we will start off with corner pieces.
            else if (i == System.Math.Abs(Tiles_Across) - 1)
            {
                New_Spot_Top = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Right)) as GameObject;
                New_Spot_Bottom = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Right)) as GameObject;
            }
            else
            {
                New_Spot_Top = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top)) as GameObject;
                New_Spot_Bottom = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot)) as GameObject;
            }





            New_Spot_Top.GetComponent<SpriteRenderer>().sortingOrder = 10;
            New_Spot_Bottom.GetComponent<SpriteRenderer>().sortingOrder = 10;

            New_Spot_Top.tag = Current_Strings.Tag_Fuse_Background;
            New_Spot_Bottom.tag = Current_Strings.Tag_Fuse_Background;

            New_Spot_Top.transform.parent = Parent_Object.transform;
            New_Spot_Bottom.transform.parent = Parent_Object.transform;

            
            

            //set the posistion
            float TxStart = (New_Spot_Top.GetComponent<BoxCollider2D>().size.x * New_Spot_Top.transform.localScale.x);
            TxStart = TxStart * (i+0) + (TxStart / 2);

            float TyT = (New_Spot_Top.GetComponent<BoxCollider2D>().size.x * New_Spot_Top.transform.localScale.x);
            float TyB = TyT - ((New_Spot_Bottom.GetComponent<BoxCollider2D>().size.x * New_Spot_Bottom.transform.localScale.x));

            New_Spot_Top.transform.position = new Vector2(TxStart, TyT);
            New_Spot_Bottom.transform.position = new Vector2(TxStart, TyB);
            
    
        }

        //Move the fuse menu to be away from the center.
        Parent_Object.transform.position = new Vector2(100, 100);
    }

    //make the invintory space for gems to be in.
    void Setup_Inventory_Boxes()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.allCameras[0].orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        //the number of y boxes. Static and set to a number that give a good amount of invintory.
        int i_Inventory_y = 5;
        //used in invintory for the scale.
        float f_Default_Scale = (cameraHeight - f_Hotbox_Size) / (f_Default_Box_Size * i_Inventory_y);
        //the x amount is based on the scale.
        int i_Inventory_x = (int)(cameraWidth / (f_Default_Scale * f_Default_Box_Size));
        //sets up the amount of invintory spaces. this is used for placing in towers that are auto generated.
        Main_Script.i_Invintory_Space_Amount = (i_Inventory_x * i_Inventory_y);

        //get the pices and lay them out for the invintory bag.
        for (int tx = 0; tx < i_Inventory_x; tx++)
        {
            for (int ty = 0; ty < i_Inventory_y; ty++)
            {
                GameObject New_Spot;

                //left
                if (tx == 0)
                {
                    //top left
                    if (ty == 0)
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Left)) as GameObject;
                    }
                    //bot left
                    else if (ty == i_Inventory_y - 1)
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Left)) as GameObject;
                    }
                    //left
                    else
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Left)) as GameObject;
                    }
                }
                //top
                else if (ty == 0 && tx != i_Inventory_x - 1)
                {
                    New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top)) as GameObject;
                }
                //right
                else if (tx == i_Inventory_x - 1)
                {
                    //top right
                    if (ty == 0)
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Top_Right)) as GameObject;
                    }
                    //bot right
                    else if (ty == i_Inventory_y - 1)
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot_Right)) as GameObject;
                    }
                    //right
                    else
                    {
                        New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Right)) as GameObject;
                    }
                }
                //bottom
                else if (ty == i_Inventory_y - 1)
                {
                    New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Bot)) as GameObject;
                }
                //center
                else
                {
                    New_Spot = Instantiate(Resources.Load(Current_Strings.Prefab_I_Empty)) as GameObject;
                }

                //the x/y of the newly created spots.
                float Place_x = (tx * f_Default_Box_Size * f_Default_Scale) + ((((f_Default_Scale * f_Default_Box_Size) / 2) - (cameraWidth / 2) + (cameraWidth - (i_Inventory_x * f_Default_Scale * f_Default_Box_Size)) / 2));
                float Place_y = (cameraHeight / 2) - ((f_Default_Scale * f_Default_Box_Size) / 2) - (ty * f_Default_Box_Size * f_Default_Scale);

                //update scale/posistion/parent
                New_Spot.transform.localScale = new Vector2(f_Default_Scale, f_Default_Scale);
                New_Spot.transform.position = new Vector3(Place_x, Place_y);
                New_Spot.transform.parent = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform;
            }
        }

        //Move the invintory to be away from the center.
        GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.position = new Vector2(100, 100);
    }

    //This will go and create all the Hotboxes and place them all in the correct spots and have them parent to a Hotbar parent.
    void Setup_Hotbar()
    {
        //do math, figure out number of boxes, create, line them up, and have all of them be a parent to a new item called HotBoxParent.
        //get the camera details
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.allCameras[0].orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;
        int i_Max_Boxes = (int)(cameraWidth / f_Hotbox_Size);
        int i_Side_Box_Amount = (i_Max_Boxes - 1) / 2;

        //Make the objects and place them at the correct spots.
        for (int i = 0; i < (i_Side_Box_Amount *2) + 1; i++)
        {
            float tx = (i * f_Hotbox_Size) + ((1.33f) - (cameraWidth / 2)) + ((cameraWidth - (i_Side_Box_Amount * 2 + 1) * f_Hotbox_Size) /2);
            float ty = 1.33f-(cameraHeight / 2);

            //center box
            if (i == i_Side_Box_Amount)
            {
                GameObject Empty_Hotbox = Instantiate(Resources.Load(Current_Strings.Prefab_Hotbar_Middle)) as GameObject;
                Empty_Hotbox.transform.position = new Vector3(tx, ty);
                Empty_Hotbox.transform.parent = GameObject.Find(Current_Strings.Name_Hotbar_Parent).transform;
            }
            //side boxes
            else
            {
                GameObject Empty_Hotbox = Instantiate(Resources.Load(Current_Strings.Prefab_Hotbar_Box)) as GameObject;
                Empty_Hotbox.transform.position = new Vector3(tx, ty);
                Empty_Hotbox.transform.parent = GameObject.Find(Current_Strings.Name_Hotbar_Parent).transform;

                //add the hotbox to the list in the main script.
                Main_Script.Hotbar_Gameobjects.Add(Empty_Hotbox);
            }
        }
    }

    //This sets up the enemy viewer by posistioning the background and view button and getting it ready to use.
    void Setup_Enemy_Viewer()
    {
        //get the camera details
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.allCameras[0].orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        //get the enemy background item we will be changing the size of.
        GameObject Enemy_Background_Viewer = GameObject.Find(Current_Strings.Name_Enemy_Viewer_Background);

        //we want the enemy screen to be 10% of screen width
        float Enemy_Background_Width = cameraWidth * .1f;

        //now we apply a scale to the width and height of the background viewer to make it fill the left side of the screen then move it.
        float Scale_Width = (Enemy_Background_Width / Enemy_Background_Viewer.GetComponent<BoxCollider2D>().size.x);
        float Scale_Height = (cameraHeight / Enemy_Background_Viewer.GetComponent<BoxCollider2D>().size.y);

        Enemy_Background_Viewer.transform.localScale = new Vector2(Scale_Width, Scale_Height);

        //now place it on the left side. the pivot is in the center.
        Enemy_Background_Viewer.transform.position = new Vector2(-cameraWidth/2 - Enemy_Background_Width/2, 0);

        //now move the open/close to the middle right of the background and make it's parent the viewer.
        GameObject Enemy_Button = GameObject.Find(Current_Strings.Name_Enemy_Viewer_Button);
        //first move into place.
        Enemy_Button.transform.position = new Vector2(-cameraWidth / 2 + (Enemy_Button.GetComponent<BoxCollider2D>().size.x / 2), 0);
        //set the parent.
        Enemy_Button.transform.parent = Enemy_Background_Viewer.transform;
        //now need to adjust the scale since we don't want to use the parent's scale.
        Enemy_Button.transform.localScale = new Vector2(Enemy_Button.transform.localScale.x / Enemy_Background_Viewer.transform.localScale.x, Enemy_Button.transform.localScale.y / Enemy_Background_Viewer.transform.localScale.y);


        //now the hard part of placing in enemies. Could either place all of them in.. but for endless mode it would not work... 
        //so maybe just load up 50 waves and future vision can't go past a certain amount.. so each wave kill it updates.
        //or just load them up when it's opened and only show what future vision shows. then only update when the future vision number changes or a wave is complete.
        //thinking i will go with the last one so look in the button opening and update for it calling the code to place in the sprites/info.

        //Debug.Log("EEE::" + Enemy_Background_Width);


    }
}
