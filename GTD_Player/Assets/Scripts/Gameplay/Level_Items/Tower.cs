﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Gameplay.Level_Items
{
    public class Tower : MonoBehaviour
    {
        bool b_On_Field = false;
        bool b_Fused = false;
        public string s_Name = "Null";
        int i_Level = 0;
        int i_Spending_Points = 0;
        public int i_exp = 0;
        public int[] i_Exp_Level = new int[] {10,100,100,100,100,100,100,100,10,10 };
        //10 levels is MAX unless it's set manually.
        public int[] i_Power_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Power_Amount = 1;
        public int[] i_Range_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Range_Amount = 1;
        public int[] i_Speed_Levels = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public int i_Speed_Amount = 1;

        //This is the tower.
        public GameObject This_Tower;

        //The string tracker.
        String_Tracker Current_Strings = new String_Tracker();

        /*
        //we create the tower and return that tower that is created.
        public GameObject Create_New_Tower(string Tower_Name)
        {
            
            //This is the main script that holds all the information.
            Player_Main_Script Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();

            GameObject New_Tower = null;
            //gets all the information from the locked list. that's where we will grab it and just pass it the name. don't need to set it up ehre.
            for (int i = 0; i < Main_Script.Locked_Gems.Locked_Gem_List.Count; i++)
            {
                if (Main_Script.Locked_Gems.Locked_Gem_List[i].Name == Tower_Name)
                {
                    s_Name = Main_Script.Locked_Gems.Locked_Gem_List[i].Name;
                    i_Exp_Level = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Exp_Level;
                    i_Power_Levels = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Power_Levels;
                    i_Power_Amount = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Power_Amount;
                    i_Range_Levels = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Range_Levels;
                    i_Speed_Amount = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Range_Amount;
                    i_Speed_Levels = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Speed_Levels;
                    i_Speed_Amount = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Speed_Amount;

                    //tower was found so we create the gameobject.
                    New_Tower = Instantiate(Resources.Load(Main_Script.Locked_Gems.Locked_Gem_List[i].s_Prefab_Location)) as GameObject;
                    This_Tower = New_Tower;
                    New_Tower.AddComponent<Tower>();
                    
                    //setting the spirte for idle animations.
                    //New_Tower.GetComponent<SpriteRenderer>().sprite = Main_Script.Locked_Gems.Locked_Gem_List[i].sp_Tower_Sprite;
                }
            }

            return New_Tower;
        }
        */

        public void Set_Tower_Stats()
        {

        }

        public void Move_To_Invintory(bool b_Spawing_In)
        {
            //ensuring it's not on the field.
            b_On_Field = false;

            if (b_Spawing_In)
            {
                //this places them in the next spot on the field.
                int Current = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>().i_Invintory_Space_Amount;
                Current = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.childCount - Current;

                //will randomally place it on the invintory field. so multiples don't get too crowded.
                //int i_Random = UnityEngine.Random.Range(0, GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.childCount);

                transform.position = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.GetChild(Current).transform.position;//new Vector2(0,0);
            }

            //set the scale. we use the first child since any child will have the same scale.
            transform.localScale = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.GetChild(0).transform.localScale;

            //set the parent of the tower.
            transform.parent = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform;


            //set the layering to be + 1 of the field.
            GetComponent<SpriteRenderer>().sortingOrder = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder + 1;
        }


        // Use this for initialization
        void Start()
        {
            Debug.Log("Working");
            //get the main script to connect to it/start calls in it when we are finished setting things up.
            //Main_Script = 
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}