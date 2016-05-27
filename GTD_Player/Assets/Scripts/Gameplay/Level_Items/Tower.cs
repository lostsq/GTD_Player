using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using UnityEditor;

namespace Assets.Scripts.Gameplay.Level_Items
{
    public class Tower : MonoBehaviour
    {
        public bool b_On_Field = false;
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

        //this is the size of the gem.
        public float f_Scale_Amount = 1;


        public float f_Timer = 0;

        //This is the tower.
        public GameObject This_Tower;
        //this is it's target
        public GameObject The_Target = null;

        public string s_Bullet_Prefab;

        //The string tracker.
        String_Tracker Current_Strings = new String_Tracker();
        Player_Main_Script Main_Script;

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

            //set the scale.
            transform.localScale = new Vector2(f_Scale_Amount, f_Scale_Amount);

            //set the parent of the tower.
            transform.parent = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform;

            


            //set the layering to be + 1 of the field.
            GetComponent<SpriteRenderer>().sortingOrder = GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder + 1;
        }



        // Use this for initialization
        void Start()
        {
            //Debug.Log("Working");
            //get the main script to connect to it/start calls in it when we are finished setting things up.
            Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();

            //this works for transparancy.
            //we get the child/ghost and make it transparent.
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
           
            //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }

        // Update is called once per frame
        void Update()
        {
            /*
            bool down = Input.GetKeyDown(KeyCode.Space);
            bool held = Input.GetKey(KeyCode.Space);
            bool up = Input.GetKeyUp(KeyCode.Space);

            if (down)
            {
               
                //GetComponent<Animator>().SetBool("Attacking", true);
                
            }
            else if (held)
            {
                //graphic.texture = heldgfx;
            }
            else if (up)
            {
                GetComponent<Animator>().SetBool("Attacking", true);
            }

            if (AnimatorIsPlaying(s_Name + "_Attack"))
            {
                //if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length < GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                {
                    //GetComponent<Animator>().SetBool("Attacking", false);
                   // Debug.Log("End");
                }
            }
            */

            if (b_On_Field)
            {
                f_Timer += Time.deltaTime;


                //check the timer for cooldown on attacks.
                if (f_Timer > .2f)
                {
                    //need to find the closest target in range.
                    RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (((i_Range_Amount * gameObject.GetComponent<BoxCollider2D>().size.x) * Main_Script.f_Zoom_Level) * transform.localScale.x), new Vector3(0, 0, 0));
                    //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + (((i_Range_Amount * gameObject.GetComponent<BoxCollider2D>().size.x) * Main_Script.f_Zoom_Level) * transform.localScale.x), 0), Color.red);

                    //RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, 4, new Vector3(0, 0, 0));

                    foreach (RaycastHit2D hit2 in Hits)
                    {
                        //Debug.Log(" obj " + hit2.collider.gameObject.name);

                        if (hit2.collider.gameObject.tag == Current_Strings.Tag_Enemy)
                        {
                            //check if it's the closest one.
                            if (The_Target != null)
                            {
                                //finds the closest one. Will add in more for lowest hp and such.
                                if (Vector2.Distance(The_Target.transform.position,transform.parent.position) > Vector2.Distance(hit2.collider.gameObject.transform.position, transform.parent.position))
                                {
                                    The_Target = hit2.collider.gameObject;
                                }
                            }
                            else
                            {
                                The_Target = hit2.collider.gameObject;
                            }
                        }
                        //transform.gameObject.SendMessage("GotHit", damage);
                    }

                    //target found that we are going to attack.
                    if (The_Target != null)
                    {
                        //set attacking to true.
                        GetComponent<Animator>().SetBool("Attacking", true);

                        //Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + "T1");
                        //Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime + "T2");

                        if (AnimatorIsPlaying(s_Name + "_Attack"))
                        {
                            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                            {
                                //the the attack to false and timer to 0 to reset.
                                GetComponent<Animator>().SetBool("Attacking", false);
                                f_Timer = 0;
                                
                                //we will spawn out a attack and assign out the variable on it.
                                GameObject New_Attack = Instantiate(Resources.Load(s_Bullet_Prefab)) as GameObject;
                                Vector3 Cur_Scale = New_Attack.transform.localScale;
                                New_Attack.GetComponent<Attacks.Attack_Base>().Set_Up_Attack_Vars(The_Target, false);
                                //the location/spawn of the attack is the parent since the object can move.
                                New_Attack.transform.position = transform.parent.position;
                                New_Attack.transform.parent = transform.parent;
                                New_Attack.transform.localScale = Cur_Scale;
                                New_Attack.GetComponent<SpriteRenderer>().sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
                            }
                        }

                    }
                    
                    
                    

                    

                }

            }
        }

        bool AnimatorIsPlaying(string stateName)
        {
            return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

    }
}
