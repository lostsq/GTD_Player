using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;
using Assets.Scripts.Gameplay.Level_Items;
using System.Collections.Generic;

public class Attack_Opal : Attack_Base {


    //used for attack
    List<GameObject> Attack_List = new List<GameObject>();
    public bool b_Orginal = true;
    int i_Number_Of_Targets = 3;

    // Use this for initialization
    void Start () {
        //SETUP
        Opal_Setup();
        //set the name of the gem we are working with.
        s_Name_Of_Gem = "Opal";

        //set the animation to start the bullet animation.
        if (!GetComponent<Animator>().GetBool("Bullet"))
        {
            GetComponent<Animator>().SetBool("Bullet", true);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Main_Script.b_Is_Running)
        {
            //unpause animation if paused.
            if (GetComponent<Animator>().enabled == false)
            {
                GetComponent<Animator>().enabled = true;
            }
            //make sure the correct animation is playing.
            if (AnimatorIsPlaying(s_Name_Of_Gem + "_Bullet"))
            {
                //this is triggered at the end of the animation, but will keep going if nothing changes.
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
                {
                    //Destroy(this.gameObject);
                }

                //first we move to the desired location and then we check if we are in the range we want to attack.
                Move_Towards_Target();
                //range check.
                if (Check_Range_To_Target())
                {
                    Opal_Attack();
                }
            }
        }
        else
        {
            //we need to pause the animation.
            Pause_Animation();
        }
    }

    //Used to set up the desired effects for this attack/gem.
    void Opal_Setup()
    {
        //only want the orginal bullet to do this.
        if (b_Orginal)
        {
            int i_Target_Counter = 0;

            Attack_List.Add(Target);
            //need to check for up to x number of targets in range that are not already being targeted.
            //scan all items within the range first round.
            RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (f_Range_Level * 2.56f * Main_Script.f_Zoom_Level), new Vector3(0, 0, 0));

            foreach (RaycastHit2D hit2 in Hits)
            {
                //ensure it's not already been attacked.
                if (!Attack_List.Contains(hit2.collider.gameObject) && i_Target_Counter < i_Number_Of_Targets)
                {
                    Attack_List.Add(hit2.collider.gameObject);

                    if (b_Is_Enemy_Attack)
                    {
                        if (hit2.collider.gameObject.tag == Current_Strings.Tag_Finish_Temple)
                        {
                            //we will spawn out a attack and assign out the variable on it.
                            GameObject New_Attack = Instantiate(Resources.Load(Owner.GetComponent<Tower>().s_Bullet_Prefab)) as GameObject;
                            Vector3 Cur_Scale = New_Attack.transform.localScale;

                            New_Attack.GetComponent<Attack_Base>().Set_Up_Attack_Vars(hit2.collider.gameObject, true, f_Damage_Level, f_Range_Level);
                            //the location/spawn of the attack is the parent since the object can move.
                            New_Attack.transform.position = transform.parent.position;
                            New_Attack.transform.parent = transform.parent;
                            New_Attack.transform.localScale = transform.localScale;
                            New_Attack.GetComponent<Attack_Base>().Owner = Owner;

                            New_Attack.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

                            New_Attack.GetComponent<Attack_Opal>().b_Orginal = false;

                            i_Target_Counter++;
                        }
                    }
                    else
                    {
                        if (hit2.collider.gameObject.tag == Current_Strings.Tag_Enemy)
                        {
                            //we will spawn out a attack and assign out the variable on it.
                            GameObject New_Attack = Instantiate(Resources.Load(Owner.GetComponent<Tower>().s_Bullet_Prefab)) as GameObject;
                            Vector3 Cur_Scale = New_Attack.transform.localScale;

                            New_Attack.GetComponent<Attack_Base>().Set_Up_Attack_Vars(hit2.collider.gameObject, false, f_Damage_Level, f_Range_Level);
                            //the location/spawn of the attack is the parent since the object can move.
                            New_Attack.transform.position = transform.parent.position;
                            New_Attack.transform.parent = transform.parent;
                            New_Attack.transform.localScale = transform.localScale;
                            New_Attack.GetComponent<Attack_Base>().Owner = Owner;
                            New_Attack.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

                            New_Attack.GetComponent<Attack_Opal>().b_Orginal = false;

                            i_Target_Counter++;
                        }
                    }
                }
            }
        }


        //is enemy
        if (b_Is_Enemy_Attack)
        {

        }
        //is tower
        else
        {

        }
    }

    void Opal_Attack()
    {
        //will need to make sure the target is not null, might do that inside the base.
        if (Target == null)
        {
            Destory_Attack();
        }
        else
        { 
            //we deal the damage.
            Deal_Damage(Target);
            //destory the attack at the end.
            Destory_Attack();
        }
    }


    //This deals damage to the enemy that is passed.
    void Deal_Damage(GameObject Enemy_To_Deal_Damage_To)
    {
        //This is where each bullet has it's own unique attack placed in the enemy/item.
        if (Enemy_To_Deal_Damage_To.tag.Contains(Current_Strings.Tag_Enemy))
        {
            //Opal. Just raw shoot dmg.
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Apply_Damage(Current_Strings.Atk_Raw, f_Damage_Level, 0, false);


        }
        else
        {
            //temp for now, cause only temple attacks lolz.
            Main_Script.f_HP -= f_Damage_Level;
            //Debug.Log(Main_Script.i_HP);
        }
    }


}
