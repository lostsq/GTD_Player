using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Level_Items
{
    public class Map_Path : Map_Space
    {
        String_Tracker Current_Strings = new String_Tracker();

        public int Path_Number;

        public void Map_Path_Setup(string p_Name, int px, int py)
        {

            //possible optimzation
            //Sprite[] sprites = Resources.LoadAll<Sprite>("Textures");


            //have to set up the path and split out the name and the sprite and such.
            Path_Number = int.Parse(p_Name.Split('_')[1]);
            Name = p_Name.Split('_')[0];

            if (Name == "w")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Up_Basic);
            }
            else if (Name == "s")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Down_Basic);
            }
            else if (Name == "a")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Left_Basic);
            }
            else if (Name == "d")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Right_Basic);
            }



            X_Spot = px;
            Y_Spot = py;

            b_Contains_Object = true;
        }

        public void Set_Corner_Sprite(string Name_Code)
        {
            if (Name_Code == "wd" || Name_Code == "as")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Corner_TR_Basic);
            }
            if (Name_Code == "sd"|| Name_Code == "aw")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Corner_BR_Basic);
            }
            if (Name_Code == "wa" || Name_Code == "ds")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Corner_TL_Basic);
            }
            if (Name_Code == "sa" || Name_Code == "dw")
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Current_Strings.Sprite_Corner_BL_Basic);
            }

        }
    }
}
