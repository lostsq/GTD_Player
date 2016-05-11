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
        public GameObject Target;

        public int i_Damage = 5;
        public float i_Range_To_Target_Offset = .1f;


        float f_Attack_Speed = 10;

        public Vector2 Target_Position;

        //This is called to set up the basic information upon loading so it doesn't mess up.
        public void Setup_Base_Start()
        {
            Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();
        }

        public void Set_Up_From_Tower(GameObject Passed_Target)
        {
            //will set up the target and such here it will need to be passed.
            Target = Passed_Target;
            Target_Position = Target.transform.position;
        }

        //this will move the bullet/attack to the target posistion which is determained by the type.
        public void Move_Towards_Target()
        {
            //moves the bullet/attack towards the point that it needs to be that point could be a object or a point in space.
            transform.position = Vector2.MoveTowards(transform.position, Target_Position, (f_Attack_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level);
        }

        public bool Check_Range_To_Target()
        {
            if ((Vector2.Distance(transform.position, Target_Position) < i_Range_To_Target_Offset))
            {
                return true;
            }
            return false;
        }


        //This deals damage to the enemy that is passed.
        public void Deal_Damage(GameObject Enemy_To_Deal_Damage_To)
        {
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Deal_Damage(i_Damage);
        }

        //This destorys this bullet/attack.
        public void Destory_Attack()
        {
            Destroy(this.gameObject);
        }


        

    }
}
