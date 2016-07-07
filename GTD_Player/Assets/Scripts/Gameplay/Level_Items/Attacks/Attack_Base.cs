using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Gameplay.Level_Items.Attacks
{
    public class Attack_Base : MonoBehaviour
    {
        public Player_Main_Script Main_Script;
        String_Tracker Current_Strings = new String_Tracker();
        //this bool is to determain if it's an enemy attack or tower attacking to know what to attack.
        public bool b_Is_Enemy_Attack = false;
        public bool b_Set_Up_And_Ready = false;
        public GameObject Target;
        public GameObject Empty_Target_Pos;

        public string s_Name_Of_Gem;

        public GameObject Owner;




        public float f_Damage_Level = 5;
        public float f_Range_Level = 5;

        public float i_Range_To_Target_Offset = .1f;


        float f_Attack_Speed = 20;

        //public Vector3 Target_Position;
        //public Vector3 Map_Start_Pos;
        //float f_Last_Zoom_Amount;
        //public Vector3 Offset;

        public void Set_Up_Attack_Vars(GameObject Passed_Target, bool b_Is_Enemy_Attack_Passed, float Passed_Power_Level, float Passed_Range_Level)
        {
            //set up the base power/range.
            f_Damage_Level = Passed_Power_Level;
            f_Range_Level = Passed_Range_Level;

            Empty_Target_Pos = new GameObject();
            Empty_Target_Pos.transform.parent = GameObject.Find(Current_Strings.Name_Map_Parent).transform;
            //set the parent to be the normal map.
            //Owner = transform.parent.gameObject;
            //transform.parent = GameObject.Find(Current_Strings.Name_Map_Parent).transform;

            b_Is_Enemy_Attack = b_Is_Enemy_Attack_Passed;
            //will set up the target and such here it will need to be passed.
            Target = Passed_Target;
            Empty_Target_Pos.transform.position = Target.transform.position;

            Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();
            //f_Last_Zoom_Amount = Main_Script.f_Zoom_Level;
            //Map_Start_Pos = GameObject.Find(Current_Strings.Name_Map_Parent).transform.position;
            b_Set_Up_And_Ready = true;
            //Debug.Log("Setup");

            //going to have to generate out a gameobject/point, and set it to the same parent as the target posistion so that when zoom/move accours it stays locked on.
            

        }

        //this will move the bullet/attack to the target posistion which is determained by the type.
        public void Move_Towards_Target()
        {


            //SICNE THE TARGET CAN CHANGE WHEN THE USER MVOES THE FIELD WE WILL NEED A BETTER WAY TO KEEP TRACK OF IT'S LOCATION
            //Offset = Map_Start_Pos - GameObject.Find(Current_Strings.Name_Map_Parent).transform.position;
            //moves the bullet/attack towards the point that it needs to be that point could be a object or a point in space.
            //transform.position = Vector2.MoveTowards(transform.position + Offset, Target_Position + Offset, (f_Attack_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level);
            //transform.position = Vector2.MoveTowards(transform.position, Target_Position - Offset, ((f_Attack_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level));
            transform.position = Vector2.MoveTowards(transform.position, Empty_Target_Pos.transform.position, ((f_Attack_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level));
        }

        public bool Check_Range_To_Target()
        {
            //Offset = Map_Start_Pos - GameObject.Find(Current_Strings.Name_Map_Parent).transform.position;

            //should measure the distance a bullet will go and has a little bit extra to be safe.
            if ((Vector2.Distance(transform.position, Empty_Target_Pos.transform.position) <= ((f_Attack_Speed + .01) * Time.deltaTime) * Main_Script.f_Zoom_Level))
            {
                return true;
            }
            return false;
        }


        //This deals damage to the enemy that is passed.
        public void Deal_Damage(GameObject Enemy_To_Deal_Damage_To)
        {
            //sorts out what needs to take damage since both towers and enemies share this.
            if (Enemy_To_Deal_Damage_To.tag.Contains(Current_Strings.Tag_Enemy))
            {
                Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Take_Damage(f_Damage_Level);
            }
            else
            {
                //temp for now, cause only temple attacks lolz.
                Main_Script.f_HP -= f_Damage_Level;
                //Debug.Log(Main_Script.i_HP);
            }
        }

        //This destorys this bullet/attack.
        public void Destory_Attack()
        {
            Destroy(Empty_Target_Pos);
            Destroy(this.gameObject);
        }


        public bool AnimatorIsPlaying(string stateName)
        {
            return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public void Pause_Animation()
        {
            GetComponent<Animator>().enabled = false;
        }

    }
}
