﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Gameplay.Level_Items;
using Assets.Scripts.Gameplay.Generic;

public class Player_Main_Script : MonoBehaviour {

    public String_Tracker Current_Strings = new String_Tracker();

    //This is if the game and all objects are paused.
    public bool b_Is_Running = true;
    public bool b_Is_Not_Paused = true;

    bool b_Was_Paused = false;
    //spawn/enemy items.
    float f_Spawn_Timer = 0;
    bool b_Wave_Transistion = false;
    int i_Transistion_Time = 2;
    int i_Wave_Number = 0;

    //Level variables
    public string s_Level_Name = "";
    public float f_Energy = 0;
    //public float f_Max_Energy = 100;
    public int i_Max_HP = 0;
    public float f_HP = 0;

    public int i_Invintory_Space_Amount = 0;

    //zoom info
    public float f_Zoom_Level = 1;
    public float f_Zoom_Max = 3;
    public float f_Zoom_Min = .3f;

    //pan bounds info. what is the min/max the center can go to on x/y.
    public float f_x_Bound = 0;
    public float f_y_Bound = 0;

    //exp stuff
    public bool Tower_Change = true;
    public float f_Exp_Gathered = 0;
    List<Tower> Towers_On_Field = new List<Tower>();

    //for confirmation button.
    public bool b_Confirmation_Open = false;
    public bool b_Confirmation_True = true;
    public GameObject[] Confirmation_Objects;
    public string s_Confirmation_Action = "none";


    //Lists and arrays.
    List<Enemy> Enemy_Spawns = new List<Enemy>();
    public List<Enemy> Enemies_On_Field = new List<Enemy>();
    public List<GameObject> Tower_List = new List<GameObject>();
    public All_Locked_Gems Locked_Gems = new All_Locked_Gems();
    public GameObject[,] Map_Spaces;
    public List<Map_Path> Path_List = new List<Map_Path>();

    //the hover collider
    public Collider2D Hover_Collider = null;
    //Tower click items.
    public Collider2D Tower_Open_Collider = null;
    public bool b_Tower_Open = false;

    //enemy viewer open or now.
    bool b_Enemy_Viewer_Open = false;
    //the amount of enemies the user can view.
    int i_Enemy_Viewer_Count = 0;


    //This is a list of the hotbar gameobjects. used for when fusing to ensure gems go back to an empty spot.
    public List<GameObject> Hotbar_Gameobjects = new List<GameObject>();
    


    // Use this for initialization
    void Start () {
	    
	}

   
	
	// Update is called once per frame
	void Update () {

        //confirmation box is up so we pause.
        if (b_Confirmation_Open)
        {
            b_Is_Running = false;
        }
        //triggers when confirmation has been used and has an action.
        if (!b_Confirmation_Open && s_Confirmation_Action != "none")
        {
            Confirmation_Process();
            //un-pause the game since confirm box closed.
            b_Is_Running = true;
        }
       

        //we perform hover check.
        Hover_Box();

        //pause check.
        if (b_Is_Running)
        {
            //check if it was paused.
            if (b_Was_Paused)
            {
                b_Was_Paused = false;
            }

            f_Spawn_Timer += Time.deltaTime;
            Update_Spawn();
        }
        else
        {
            //Debug.Log(f_Spawn_Timer);
            //Debug.Log(Enemy_Spawns[0].s_Name);
            //lets us know that it was paused.
            b_Was_Paused = true;
        }


        //tower is clicked/open.
        if (b_Tower_Open && Tower_Open_Collider != null)
        {
            Tower_Open_Stats();
        }
        else if (!b_Tower_Open && Tower_Open_Collider != null)
        {
            Tower_Close_Stats();
        }

        //perform exp stuff
        Exp_Handler();
    }

    //This gives each tower exp as fit and tells them when to level.
    void Exp_Handler()
    {
        //find out how many towers are on the field.
        if (Tower_Change)
        {
            Towers_On_Field.Clear();
            for (int i = 0; i < Tower_List.Count; i++)
            {
                if (Tower_List[i].tag.Contains(Current_Strings.Tag_Tower_On_Map))
                {
                    //if the tower is not max level, we don't count max level towers.
                    if (Tower_List[i].GetComponent<Tower>().i_Level < Tower_List[i].GetComponent<Tower>().i_Max_Level)
                    {
                        Towers_On_Field.Add(Tower_List[i].GetComponent<Tower>());
                    }
                }
            }
        }

        //assign out the exp if there are towers on the field.
        if (Towers_On_Field.Count > 0)
        {
            //Debug.Log("Exp:" + f_Exp_Gathered);
            List<Tower> Remove_These_From_Field_List_Since_Max_Level = new List<Tower>();

            while (f_Exp_Gathered / Towers_On_Field.Count >= 1)
            {
                for (int i = 0; i < Towers_On_Field.Count; i++)
                {
                    //check if the tower is max level, can only happen if the tower leveled to max level during this kill.
                    if (Towers_On_Field[i].i_Level < Towers_On_Field[i].i_Max_Level)
                    {
                        Towers_On_Field[i].i_exp += 1;
                        //Debug.Log(Towers_On_Field[i].i_exp);
                        if (Towers_On_Field[i].i_exp == Towers_On_Field[i].i_Exp_Level[Towers_On_Field[i].i_Level])
                        {
                            //tell the tower to level up.
                            Towers_On_Field[i].Level_Up();

                            //for when a tower reaches max level and still on the field.
                            if (Towers_On_Field[i].i_Level == Towers_On_Field[i].i_Max_Level)
                            {
                                Remove_These_From_Field_List_Since_Max_Level.Add(Towers_On_Field[i]);
                            }
                        }
                    }
                }
                
                //remove the exp gathered.
                f_Exp_Gathered -= Towers_On_Field.Count;
            }

            //we remove the towers that maxed out, should not happen often, but will prevent any errors if the user were to level up again.
            for (int j = 0; j < Remove_These_From_Field_List_Since_Max_Level.Count; j++)
            {
                Towers_On_Field.Remove(Remove_These_From_Field_List_Since_Max_Level[j]);
            }
        }


    }
    //this will close/hide the stats and background items.
    void Tower_Close_Stats()
    {
        //move the background.
        GameObject.Find(Current_Strings.Name_Fade_Background).transform.position = new Vector2(500, 500);
        //move the icons.
        GameObject.Find(Current_Strings.Name_Plus_Icons).transform.position = new Vector2(500, 500);

        Tower_Open_Collider = null;
    }
    //This will open/display the stats.
    void Tower_Open_Stats()
    {
        GameObject Hover_Text = GameObject.Find(Current_Strings.Name_Hover_Click_Text);

        if (Tower_Open_Collider != null)
        {
            //move the background.
            GameObject.Find(Current_Strings.Name_Fade_Background).transform.position = new Vector2(0, 0);

            GameObject Hover_This = Tower_Open_Collider.gameObject;
            //set the text
            string Text_To_Place = "";

            if (Hover_This.tag.Contains(Current_Strings.Tag_Tower_Drag))
            {
                

                Text_To_Place += "Name: " + Hover_This.GetComponent<Tower>().s_Name + "\n";
                Text_To_Place += "Level: " + Hover_This.GetComponent<Tower>().i_Level + "/" + Hover_This.GetComponent<Tower>().i_Max_Level + "\n";
                Text_To_Place += "Points: " + Hover_This.GetComponent<Tower>().i_Spending_Points + "\n";
                Text_To_Place += "    Cooldown: " + Hover_This.GetComponent<Tower>().f_Speed_Amount + " >> " + (Hover_This.GetComponent<Tower>().f_Speed_Upgrade+Hover_This.GetComponent<Tower>().f_Speed_Amount) + "\n";
                Text_To_Place += "    Power: " + Hover_This.GetComponent<Tower>().f_Power_Amount + " >> " + (Hover_This.GetComponent<Tower>().f_Power_Upgrade + Hover_This.GetComponent<Tower>().f_Power_Amount) + "\n";
                Text_To_Place += "    Range: " + Hover_This.GetComponent<Tower>().f_Range_Amount + " >> " + (Hover_This.GetComponent<Tower>().f_Range_Upgrade + Hover_This.GetComponent<Tower>().f_Range_Amount) + "\n";
                Text_To_Place += "Xp: " + Hover_This.GetComponent<Tower>().i_exp + "/" + Hover_This.GetComponent<Tower>().i_Exp_Level[Hover_This.GetComponent<Tower>().i_Level];
                

            }
            else
            {
                //some mistake happened and we entered this without being over a tower and clicking it. reset.
                b_Tower_Open = false;
                Debug.Log("Tower Click Failed");
            }


            if (Text_To_Place != "")
            {
                Hover_Text.GetComponent<TextMesh>().text = Text_To_Place;
                Hover_Text.GetComponent<Text_Box_Background>().b_Is_Enabled = true;

                //Have to do this.. why.. not sure, can't find out where it is being enabled or how after i remove/add it in it's script.
                Hover_Text.GetComponent<BoxCollider2D>().enabled = false;


                //now need to find if there is more space to the left or right the unit for placing the hover box.
                Vector2 Box_Placement = Hover_This.transform.position;
                Vector2 GOpix = Camera.main.WorldToScreenPoint(Hover_This.transform.position);

                float Bx1 = (Hover_This.GetComponent<BoxCollider2D>().size.x / 2) * Hover_This.transform.localScale.x;
                float By1 = (Hover_This.GetComponent<BoxCollider2D>().size.y / 2) * Hover_This.transform.localScale.y;

                //x is bigger than half the screen we place it on the right.
                if (GOpix.x < ((float)Screen.width / 2))
                {
                    Box_Placement.x += Bx1;
                }
                //else it goes on the left side.
                else
                {
                    Box_Placement.x -= Bx1 + ((Hover_Text.GetComponent<BoxCollider2D>().size.x) * Hover_Text.transform.localScale.x);//Bx1 + 0;
                }

                //set up the y spot.

                if (GOpix.y > ((float)Screen.height / 2))
                {
                    Box_Placement.y -= By1;//((Hover_Text.GetComponent<BoxCollider2D>().size.y) * Hover_Text.transform.localScale.y);
                }
                //else it goes on the left side.
                else
                {
                    Box_Placement.y += By1 + ((Hover_Text.GetComponent<BoxCollider2D>().size.y) * Hover_Text.transform.localScale.y);
                }

                Hover_Text.transform.position = Box_Placement;
                //place the plus icons and such.
                GameObject.Find(Current_Strings.Name_Plus_Icons).transform.position = Hover_Text.transform.position;
            }
        }
        else
        {
            Hover_Text.GetComponent<Text_Box_Background>().b_Is_Enabled = false;
        }
    }

    public void Plus_Icon_Clicked(Collider2D Icon_Clicked)
    {
        //first make sure there is a point to spend.
        if (Tower_Open_Collider != null)
        {
            Tower Working_With = Tower_Open_Collider.GetComponent<Tower>();

            if (Working_With.i_Spending_Points > 0)
            {
                if (Icon_Clicked.gameObject.name.Contains("Range"))
                {
                    Working_With.f_Range_Amount += Working_With.f_Range_Upgrade;
                    Working_With.i_Spending_Points -= 1;
                }
                else if (Icon_Clicked.gameObject.name.Contains("Speed"))
                {
                    Working_With.f_Speed_Amount += Working_With.f_Speed_Upgrade;
                    Working_With.i_Spending_Points -= 1;
                }
                else if (Icon_Clicked.gameObject.name.Contains("Power"))
                {
                    Working_With.f_Power_Amount += Working_With.f_Power_Upgrade;
                    Working_With.i_Spending_Points -= 1;
                }
            }
        }
    }

    //show/hide the enemy viewer.
    public void Enemy_Viewer_Clicked()
    {
        GameObject Enemy_Viewer = GameObject.Find(Current_Strings.Name_Enemy_Viewer_Background);
        //first check if it's showing or now.
        if (b_Enemy_Viewer_Open)
        {
            //set it to false and close it.
            b_Enemy_Viewer_Open = false;
            Enemy_Viewer.transform.position = new Vector2(Enemy_Viewer.transform.position.x - Enemy_Viewer.GetComponent<BoxCollider2D>().size.x * Enemy_Viewer.transform.localScale.x, 0);
        }
        else
        {
            //it's closed so we update the enemy stuff and set it to open the move to open.
            b_Enemy_Viewer_Open = true;
            Update_Enemy_Viewer();
            //now move it out to be visible.
            Enemy_Viewer.transform.position = new Vector2(Enemy_Viewer.transform.position.x + Enemy_Viewer.GetComponent<BoxCollider2D>().size.x * Enemy_Viewer.transform.localScale.x, 0);
        }
    }

    //this will update the viewer showing what it should.
    void Update_Enemy_Viewer()
    {

    }

    //This is fired when we can hover a box over a gameobject. the object is passed.
    void Hover_Box()
    {
        GameObject Hover_Text = GameObject.Find(Current_Strings.Name_Hover_Click_Text);

        if (Hover_Collider != null)
        {
            GameObject Hover_This = Hover_Collider.gameObject;
            //set the text
            string Text_To_Place = "";

            //first we determin if it's a tower or a enemy.
            if (Hover_This.tag.Contains(Current_Strings.Tag_Tower_Drag))
            {
                Text_To_Place += "(L" + Hover_This.GetComponent<Tower>().i_Level + ")" + Hover_This.GetComponent<Tower>().s_Name + "\n";
                Text_To_Place += "Cooldown: " + Hover_This.GetComponent<Tower>().f_Speed_Amount + "\n";
                Text_To_Place += "Power: " + Hover_This.GetComponent<Tower>().f_Power_Amount + "\n";
                Text_To_Place += "Range: " + Hover_This.GetComponent<Tower>().f_Range_Amount + "\n";
                Text_To_Place += "Xp: " + Hover_This.GetComponent<Tower>().i_exp + "/" + Hover_This.GetComponent<Tower>().i_Exp_Level[Hover_This.GetComponent<Tower>().i_Level] + "\n";
                Text_To_Place += "Points: " + Hover_This.GetComponent<Tower>().i_Spending_Points;

            }
            else if (Hover_This.tag.Contains(Current_Strings.Tag_Tower_Display))
            {
                Text_To_Place += "Name:" + Hover_This.GetComponent<Tower>().s_Name + "\n";
                Text_To_Place += "Cost:" + Hover_This.GetComponent<Tower>().i_Cost + "\n";
                Text_To_Place += "Base Cooldown: " + Hover_This.GetComponent<Tower>().f_Speed_Amount + "::Added Per Point: " + Hover_This.GetComponent<Tower>().f_Speed_Upgrade + "\n";
                Text_To_Place += "Base Power: " + Hover_This.GetComponent<Tower>().f_Power_Amount + "::Added Per Point: " + Hover_This.GetComponent<Tower>().f_Power_Upgrade + "\n";
                Text_To_Place += "Base Range: " + Hover_This.GetComponent<Tower>().f_Range_Amount + "::Added Per Point: " + Hover_This.GetComponent<Tower>().f_Range_Upgrade + "\n";
                Text_To_Place += Hover_This.GetComponent<Tower>().s_Short_Description;
            }
            else if (Hover_This.tag.Contains(Current_Strings.Tag_Enemy))
            {
                Text_To_Place += Hover_This.GetComponent<Enemy>().s_Name + "\n";
                Text_To_Place += "Cooldown: " + Hover_This.GetComponent<Enemy>().f_Speed + "\n";
                Text_To_Place += "Power: " + Hover_This.GetComponent<Enemy>().f_Power + "\n";
                Text_To_Place += "Range: " + Hover_This.GetComponent<Enemy>().f_Range + "\n";
                Text_To_Place += "HP: " + Hover_This.GetComponent<Enemy>().f_HP + "/" + Hover_This.GetComponent<Enemy>().f_Max_HP;
            }
            else
            {
                Hover_Text.GetComponent<Text_Box_Background>().b_Is_Enabled = false;
            }

            if (Text_To_Place != "")
            {
                Hover_Text.GetComponent<TextMesh>().text = Text_To_Place;
                Hover_Text.GetComponent<Text_Box_Background>().b_Is_Enabled = true;

                //Have to do this.. why.. not sure, can't find out where it is being enabled or how after i remove/add it in it's script.
                Hover_Text.GetComponent<BoxCollider2D>().enabled = false;


                //now need to find if there is more space to the left or right the unit for placing the hover box.
                Vector2 Box_Placement = Hover_This.transform.position;
                Vector2 GOpix = Camera.main.WorldToScreenPoint(Hover_This.transform.position);


                //this is to get the map scale if hovering in the map area. right now using what layer they are on, anything less than 30 is map for now.
                float f_tScale = 1;
                if (Hover_This.GetComponent<SpriteRenderer>().sortingOrder < 30)
                {
                    f_tScale = f_Zoom_Level;
                }

                float Bx1 = (Hover_This.GetComponent<BoxCollider2D>().size.x / 2)* Hover_This.transform.localScale.x * f_tScale;
                float By1 = (Hover_This.GetComponent<BoxCollider2D>().size.y / 2)* Hover_This.transform.localScale.y * f_tScale;
                //Debug.Log(Bx1);
                //x is bigger than half the screen we place it on the right.
                if (GOpix.x < ((float)Screen.width / 2))
                {
                    Box_Placement.x += Bx1;
                }
                //else it goes on the left side.
                else
                {
                    Box_Placement.x -= Bx1 + ((Hover_Text.GetComponent<BoxCollider2D>().size.x));// * Hover_Text.transform.localScale.x);//Bx1 + 0;
                }

                //set up the y spot.

                if (GOpix.y > ((float)Screen.height / 2))
                {
                    Box_Placement.y -= By1;
                }
                //else it goes on the left side.
                else
                {
                    Box_Placement.y += By1 + ((Hover_Text.GetComponent<BoxCollider2D>().size.y));// * Hover_Text.transform.localScale.y);
                }

               

                Hover_Text.transform.position = Box_Placement;
            }
        }
        else
        {
            Hover_Text.GetComponent<Text_Box_Background>().b_Is_Enabled = false;
        }
    }


    //we create the tower and return that tower that is created.
    public GameObject Create_New_Tower(string Tower_Name)
    {
        //This is the main script that holds all the information.
        GameObject go_New_Tower = null;
        //gets all the information from the locked list. that's where we will grab it and just pass it the name. don't need to set it up ehre.
        for (int i = 0; i < Locked_Gems.Locked_Gem_List.Count; i++)
        {
            if (Locked_Gems.Locked_Gem_List[i].Name == Tower_Name)
            {
                //found a match so we create the tower and set it up.
                go_New_Tower = Instantiate(Resources.Load(Locked_Gems.Locked_Gem_List[i].s_Prefab_Location)) as GameObject;
                go_New_Tower.AddComponent<Tower>();
                Tower New_Tower = go_New_Tower.GetComponent<Tower>();

                New_Tower.s_Name = Locked_Gems.Locked_Gem_List[i].Name;
                New_Tower.s_Attack_Name = Locked_Gems.Locked_Gem_List[i].Attack_Name;

                //set up the bullet/attack prefab here.
                New_Tower.s_Bullet_Prefab = Locked_Gems.Locked_Gem_List[i].s_Bullet_Prefab_Location;

                New_Tower.i_Exp_Level = Locked_Gems.Locked_Gem_List[i].i_Exp_Level;
                New_Tower.f_Power_Upgrade = Locked_Gems.Locked_Gem_List[i].f_Power_Levels;
                New_Tower.f_Power_Amount = Locked_Gems.Locked_Gem_List[i].f_Power_Amount;
                New_Tower.f_Range_Upgrade = Locked_Gems.Locked_Gem_List[i].f_Range_Levels;
                New_Tower.f_Range_Amount = Locked_Gems.Locked_Gem_List[i].f_Range_Amount;
                New_Tower.f_Speed_Upgrade = Locked_Gems.Locked_Gem_List[i].f_Speed_Levels;
                New_Tower.f_Speed_Amount = Locked_Gems.Locked_Gem_List[i].f_Speed_Amount;
                New_Tower.f_Scale_Amount = Locked_Gems.Locked_Gem_List[i].f_Scale_Amount;
                New_Tower.i_Cost = Locked_Gems.Locked_Gem_List[i].i_Cost;
                New_Tower.s_Short_Description = Locked_Gems.Locked_Gem_List[i].s_Desc;

                //go_New_Tower.transform.localScale = new Vector2(New_Tower.f_Scale_Amount, New_Tower.f_Scale_Amount);
            }
        }
        //return the new tower that is created. we don't add since can be created during load or later when buying.
        return go_New_Tower;
    }

    //we create the tower and return that tower that is created.
    public GameObject Spawn_Enemy(string Enemy_Name) //NEED TO ADD IN STATS HERE.
    {
        //This is the main script that holds all the information.
        GameObject go_New_Enemy = null;

        /*
        //found a match so we create the tower and set it up.
        go_New_Enemy = Instantiate(Resources.Load(Locked_Gems.Locked_Gem_List[i].s_Prefab_Location)) as GameObject;
        go_New_Enemy.AddComponent<Tower>();
                Tower New_Tower = go_New_Enemy.GetComponent<Tower>();

                New_Tower.s_Name = Locked_Gems.Locked_Gem_List[i].Name;
                New_Tower.i_Exp_Level = Locked_Gems.Locked_Gem_List[i].i_Exp_Level;
                New_Tower.i_Power_Levels = Locked_Gems.Locked_Gem_List[i].i_Power_Levels;
                New_Tower.i_Power_Amount = Locked_Gems.Locked_Gem_List[i].i_Power_Amount;
                New_Tower.i_Range_Levels = Locked_Gems.Locked_Gem_List[i].i_Range_Levels;
                New_Tower.i_Speed_Amount = Locked_Gems.Locked_Gem_List[i].i_Range_Amount;
                New_Tower.i_Speed_Levels = Locked_Gems.Locked_Gem_List[i].i_Speed_Levels;
                New_Tower.i_Speed_Amount = Locked_Gems.Locked_Gem_List[i].i_Speed_Amount;
        
    */
        //return the enemy we just created.
        return go_New_Enemy;
    }


    //This adds an enemy to the spawn list and will return what location the newly created enemy is. well returns how many.
    public int Add_To_Enemy_Spawns(Enemy Passed)
    {
        Enemy_Spawns.Add(Passed);
        return Enemy_Spawns.Count;
    }

    //This will update the enemies and spawn them in if needed.
    void Update_Spawn()
    {
        //this lets us know if we need to go to the next wave.
        bool b_none_active = true;

        //go through all of the spawners and check if any need to update their spawn.
        for (int i = 0; i < Enemy_Spawns.Count; i++)
        {
            //check for wave numbers.
            if (Enemy_Spawns[i].i_Wave_Number == i_Wave_Number)
            {
                //check the spawn amount.
                if (Enemy_Spawns[i].i_Amount_Generated < Enemy_Spawns[i].i_Amount)
                {
                    //tell this spawner to spawn.
                    Enemy_Spawns[i].Spawn_Enemy();
                    //will set none to active as false since this one still is neededing to spawn more.
                    b_none_active = false;
                }
            }
        }

        //check transistion times and set up next wave.
        if (b_none_active && !b_Wave_Transistion)
        {
            b_Wave_Transistion = true;
            f_Spawn_Timer = 0;
        }
        else if (b_none_active && b_Wave_Transistion)
        {
            if (f_Spawn_Timer > i_Transistion_Time)
            {
                f_Spawn_Timer = 0;
                i_Wave_Number++;
                b_Wave_Transistion = false;
            }
        }


    }



    //processes the confirmation when it's accepted or declined.
    //The logic of what happens when something is confirmed happens here.
    void Confirmation_Process()
    {
        //regaurdless we need to move the box back to it's default spot.
        GameObject.Find(Current_Strings.Name_Confirmation_Box).transform.position = new Vector3(500, 500);

        if (s_Confirmation_Action == Current_Strings.Confirm_Tower_To_Field)
        {
            //Move the tower to the spot on the field.
            if (b_Confirmation_True)
            {
                //need to enable tower and move the main item.
                Confirmation_Objects[1].GetComponent<Tower>().b_On_Field = true;
                //set timer back to 0.
                Confirmation_Objects[1].GetComponent<Tower>().f_Timer = 0;
                //set target to null
                Confirmation_Objects[1].GetComponent<Tower>().The_Target = null;
                //set up the tag.
                Confirmation_Objects[1].tag = Current_Strings.Tag_Tower_On_Map;
                

                //Ghost and object
                Confirmation_Objects[1].transform.position = Confirmation_Objects[2].transform.position;
                Confirmation_Objects[1].transform.parent = Confirmation_Objects[2].transform;
                Confirmation_Objects[1].transform.localScale = new Vector2(Confirmation_Objects[1].GetComponent<Tower>().f_Scale_Amount, Confirmation_Objects[1].GetComponent<Tower>().f_Scale_Amount);
                Confirmation_Objects[1].GetComponent<Animator>().SetBool("Attacking", false);

                Confirmation_Objects[1].transform.GetChild(0).position = Confirmation_Objects[2].transform.position;
                //Confirmation_Objects[1].transform.GetChild(0).parent = Confirmation_Objects[1].transform;

                //sprite render stuff.
                Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[2].GetComponent<SpriteRenderer>().sortingOrder + 2;
                //set the color to invis for the ghost..
                Confirmation_Objects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                //Confirmation_Objects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder

                Tower_Change = true;

            }
            //return the tower to the former location.
            else
            {
                //ONLY THE GHOST.
                Confirmation_Objects[1].transform.GetChild(0).position = Confirmation_Objects[1].transform.position;
                //Confirmation_Objects[1].transform.GetChild(0).parent = Confirmation_Objects[1].transform;
                //sprite render stuff.
                //Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[0].GetComponent<SpriteRenderer>().sortingOrder + 2;
                //set the color to invis for the ghost..
                Confirmation_Objects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }

        if (s_Confirmation_Action == Current_Strings.Confirm_Tower_To_Hotbar)
        {
            //Moving tower from field to hotspot.
            if (b_Confirmation_True)
            {
                //need to disable tower
                Confirmation_Objects[1].GetComponent<Tower>().b_On_Field = false;
                //set up the tag.
                Confirmation_Objects[1].tag = Current_Strings.Tag_Tower_Drag;

                //Ghost and object
                Confirmation_Objects[1].transform.position = Confirmation_Objects[2].transform.position;
                Confirmation_Objects[1].transform.parent = Confirmation_Objects[2].transform;
                Confirmation_Objects[1].transform.localScale = new Vector2(Confirmation_Objects[1].GetComponent<Tower>().f_Scale_Amount, Confirmation_Objects[1].GetComponent<Tower>().f_Scale_Amount);
                Confirmation_Objects[1].GetComponent<Animator>().SetBool("Attacking", false);

                Confirmation_Objects[1].transform.GetChild(0).position = Confirmation_Objects[2].transform.position;
                //Confirmation_Objects[1].transform.GetChild(0).parent = Confirmation_Objects[1].transform;

                //sprite render stuff.
                Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[2].GetComponent<SpriteRenderer>().sortingOrder + 2;
                //set the color to invis for the ghost..
                Confirmation_Objects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

                Tower_Change = true;
            }
            //return the ghost to the correct location.
            else
            {
                //ONLY THE GHOST.
                Confirmation_Objects[1].transform.GetChild(0).position = Confirmation_Objects[1].transform.position;
                //Confirmation_Objects[1].transform.GetChild(0).parent = Confirmation_Objects[1].transform;
                //sprite render stuff.
                //Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[0].GetComponent<SpriteRenderer>().sortingOrder + 2;
                //set the color to invis for the ghost..
                Confirmation_Objects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

            }
        }

        //puchasing a tower. it passes [Tower//Hotspot]
        if (s_Confirmation_Action == Current_Strings.Confirm_Tower_Purchase)
        {
            //Moving tower from field to hotspot.
            if (b_Confirmation_True)
            {

                //HERE WE PLACE IN MONEY CHECK WHEN IT IS PLACED IN.



                //set the ghsot back to invis.
                Confirmation_Objects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);


                //create a new tower.
                GameObject New_Tower = Create_New_Tower(Confirmation_Objects[0].GetComponent<Tower>().s_Name);
                //need to disable tower
                New_Tower.GetComponent<Tower>().b_On_Field = false;
                //set posistion.
                New_Tower.transform.position = Confirmation_Objects[1].transform.position;
                //set parent.
                New_Tower.transform.parent = Confirmation_Objects[1].transform;
                //set scale.
                New_Tower.transform.localScale = new Vector2(New_Tower.GetComponent<Tower>().f_Scale_Amount, New_Tower.GetComponent<Tower>().f_Scale_Amount);
                //sprite render stuff.
                New_Tower.GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder + 2;
                //set up the tag.
                New_Tower.tag = Current_Strings.Tag_Tower_Drag;
                //Set Animation.
                New_Tower.GetComponent<Animator>().SetBool("Attacking", false);

                //Add the tower to the tower list.
                Tower_List.Add(New_Tower);


            }
            //return the ghost to the correct location.
            else
            {
                //set the ghost back to invis.
                Confirmation_Objects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }

        //FUSING GEMS
        if (s_Confirmation_Action == Current_Strings.Confirm_Tower_Fuse)
        {
            //Moving tower from field to hotspot.
            if (b_Confirmation_True)
            {

                //now we need to create a new tower with the merged stats and then remove the old two.
                //place the new tower in the center area.


                //need to perfrom MONEY check here.

                //create the new tower
                GameObject New_Tower = Create_New_Tower(Is_Fuse_Possible());


                //get the two fuses for reference and to remove after making the new tower.
                GameObject Fuse_01 = null;
                GameObject Fuse_02 = null;

                //first get the two items and make sure there are two, if not return no.
                if (GameObject.Find(Current_Strings.Name_Fuse_Box_01).transform.childCount > 0)
                {
                    Fuse_01 = GameObject.Find(Current_Strings.Name_Fuse_Box_01).transform.GetChild(0).gameObject;
                }
                if (GameObject.Find(Current_Strings.Name_Fuse_Box_02).transform.childCount > 0)
                {
                    Fuse_02 = GameObject.Find(Current_Strings.Name_Fuse_Box_02).transform.GetChild(0).gameObject;
                }

                //ensure there is a tower.
                if (New_Tower != null)
                {
                    //need to disable tower
                    New_Tower.GetComponent<Tower>().b_On_Field = false;
                    //set posistion.
                    New_Tower.transform.position = GameObject.Find(Current_Strings.Name_Fuse_Box_Combine).transform.position;
                    //set parent.
                    New_Tower.transform.parent = GameObject.Find(Current_Strings.Name_Fuse_Box_Combine).transform;
                    //set scale.
                    New_Tower.transform.localScale = new Vector2(New_Tower.GetComponent<Tower>().f_Scale_Amount, New_Tower.GetComponent<Tower>().f_Scale_Amount);
                    //sprite render stuff.
                    New_Tower.GetComponent<SpriteRenderer>().sortingOrder = GameObject.Find(Current_Strings.Name_Fuse_Box_Combine).GetComponent<SpriteRenderer>().sortingOrder + 2;
                    //set up the tag.
                    New_Tower.tag = Current_Strings.Tag_Tower_Drag;
                    //Set Animation.
                    New_Tower.GetComponent<Animator>().SetBool("Attacking", false);

                    //Add the tower to the tower list.
                    Tower_List.Add(New_Tower);

                    //we remove the old towers.
                    Tower_List.Remove(Fuse_01);
                    Tower_List.Remove(Fuse_02);
                    GameObject.Destroy(Fuse_01);
                    GameObject.Destroy(Fuse_02);
                }
            }
            //return the ghost to the correct location.
            else
            {

            }
        }


        //reset the confirmation message to "none"
        s_Confirmation_Action = "none";
        


    }

    public void Attempt_Fuse()
    {
        string Fuse_Check_Answer = Is_Fuse_Possible();

        //check if a fuse can even be done.
        if (Fuse_Check_Answer != "No")
        {
            //ensure that there is not a gem/item in the complete box first.
            if (GameObject.Find(Current_Strings.Name_Fuse_Box_Combine).transform.childCount == 0)
            {
                //ALSO NEED TO CHECK IF YOU HAVE ENOUGH MONEY.

                //Enable the confirmation message for fusing gems.
                s_Confirmation_Action = Current_Strings.Confirm_Tower_Fuse;
                b_Confirmation_Open = true;
                //move the confirmation to the spot where the item is. want it directly above the item but for now middle.
                GameObject.Find(Current_Strings.Name_Confirmation_Box).transform.position = new Vector3(0, 0);
            }
            
        }
    }

    //this checks if the two gems in the spots can fuse and if so what it will fuse into. 
    //Will return a string of "No" if it's not possible.
    public string Is_Fuse_Possible()
    {
        //we take the tower in the first location and pass it the name/kind of gem of the second.
        //Then the tower it'self will check if it can make the fuse and return the name if any.

        GameObject Fuse_01 = null;
        GameObject Fuse_02 = null;

        //first get the two items and make sure there are two, if not return no.
        if (GameObject.Find(Current_Strings.Name_Fuse_Box_01).transform.childCount > 0)
        {
            Fuse_01 = GameObject.Find(Current_Strings.Name_Fuse_Box_01).transform.GetChild(0).gameObject;
        }
        if (GameObject.Find(Current_Strings.Name_Fuse_Box_02).transform.childCount > 0)
        {
            Fuse_02 = GameObject.Find(Current_Strings.Name_Fuse_Box_02).transform.GetChild(0).gameObject;
        }

        //make sure there is a gem in both spots.
        if (Fuse_01 != null && Fuse_02 != null)
        {
            //search the locked gems for the name of fuse 01 to start the compare.
            for (int i = 0; i < Locked_Gems.Locked_Gem_List.Count; i++)
            {
                if (Locked_Gems.Locked_Gem_List[i].Name == Fuse_01.GetComponent<Tower>().s_Name)
                {
                    //once first name is found we search the fuse list to see if the second gem name is in there.
                    for (int j = 0; j < Locked_Gems.Locked_Gem_List[i].Fuse_Tables.GetLength(0); j++)
                    {
                        //if there is a match we return the name of the gem that will be made from the fusion.
                        if (Locked_Gems.Locked_Gem_List[i].Fuse_Tables[j,0] == Fuse_02.GetComponent<Tower>().s_Name)
                        {
                            //return the name of j-1; aka the fusion name.
                            return Locked_Gems.Locked_Gem_List[i].Fuse_Tables[j, 1];
                        }
                    }
                }
            }
        }

        //there was either no gem in the second area, or no fuse options avilable.
        return "No";
    }

}
