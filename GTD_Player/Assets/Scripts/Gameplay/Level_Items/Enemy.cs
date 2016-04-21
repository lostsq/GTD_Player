using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Gameplay.Level_Items
{
    public class Enemy
    {
        //emy_List[i].Enemy_Start_After + "," + Enemy_List[i].Enemy_Reward_Single + "," + Enemy_List[i].Enemy_Reward_Wave + "," + Enemy_List[i].Enemy_Mod;

        //variables
        string s_Name;
        int i_Wave_Number;
        int i_HP;
        int i_Speed;
        int i_Power;
        int i_Amount;
        int i_Start_After;
        int i_Reward_Single;
        int i_Reward_Wave;
        string s_Mod;
            


        //Create the enemy with all the stats needed to get it going.
        public Enemy(string p_Name, int p_Wave_Number, int p_HP, int p_Speed, int p_Power, int p_Amount, int p_Start_After, int p_Reward_Single, int p_Reward_Wave, string p_Mod)
        {
            s_Name = p_Name;
            i_Wave_Number = p_Wave_Number;
            i_HP = p_HP;
            i_Speed = p_Speed;
            i_Power = p_Power;
            i_Amount = p_Amount;
            i_Start_After = p_Start_After;
            i_Reward_Single = p_Reward_Single;
            i_Reward_Wave = p_Reward_Wave;
            s_Mod = p_Mod;
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
