using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;

public class Attack_Ruby : Attack_Base {


	// Use this for initialization
	void Start () {
        //RUBY SETUP
        Ruby_Setup();
    }
	
	// Update is called once per frame
	void Update () {

        //first we move to the desired location and then we check if we are in the range we want to attack.
        Move_Towards_Target();
        //range check.
        if (Check_Range_To_Target())
        {
            Ruby_Attack();
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


        //we deal the damage.
        Deal_Damage(Target);
        //destory the attack at the end.
        Destory_Attack();
    }

   
}
