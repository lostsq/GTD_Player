using UnityEngine;
using System.Collections;

public class HP_Bar_Display : MonoBehaviour {

    //the object type is used to know what to check for on the parent.
    //n = nothing, e = enemy, t = temple.
    char Object_Type = 'n';

    Player_Main_Script Main_Script;

	// Use this for initialization
	void Start () {
        Main_Script = GameObject.Find("Main_Script").GetComponent<Player_Main_Script>();

        if (transform.parent.tag.Contains(Main_Script.Current_Strings.Tag_Enemy))
        {
            Object_Type = 'e';
        }
        else if (transform.parent.tag.Contains(Main_Script.Current_Strings.Tag_Finish_Temple))
        {
            Object_Type = 't';
        }

    }
	
	// Update is called once per frame
	void Update () {


	
	}
}
