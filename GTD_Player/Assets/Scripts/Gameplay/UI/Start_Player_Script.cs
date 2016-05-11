using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        //loads the level we are playing.
        Load_Level_Save();
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
                Main_Script.i_Energy = int.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Shared Enery("))
            {
                b_Shared_Energy = bool.Parse(Level_Items[i].Split('(')[1]);
            }
            else if (Level_Items[i].Contains("Starting HP("))
            {
                Main_Script.i_HP = int.Parse(Level_Items[i].Split('(')[1]);
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
                            Main_Script.Locked_Gems.Locked_Gem_List[j].Cost = int.Parse(Arg3);
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

                    //WE WILL SET UP THE TOWERS LATER, FOR NOW JUST ADDING THEM.

                    //tower not null, we add it to the list and move it to invintory.
                    if (go_New_Tower != null)
                    {
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
                    GameObject New_Temp = new GameObject();
                    //New_Temp.transform.position = new Vector3(-500, -500);
                    //New_Temp.AddComponent<Enemy>();

                    //Enemy New_Enemy = New_Temp.GetComponent<Enemy>();
                    Enemy New_Enemy = new Enemy();
                    //set up the new enemy as a spawner.
                    New_Enemy.Set_Enemy(Arg1, Arg2, Arg3, Arg4, Arg5, Arg6, Arg7, Arg8, Arg9, Arg10,true);

                    //might need to create the item so it's not null and have it off screen.
                    New_Enemy.i_Location_In_Spawn_Array = Main_Script.Enemy_Spawns.Count;
                    Main_Script.Enemy_Spawns.Add(New_Enemy);
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
                        }
                        else if (Arg1.Contains("Start_Point"))
                        {
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Spots_Start)) as GameObject;
                            New_Item.name = Current_Strings.Name_Map_Start;
                            New_Item.AddComponent<Map_Start>();
                            New_Item.GetComponent<Map_Start>().Map_Start_Setup(Current_Strings.Name_Map_Start, Arg2, Arg3);
                            Space_Array[Arg2, Arg3] = New_Item;
                        }
                        else if (Arg1.Contains("Name_Path_Maker"))
                        {
                            New_Item = Instantiate(Resources.Load(Current_Strings.Prefab_MI_Spots_Temple)) as GameObject;
                            New_Item.name = Current_Strings.Name_Map_Temple;
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

        //Sets the shared items to use.
        if (b_Shared_Energy)
        {
            //WILL ADD IN AFTER WE HAVE THE MAIN MENU SAVING AND PROFILE SAVING DONE.else if (Level_Items[i].Contains("Starting Energy("))
        }
        if (b_Shared_Allies)
        {
            //WILL ADD IN AFTER WE HAVE THE MAIN MENU SAVING AND PROFILE SAVING DONE.else if (Level_Items[i].Contains("Starting Energy("))
        }


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
            }
        }
    }
}
