using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using UnityEditor;

namespace Assets.Scripts.Gameplay.Level_Items
{
    public class Enemy : MonoBehaviour
    {
        //emy_List[i].Enemy_Start_After + "," + Enemy_List[i].Enemy_Reward_Single + "," + Enemy_List[i].Enemy_Reward_Wave + "," + Enemy_List[i].Enemy_Mod;
        Player_Main_Script Main_Script;
        String_Tracker Current_Strings = new String_Tracker();
        float f_Spawn_Timer = 0;
        int i_Spawn_Inbetween = 1;

        //this is to make them not stack and give a little randomness.
        float f_Random_x;
        float f_Random_y;

        //variables
        public string s_Name;
        public int i_Wave_Number;
        public int i_HP;
        public int i_Speed;
        public int i_Power;
        public int i_Amount;
        public int i_Start_After;
        public int i_Reward_Single;
        public int i_Reward_Wave;
        public string s_Mod;

        public int i_Range;

        //this is the size of the gem.
        public float f_Scale_Amount = 1;


        //the target it's targeting.
        GameObject The_Target;

        //used for moving
        public GameObject Next_Spot;
        int i_Moving_Towards_Path_Number = 0;


        //used for keeping track of waves.
        public bool b_Spawn_Tracker = true;
        public int i_Amount_Generated = 0;
        public int i_Amount_Destoryed = 0;
        public bool b_In_Progress = false;
        public int i_Location_In_Spawn_Array;
        public Enemy Parent_Spawner;

        //bullet prefab.
        public string s_Bullet_Prefab;



        //Create the enemy with all the stats needed to get it going.
        public void Set_Enemy(string p_Name, int p_Wave_Number, int p_HP, int p_Speed, int p_Power, int p_Amount, int p_Start_After, int p_Reward_Single, int p_Reward_Wave, string p_Mod, bool b_Is_Spawner)
        {
            //get the main script to connect to it/start calls in it when we are finished setting things up.
            Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();

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

            //set the scale by comparing against the locked gems. if it's not in there we set/keep at one.
            for (int i = 0; i < Main_Script.Locked_Gems.Locked_Gem_List.Count; i++)
            {
                //all locked gems can also be enemies so we use that list to also check for scales/sizes.
                if (Main_Script.Locked_Gems.Locked_Gem_List[i].Name == s_Name)
                {
                    f_Scale_Amount = Main_Script.Locked_Gems.Locked_Gem_List[i].f_Scale_Amount;
                    i_Range = Main_Script.Locked_Gems.Locked_Gem_List[i].i_Range_Amount;
                }
            }

            if (!b_Is_Spawner)
            {
                b_Spawn_Tracker = false;
            }

            

        }

        //this will spawn out an enemy when called.
        public void Spawn_Enemy()
        {
            f_Spawn_Timer += Time.deltaTime;

            //check to see if one needs to be generated.
            if (i_Amount_Generated < i_Amount && f_Spawn_Timer > i_Spawn_Inbetween)
            {
                //Debug.Log("Spawn Occured:" + f_Spawn_Timer);
                //reset the spawn timer.
                f_Spawn_Timer = 0;
                //create the enemy.
                GameObject go_New_Enemy = null;
                go_New_Enemy = Instantiate(Resources.Load(Current_Strings.Prefab_Enemy_Location + s_Name)) as GameObject;
                if (go_New_Enemy != null)
                {
                    go_New_Enemy.AddComponent<Enemy>();
                    Enemy New_Enemy = go_New_Enemy.GetComponent<Enemy>();
                    New_Enemy.Set_Enemy(s_Name, i_Wave_Number, i_HP, i_Speed, i_Power, i_Amount, i_Start_After, i_Reward_Single, i_Reward_Wave, s_Mod, false);
                    New_Enemy.s_Bullet_Prefab = s_Bullet_Prefab;
                    New_Enemy.Parent_Spawner = this;//.gameObject.GetComponent<Enemy>();

                    

                    New_Enemy.f_Random_x = UnityEngine.Random.Range(-.5f, .5f);
                    New_Enemy.f_Random_y = UnityEngine.Random.Range(-.5f, .5f);

                    GameObject Start = GameObject.Find(Current_Strings.Name_Map_Start);

                    

                    //Set the enemy at the start.
                    go_New_Enemy.transform.position = Start.transform.position;
                    go_New_Enemy.transform.parent = Start.transform;
                    //scale must be set under parent or else it doesn't work right. above and if you scroll in/out it messes it up and set it to 0.
                    go_New_Enemy.transform.localScale = new Vector2(f_Scale_Amount,f_Scale_Amount);// Start.transform.localScale;

                    //set the next spot to be the first spot they move towards.
                    for (int i = 0; i < Main_Script.Path_List.Count; i++)
                    {
                        if (Main_Script.Path_List[i].Path_Number == 0)
                        {
                            New_Enemy.Next_Spot = Main_Script.Path_List[i].gameObject;
                        }
                    }


                    i_Amount_Generated++;

                }
                //prefab was not loaded, error.
                else
                {
                    Debug.Log("Prefab/gameobject null in enemy error");
                }
            }
        }


        // Use this for initialization
        void Start()
        {
            //Debug.Log("Working");
            //set moving to start by default.
            GetComponent<Animator>().SetBool("Moving", true);
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(f_Spawn_Timer);
            f_Spawn_Timer += Time.deltaTime;

            //make sure it's not a spawner.
            if (!b_Spawn_Tracker)
            {
                //is attacking
                if (GetComponent<Animator>().GetBool("Attacking"))
                {
                    //for movement we just need to know the next spot and head towards it until we reach 0/on it then set the next parent.
                    if (The_Target != null)
                    {
                        GetComponent<Animator>().SetBool("Moving", false);
                        GetComponent<Animator>().SetBool("Attacking", true);

                        if (Still_In_Range())
                        {
                            if (AnimatorIsPlaying(s_Name + "_Attack"))
                            {
                                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                                {

                                    GetComponent<Animator>().SetBool("Moving", true);
                                    GetComponent<Animator>().SetBool("Attacking", false);


                                    f_Spawn_Timer = 0;
                                    //then we need to attack and perform attack animation.

                                    //we will spawn out a attack and assign out the variable on it.
                                    GameObject New_Attack = Instantiate(Resources.Load(s_Bullet_Prefab)) as GameObject;
                                    Vector3 Cur_Scale = New_Attack.transform.localScale;
                                    New_Attack.GetComponent<Attacks.Attack_Base>().Set_Up_Attack_Vars(The_Target, false);
                                    New_Attack.GetComponent<Attacks.Attack_Base>().Owner = gameObject;
                                    //the location/spawn of the attack is the parent since the object can move.
                                    New_Attack.transform.position = transform.position;
                                    New_Attack.transform.parent = transform.parent;//GameObject.Find(Current_Strings.Name_Map_Parent).transform;
                                    New_Attack.transform.localScale = Cur_Scale;
                                    New_Attack.GetComponent<SpriteRenderer>().sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;


                                    The_Target = null;
                                }
                            }

                        }
                        else
                        {
                            GetComponent<Animator>().SetBool("Moving", true);
                            GetComponent<Animator>().SetBool("Attacking", false);
                            //Move_Enemy();
                        }
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("Moving", true);
                        GetComponent<Animator>().SetBool("Attacking", false);
                        //Move_Enemy();
                    }
                }
                else
                {
                    //if the target is null we check for one.
                    if (The_Target == null)
                    {
                        The_Target = Check_For_Target_In_Range();
                    }

                    if (The_Target != null && Still_In_Range())
                    {
                        //the attack timer/speed of attack.
                        if (f_Spawn_Timer > 1f)
                        {
                            GetComponent<Animator>().SetBool("Moving", false);
                            GetComponent<Animator>().SetBool("Attacking", true);
                        }
                        //set it to a idle state since it's waiting to attack.
                        else
                        {
                            GetComponent<Animator>().SetBool("Moving", false);
                            GetComponent<Animator>().SetBool("Attacking", false);
                        }
                    }
                    //either not in range or no target was found so we move.
                    else
                    {
                        The_Target = null;
                        GetComponent<Animator>().SetBool("Moving", true);
                        GetComponent<Animator>().SetBool("Attacking", false);
                        Move_Enemy();
                    }
                }
            }
        }


        GameObject Check_For_Target_In_Range()
        {
            GameObject This_Target = null;

            //need to find the closest target in range. ((range * box collider size * zoom level * local scale.))
            RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (((i_Range * gameObject.GetComponent<BoxCollider2D>().size.x) * Main_Script.f_Zoom_Level) * transform.localScale.x + (f_Random_x * f_Scale_Amount)) , new Vector3(0, 0, 0));

                foreach (RaycastHit2D hit2 in Hits)
                {
                    //Debug.Log(" obj " + hit2.collider.gameObject.name);

                    if (hit2.collider.gameObject.tag == Current_Strings.Tag_Finish_Temple)
                    {
                        //check if it's the closest one.
                        if (This_Target != null)
                        {
                            //finds the closest one. Will add in more for lowest hp and such.
                            if (Vector2.Distance(This_Target.transform.position, transform.parent.position) > Vector2.Distance(hit2.collider.gameObject.transform.position, transform.parent.position))
                            {
                                This_Target = hit2.collider.gameObject;
                            }
                        }
                        else
                        {
                            This_Target = hit2.collider.gameObject;
                        }
                    }
                    //transform.gameObject.SendMessage("GotHit", damage);
                }
            

            return This_Target;
        }

        bool Still_In_Range()
        {
            bool b_Results = false;

            //need to find the closest target in range. ((range * box collider size * zoom level * local scale.))
            RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (((i_Range * gameObject.GetComponent<BoxCollider2D>().size.x) * Main_Script.f_Zoom_Level) * transform.localScale.x), new Vector3(0, 0, 0));

            foreach (RaycastHit2D hit2 in Hits)
            {

                if (hit2.collider == The_Target.GetComponent<Collider2D>())
                {
                    b_Results = true;
                }

            }

            return b_Results;
        }

        void Move_Enemy()
        {
            //this is where we are trying to go. adding in the random amount to decrease stacking.
            Vector2 Move_Here = new Vector2(Next_Spot.transform.position.x + (f_Random_x * f_Scale_Amount), Next_Spot.transform.position.y + (f_Random_y * f_Scale_Amount));

            // Next_Spot
            transform.position = Vector2.MoveTowards(transform.position, Move_Here, ((i_Speed * Time.deltaTime) * Main_Script.f_Zoom_Level));
            //check and see if it is at it's next spot, and if so we set up the next spot at the parent and the next next spot.

            //need to have it close but not exact cause times it won't fall right on it.
            float t_distance = Vector2.Distance(transform.position, Move_Here);

            //the final spot has been reached so we set the temple as the last spot.
            if (Main_Script.Path_List.Count == i_Moving_Towards_Path_Number + 1)
            {
                Next_Spot = GameObject.Find(Current_Strings.Name_Map_Temple);
            }

            //check distance to make sure it's close before going to next spot.
            if (t_distance < (.01 * Main_Script.f_Zoom_Level))
            {

                for (int i = 0; i < Main_Script.Path_List.Count; i++)
                {
                    if (Main_Script.Path_List[i].Path_Number == i_Moving_Towards_Path_Number + 1)
                    {
                        //transform.parent = Next_Spot.transform;
                        Next_Spot = Main_Script.Path_List[i].gameObject;
                    }
                }
                i_Moving_Towards_Path_Number++;
            }
        }

        //Is called when this enemy is taken damage.
        public void Take_Damage(int i_Damage_Taken)
        {
            i_HP -= i_Damage_Taken;
            if (i_HP <= 0)
            {
                //need to remove from spawner.
                Parent_Spawner.i_Amount_Destoryed++;
                if (Parent_Spawner.i_Amount_Destoryed == Parent_Spawner.i_Amount)
                {
                    //think we can just set the parent spawner to null.. but might not destory it.. i'll have to check, so we'll skip for now.
                    //Parent_Spawner = null;
                    Main_Script.i_Energy += i_Reward_Wave;
                }

                //give the reward and destory this object.
                Main_Script.i_Energy += i_Reward_Single;
                Destroy(this.gameObject);
            }
        }

        bool AnimatorIsPlaying(string stateName)
        {
            return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

    }
}
