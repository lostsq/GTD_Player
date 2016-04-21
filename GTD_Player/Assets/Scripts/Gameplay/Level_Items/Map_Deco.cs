using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts.Gameplay.Level_Items
{
    class Map_Deco : Map_Space
    {

        public void Deco_Setup(string p_Name, int px, int py)
        {
            X_Spot = px;
            Y_Spot = py;
            Name = p_Name;

            b_Contains_Object = true;
        }


        // Use this for initialization
        void Start()
        {
            //Debug.Log("Working");
            //get the main script to connect to it/start calls in it when we are finished setting things up.
            //Main_Script = 
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
