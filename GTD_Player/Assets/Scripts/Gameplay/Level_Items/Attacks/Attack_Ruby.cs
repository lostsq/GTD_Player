using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;

public class Attack_Ruby : Attack_Base {


	// Use this for initialization
	void Start () {
        //RUBY SETUP
        Ruby_Setup();
        //set the name of the gem we are working with.
        s_Name_Of_Gem = "Ruby";

        //set the animation to start the bullet animation.
        if (!GetComponent<Animator>().GetBool("Bullet"))
        {
            GetComponent<Animator>().SetBool("Bullet", true);
        }
    }
	
	// Update is called once per frame
	void Update () {
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
                Ruby_Attack();
            }
        }
    }

    //Used to set up the desired effects for this attack/gem.
    void Ruby_Setup()
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

    void Ruby_Attack()
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

   
}
