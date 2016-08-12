using UnityEngine;
using System.Collections;

public class Text_Box_Background : MonoBehaviour {


    public GameObject Background_Item;
    public bool b_Is_Repeated = false;

    public bool b_Is_Enabled = false;

    string Last_String = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (b_Is_Enabled)
        {
            //check the string and if different we re-set the collidor.
            if (Last_String != GetComponent<TextMesh>().text)
            {
                Destroy(GetComponent<BoxCollider2D>());
                gameObject.AddComponent<BoxCollider2D>();
                //if this is not disabled for the energy at the top it will keep throwing exception cause of colliuder stuff. Was enabled, not sure why.. but sure there is a cause. commented here so i know.
                GetComponent<BoxCollider2D>().enabled = false;
                Last_String = GetComponent<TextMesh>().text;
            }


            //set the background's scale.
            float Background_Scale_x = (GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / Background_Item.GetComponent<BoxCollider2D>().size.x;
            float Background_Scale_y = (GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / Background_Item.GetComponent<BoxCollider2D>().size.y;
            Background_Item.transform.localScale = new Vector2((Background_Scale_x * .1f) + Background_Scale_x, (Background_Scale_y * .05f) + Background_Scale_y);

            //center the background to the text.
            float Background_Pos_x = transform.position.x + ((GetComponent<BoxCollider2D>().size.x * transform.localScale.x) / 2);
            float Background_Pos_y = transform.position.y - ((GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2);
            Background_Item.transform.position = new Vector2(Background_Pos_x, Background_Pos_y);

        }
        else
        {
            transform.position = new Vector2(500, 500);
            Background_Item.transform.position = new Vector2(500, 500);
        }

	}
}
