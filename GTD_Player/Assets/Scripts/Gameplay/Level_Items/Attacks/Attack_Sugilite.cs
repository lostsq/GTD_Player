using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;
using Assets.Scripts.Gameplay.Level_Items;
using System.Collections.Generic;

public class Attack_Sugilite : Attack_Base {

    //used for attack
    List<GameObject> Attack_List = new List<GameObject>();
    public float f_Angle = 0;
    float f_Spin_Rate = 10;

    // Use this for initialization
    void Start () {
        //SETUP
        Sugilite_Setup();
        //set the name of the gem we are working with.
        s_Name_Of_Gem = "Sugilite";

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

                Sugilite_Spin_Attack();
            }
        }
        else
        {
            //we need to pause the animation.
            Pause_Animation();
        }
    }

    //Used to set up the desired effects for this attack/gem.
    void Sugilite_Setup()
    {
        //set the scale to the range.
        transform.localScale = new Vector2((f_Range_Level * 2), (f_Range_Level * 2));



        //scan all items within the range first round.
        RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (f_Range_Level * 2.56f * Main_Script.f_Zoom_Level), new Vector3(0, 0, 0));

        foreach (RaycastHit2D hit2 in Hits)
        {
            //ensure it's not already been attacked.
            if (!Attack_List.Contains(hit2.collider.gameObject))
            {
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

    void Sugilite_Spin_Attack()
    {

        f_Angle += f_Spin_Rate;

        transform.Rotate(0, 0, f_Spin_Rate);

        //Vector3 Draw_End = new Vector3(transform.position.x, transform.position.y + ((f_Range_Level * 2.56f * Main_Script.f_Zoom_Level)), 0);
        //Debug.DrawLine(transform.position, Draw_End, Color.green);

        //this is the end of the transistion.
        if (f_Angle >= 360)
        {
            //scan all items within the range.
            RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, (f_Range_Level * 2.56f * Main_Script.f_Zoom_Level), new Vector3(0, 0, 0));

            foreach (RaycastHit2D hit2 in Hits)
            {
                //ensure it's not already been attacked.
                if (!Attack_List.Contains(hit2.collider.gameObject))
                {
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

            }

            //destory the attack after it's finished.
            Destory_Attack();

        }
    }

    void Sugilite_Attack()
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
            //Sugilite. Just raw shoot dmg.
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Apply_Damage(Current_Strings.Atk_Raw, f_Damage_Level, 0, false);
            Enemy_To_Deal_Damage_To.GetComponent<Enemy>().Apply_Damage(Current_Strings.Atk_Slow, .1f, 4, true);


        }
        else
        {
            //temp for now, cause only temple attacks lolz.
            Main_Script.f_HP -= f_Damage_Level;
            //Debug.Log(Main_Script.i_HP);
        }
    }


}
