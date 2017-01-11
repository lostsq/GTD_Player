using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class Play_Script_Temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            if (Is_Play_Hit())
            {
                SceneManager.LoadScene("Player_Main");
            }
        }
    }


    bool Is_Play_Hit()
    {
        //get where the mouse is on the screen.
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //This is the list of all the objects that overlap with the point on the screen.
        Collider2D[] col = Physics2D.OverlapPointAll(v);

        //Now we go through each of the colliders.
        foreach (Collider2D c in col)
        {
            if (c.gameObject.name == "Play_Endless")
            {
                return true;
            }
        }

        return false;
        }

}
