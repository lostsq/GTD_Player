using UnityEngine;
using System.Collections;

public class Text_Order : MonoBehaviour {

	// Use this for initialization
	     void Start () 
     {
        GetComponent<Renderer>().sortingOrder = 90;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
