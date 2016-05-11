using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Gameplay.Level_Items;
using Assets.Scripts.Gameplay.Generic;

public class Player_Main_Script : MonoBehaviour {

    String_Tracker Current_Strings = new String_Tracker();

    //This is if the game and all objects are paused.
    bool b_Is_Running = true;
    //spawn/enemy items.
    float f_Spawn_Timer = 0;
    bool b_Wave_Transistion = false;
    int i_Transistion_Time = 2;
    int i_Wave_Number = 0;

    //Level variables
    public string s_Level_Name = "";
    public int i_Energy = 0;
    public int i_Max_HP = 0;
    public int i_HP = 0;

    public int i_Invintory_Space_Amount = 0;

    public float f_Zoom_Level = 1;

    //for confirmation button.
    public bool b_Confirmation_Open = false;
    public bool b_Confirmation_True = true;
    public GameObject[] Confirmation_Objects;
    public string s_Confirmation_Action = "none";


    //Lists and arrays.
    public List<Enemy> Enemy_Spawns = new List<Enemy>();
    public List<Enemy> Enemies_On_Field = new List<Enemy>();
    public List<GameObject> Tower_List = new List<GameObject>();
    public All_Locked_Gems Locked_Gems = new All_Locked_Gems();
    public GameObject[,] Map_Spaces;
    public List<Map_Path> Path_List = new List<Map_Path>();


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {


        //triggers when confirmation has been used and has an action.
        if (!b_Confirmation_Open && s_Confirmation_Action != "none")
        {
            Confirmation_Process();
        }
        //confirmation is enabled right now. we can pause the game here or perform how we want to handle it.
        else
        {
               
        }


        if (b_Is_Running)
        {
            f_Spawn_Timer += Time.deltaTime;
            Update_Spawn();
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

                //set up the bullet/attack prefab here.
                New_Tower.s_Attack_Prefab = Current_Strings.Prefab_Attack_Tower_Location + New_Tower.s_Name;

                New_Tower.i_Exp_Level = Locked_Gems.Locked_Gem_List[i].i_Exp_Level;
                New_Tower.i_Power_Levels = Locked_Gems.Locked_Gem_List[i].i_Power_Levels;
                New_Tower.i_Power_Amount = Locked_Gems.Locked_Gem_List[i].i_Power_Amount;
                New_Tower.i_Range_Levels = Locked_Gems.Locked_Gem_List[i].i_Range_Levels;
                New_Tower.i_Speed_Amount = Locked_Gems.Locked_Gem_List[i].i_Range_Amount;
                New_Tower.i_Speed_Levels = Locked_Gems.Locked_Gem_List[i].i_Speed_Levels;
                New_Tower.i_Speed_Amount = Locked_Gems.Locked_Gem_List[i].i_Speed_Amount;
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
            }
            //return the tower to the former location.
            else
            {
                //the old parent, and the current item/ghost
                Confirmation_Objects[1].transform.position = Confirmation_Objects[0].transform.position;
                Confirmation_Objects[1].transform.parent = Confirmation_Objects[0].transform;
                Confirmation_Objects[1].transform.localScale = Confirmation_Objects[0].transform.localScale;
                //sprite render stuff.
                Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[0].GetComponent<SpriteRenderer>().sortingOrder + 2;
            }
        }

        if (s_Confirmation_Action == Current_Strings.Confirm_Tower_To_Hotbar)
        {
            //Moving tower from field to hotspot.
            if (b_Confirmation_True)
            {
                //need to disable tower
                Confirmation_Objects[1].GetComponent<Tower>().b_On_Field = false;
            }
            //return the tower to the former location.
            else
            {
                //the old parent, and the current item/ghost
                Confirmation_Objects[1].transform.position = Confirmation_Objects[0].transform.position;
                Confirmation_Objects[1].transform.parent = Confirmation_Objects[0].transform;
                Confirmation_Objects[1].transform.localScale = Confirmation_Objects[0].transform.localScale;
                //sprite render stuff.
                Confirmation_Objects[1].GetComponent<SpriteRenderer>().sortingOrder = Confirmation_Objects[0].GetComponent<SpriteRenderer>().sortingOrder + 2;
            }
        }


        //reset the confirmation message to "none"
        s_Confirmation_Action = "none";

    }



}
