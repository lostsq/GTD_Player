using UnityEngine;
using System.Collections;

public class Text_Order : MonoBehaviour {

    public int i_Order_Number = 90;

	// Use this for initialization
	     void Start () 
     {
        GetComponent<Renderer>().sortingOrder = i_Order_Number;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
