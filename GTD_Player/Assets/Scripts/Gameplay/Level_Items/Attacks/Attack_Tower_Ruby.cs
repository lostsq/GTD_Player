using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items.Attacks;

public class Attack_Tower_Ruby : Attack_Base {

	// Use this for initialization
	void Start () {
        Setup_Base_Start();
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

    }

    void Ruby_Attack()
    {
        //for now we'll have it deal 1000 damage to it's target.. basically 1 hit kill.
        Target.GetComponent<Assets.Scripts.Gameplay.Level_Items.Enemy>().Deal_Damage(1000);
        //destory the attack at the end.
        Destory_Attack();
    }

   
}
