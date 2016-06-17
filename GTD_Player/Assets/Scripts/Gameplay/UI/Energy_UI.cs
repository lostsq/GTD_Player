using UnityEngine;
using System.Collections;

public class Energy_UI : MonoBehaviour {

    Player_Main_Script Main_Script;

    float f_Cur_Energy = 0;
    TextMesh Cur_Mesh;

	// Use this for initialization
	void Start () {
        Main_Script = GameObject.Find("Main_Script").GetComponent<Player_Main_Script>();
        Cur_Mesh = GetComponent<TextMesh>();

        //now to re-posistion to be dead center on top. //MIGHT NEED TO DO LATER
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.allCameras[0].orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;



    }
	
	// Update is called once per frame
	void Update () {


        if (f_Cur_Energy != Main_Script.f_Energy)
        {
            f_Cur_Energy = Main_Script.f_Energy;
            Cur_Mesh.text = "Energy: " + Main_Script.f_Energy;

            




        }

    }
}
