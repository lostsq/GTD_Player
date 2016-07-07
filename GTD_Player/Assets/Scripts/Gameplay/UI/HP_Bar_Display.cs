using UnityEngine;
using System.Collections;
using Assets.Scripts.Gameplay.Level_Items;

public class HP_Bar_Display : MonoBehaviour {

    //This is placed on everything we want to have a hp bar.
    //e = enemy, t = temple.
    char Object_Type = 'n';

    Player_Main_Script Main_Script;

    public GameObject HP_Gage;

    float HP_Max = 100;
    float HP_Cur = 0;

    public bool Perform_Update = true;

	// Use this for initialization
	void Start () {
        Main_Script = GameObject.Find("Main_Script").GetComponent<Player_Main_Script>();

        //set the HP Gage.
        HP_Gage = Instantiate(Resources.Load(Main_Script.Current_Strings.Prefab_HP_Gage)) as GameObject;

        if (tag.Contains(Main_Script.Current_Strings.Tag_Enemy))
        {
            Object_Type = 'e';
            HP_Max = GetComponent<Enemy>().f_Max_HP;
        }
        else if (tag.Contains(Main_Script.Current_Strings.Tag_Finish_Temple))
        {
            Object_Type = 't';
            HP_Max = Main_Script.i_Max_HP;
        }
        else
        {
            //no item found in our list so we don't update the hp gage and we debug log it.
            Perform_Update = false;
            Debug.Log("HP_Gage no object type found on" + transform.parent.name);
        }


       


    }
	
	// Update is called once per frame
	void Update () {

        if (Perform_Update)
        {
            //set the sprite layer/order to match the host.
            HP_Gage.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 3;
            //green
            HP_Gage.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder+ 2;
            //red
            HP_Gage.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            //check what it is and then update/show a bar above the object.

            //temple.
            if (Object_Type == 't')
            {
                HP_Cur = Main_Script.f_HP;
            }
            //enemy
            else if (Object_Type == 'e')
            {
                HP_Cur = GetComponent<Enemy>().f_HP;
            }
            //nothing (should not be hit.)
            else
            {

            }
            //set the scale.
            HP_Gage.transform.localScale = transform.localScale * Main_Script.f_Zoom_Level;

            //set the posistion.
            float ty = GetComponent<BoxCollider2D>().size.y / 2 * Main_Script.f_Zoom_Level * transform.localScale.y;
            ty += ty * .13f;
            ty += transform.position.y;
            HP_Gage.transform.position = new Vector2(transform.position.x, ty);

            //set the green bar in the hp gage to be correct amount.
            float Gage_Percentage = HP_Cur / HP_Max;
            HP_Gage.transform.GetChild(1).transform.localScale = new Vector2(Gage_Percentage, 1);
        }
        else
        {
            //not performing update cause the object was not made.
            //might add in code to remove the script later for perf reasons but should not happen except when i mess up.
        }
	
	}
}
