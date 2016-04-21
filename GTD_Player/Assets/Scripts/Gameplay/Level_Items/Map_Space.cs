using UnityEngine;
using System.Collections;

public class Map_Space : MonoBehaviour {

    public string Name;
    public int X_Spot;
    public int Y_Spot;
    //only 1 object per spot. Used to make sure no multiples on the same spot.
    public bool b_Contains_Object = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
