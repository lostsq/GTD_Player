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
        Player_Main_Script Main_Script;
        String_Tracker Current_Strings = new String_Tracker();
        //this bool is to determain if it's an enemy attack or tower attacking to know what to attack.
        public bool b_Is_Enemy_Attack = false;
        public bool b_Set_Up_And_Ready = false;
        public GameObject Target;



        public int i_Damage = 5;
        public float i_Range_To_Target_Offset = .1f;


        float f_Attack_Speed = 20;

        public Vector2 Target_Position;

        public void Set_Up_Attack_Vars(GameObject Passed_Target, bool b_Is_Enemy_Attack_Passed)
        {
            b_Is_Enemy_Attack = b_Is_Enemy_Attack_Passed;
            //will set up the target and such here it will need to be passed.
            Target = Passed_Target;
            Target_Position = Target.transform.position;
            Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();
            b_Set_Up_And_Ready = true;
            //Debug.Log("Setup");

        }

        //this will move the bullet/attack to the target posistion which is determained by the type.
        public void Move_Towards_Target()
        {
            //SICNE THE TARGET CAN CHANGE WHEN THE USER MVOES THE FIELD WE WILL NEED A BETTER WAY TO KEEP TRACK OF IT'S LOCATION

            //moves the bullet/attack towards the point that it needs to be that point could be a object or a point in space.
            transform.position = Vector2.MoveTowards(transform.position, Target_Position, (f_Attack_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level);
        }

        public bool Check_Range_To_Target()
        {
            //should measure the distance a bullet will go and has a little bit extra to be safe.
            if ((Vector2.Distance(transform.position, Target_Position) <= ((f_Attack_Speed + .01) * Time.deltaTime) * Main_Script.f_Zoom_Level))
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
                Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Deal_Damage(i_Damage);
            }
            else
            {
                //temp for now, cause only temple attacks lolz.
                Main_Script.i_HP -= i_Damage;
                Debug.Log(Main_Script.i_HP);
            }
        }

        //This destorys this bullet/attack.
        public void Destory_Attack()
        {
            Destroy(this.gameObject);
        }


        

    }
}
