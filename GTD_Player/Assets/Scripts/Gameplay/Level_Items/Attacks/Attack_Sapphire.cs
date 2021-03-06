﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;
using Assets.Scripts.Gameplay.Level_Items;
using System.Collections.Generic;

public class Attack_Sapphire : Attack_Base {


    //used for aoe attack
    List<GameObject> Attack_List = new List<GameObject>();
    float f_Seconds_For_Attack = .2f;
    float f_Added_Time = 0;

    // Use this for initialization
    void Start () {
        //SETUP
        Sapphire_Setup();
        //set the name of the gem we are working with.
        s_Name_Of_Gem = "Sapphire";

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
                Sapphire_AOE_Attack();
            }
        }
        else
        {
            //we need to pause the animation.
            Pause_Animation();
        }
    }

    //Used to set up the desired effects for this attack/gem.
    void Sapphire_Setup()
    {
        

        //is enemy
        if (b_Is_Enemy_Attack)
        {

        }
        //is tower
        else
        {

        }
    }

    void Sapphire_Attack()
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

    void Sapphire_AOE_Attack()
    {

        //we expand the bullet/attack and the area checked each time until it has reached the range. we want it to last x number of seconds.
        //the values used are in the attack and not in the base of the attack since only some will do this.
        float Delta_Time = Time.deltaTime;
        f_Added_Time += Delta_Time;
        //percentate total
        float f_Percent_Total = f_Added_Time / f_Seconds_For_Attack;

        //scan all items within the range.
        RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (f_Range_Level * 2.56f * f_Percent_Total * Main_Script.f_Zoom_Level), new Vector3(0, 0, 0));

        //set the size of the bullet.
        transform.localScale = new Vector2((f_Range_Level * 2) * f_Percent_Total, (f_Range_Level * 2) * f_Percent_Total);

        //Vector3 Draw_End = new Vector3(transform.position.x, transform.position.y + ((f_Range_Level * 2.56f * f_Percent_Total * Main_Script.f_Zoom_Level)), 0);
        //Debug.DrawLine(transform.position, Draw_End, Color.blue);

        //RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, 4, new Vector3(0, 0, 0));

        foreach (RaycastHit2D hit2 in Hits)
        {
            //Debug.Log(" obj " + hit2.collider.gameObject.name);

            if (b_Is_Enemy_Attack)
            {
                if (hit2.collider.gameObject.tag == Current_Strings.Tag_Finish_Temple)
                {
                    //check if it's in the list cause if so then it's already been hit by this attack and we don't hit it again.
                    if (!Attack_List.Contains(hit2.collider.gameObject))
                    {
                        Attack_List.Add(hit2.collider.gameObject);
                        Deal_Damage(hit2.collider.gameObject);
                    }
                }
            }
            else
            {
                if (hit2.collider.gameObject.tag == Current_Strings.Tag_Enemy)
                {
                    //Debug.Log("Hit happened");

                    //check if it's in the list cause if so then it's already been hit by this attack and we don't hit it again.
                    if (!Attack_List.Contains(hit2.collider.gameObject))
                    {
                        Attack_List.Add(hit2.collider.gameObject);
                        Deal_Damage(hit2.collider.gameObject);
                    }
                }
            }
        }

        //destroy the attack after it's past 100%
        if (f_Percent_Total > 1)
        {
            //Debug.Log(f_Percent_Total);

            Destory_Attack();
        }
    }



    //This deals damage to the enemy that is passed.
    void Deal_Damage(GameObject Enemy_To_Deal_Damage_To)
    {
        //This is where each bullet has it's own unique attack placed in the enemy/item.
        if (Enemy_To_Deal_Damage_To.tag.Contains(Current_Strings.Tag_Enemy))
        {
            //Sapphire.
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Apply_Damage(Current_Strings.Atk_Ice, f_Damage_Level, 5, true);
            //now apply the slow.
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Apply_Damage(Current_Strings.Atk_Slow, .4f, f_Damage_Level * .8f, true);


        }
        else
        {
            //temp for now, cause only temple attacks lolz.
            Main_Script.f_HP -= f_Damage_Level;
            //Debug.Log(Main_Script.i_HP);
        }
    }


}
