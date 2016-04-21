using UnityEngine;
using System.Collections;

public class Mouse_Interaction_Script : MonoBehaviour {

    String_Tracker Current_Strings = new String_Tracker();

    Player_Main_Script Main_Script;

    //This is if dragging is occuring.
    bool b_Is_Dragging = false;
    //This is the object that we are working with when dragging.
    Collider2D Collider_Working_With = null;


    //used for dragging
    Vector2 V_Offset;
    Vector2 Pos_Object_Start;
    Vector2 Pos_Start;
    GameObject Old_Parent = null;

    //the on gui/mouse over bools.
    bool Running_GUI = false;

    // Use this for initialization
    void Start () {
        Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();
    }
	
	// Update is called once per frame
	void Update () {


        //Process
        //Use clicks, we check if they are over something that is dragable.
        //If over dragable then we see if a move occurs, if it does then we drag.
        //if no move occurs we wait for mouse up to perform a click action.
        //click action occurs with a mouse down on non-drag items, and mouse up on drag items.

        //scrolling only occurs in 2 spots. the map and the enemy list area.
        //transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel"));


        //need to check if a drag has occured and if it has and the item is the map or a tower then stuff.
        //if not tower we just keep it going until mouse up and then double check to make sure we are still in the collider area.

        //Check if Collider_Working_With is not null.
        if (Collider_Working_With != null && !b_Is_Dragging)
        {
            //Debug.Log(Collider_Working_With.gameObject.tag);
            //check if it's a tower or map spot.
            if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Part_Drag))
            {
                //Debug.Log("C2");
                //item can be dragged so we see if it's being dragged.
                b_Is_Dragging = Check_For_Drag();
            }
        }


        //First we check to see if the mouse is being dragged.
        if (b_Is_Dragging)
        {
            Running_GUI = false;
            //the dragging event is fired.
            Mouse_Dragging_Fired();
        }
        else
        {
            //show mouse over events.
            Running_GUI = true;
        }

        //Check and see if the left mouse click has been pressed down.
        if (Input.GetMouseButtonDown(0))
        {
            //Call the mouse down fired event.
            Mouse_Down_Fired();
        }

        //Check and see if the left mouse click as been released.
        if (Input.GetMouseButtonUp(0))
        {
            Mouse_Up_Fired();
        }


    }

    //checks if the mouse has moved from one point to another point on a dragable item.
    bool Check_For_Drag()
    {
        bool Result = false;
        Vector2 temp = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if (Pos_Start != temp)
        {
            //set the object start posistion depending if it's tower or the map.
            if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower))
            {
                Pos_Object_Start = Collider_Working_With.gameObject.transform.position;
            }
            else
            {
                Pos_Object_Start = Collider_Working_With.gameObject.transform.position;
            }
            Result = true;
            //set the posistion.

            //Debug.Log("Mouse_Drag_True");
        }

        return Result;
    }

    void Mouse_Dragging_Fired()
    {
        //check if tower or the map we are working with.
        if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower))
        {
            //while moving a tower off of the field the orginal need to be kept in place while a ghost/phantom version is shown being moved around.
            //THis is because we want to allow the tower to continue attacking while in the user is deciding where to move.
            if ((Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_On_Map)))
            {

            }
            //not on map so we can move freely.
            else
            {

            }
            

        }
        //this is the map we are moving. very simple/easy.
        else
        {
            Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

            Vector3 Update_Spot = curPosition - Pos_Start;
            Update_Spot.Set(Update_Spot.x, Update_Spot.y, 0);

            Pos_Start = curPosition;

            Collider_Working_With.gameObject.transform.parent.transform.position += Update_Spot;
        }
    }

    void Mouse_Down_Fired()
    {
        Debug.Log("Mouse_Down");

        //clear things out.
        Collider_Working_With = null;
        b_Is_Dragging = false;

        //we get the highest collider at the mouses location.
        Collider2D Highest_Collider = Find_Highest_Collider_At_Mouse(false);

        //Verify that there is a highest collider to work with.
        if (Highest_Collider != null)
        {
            //we now wait for a mouse up to perform actions or a drag so we have our object we are interacting with.

            //get the vector 3 of the spot the mouse is at in the world.
            Pos_Start = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Collider_Working_With = Highest_Collider;



            /*

            //now with this collider we just need to determain if it's a button,object,ect.
            //we will check to see what information the tag contains.
            string s_Tag = Highest_Collider.gameObject.tag;

            //Now we check if the tag contains what information.
            if (s_Tag.Contains(G_Tags.Tag_Button))
            {
                //is a menu type object so we will pass it to the menu click.
                Button_Handler.Button_Called(Highest_Collider);
            }

            if (s_Tag.Contains(G_Tags.Tag_Drag))
            {
                //is a menu type object so we will pass it to the menu click.
                Dragable_Object(Highest_Collider);
            }

            */

        }
    }

    void Mouse_Up_Fired()
    {

        Debug.Log("Mouse_Up");

        //need to tell if the item was being dragged or not.
        if (b_Is_Dragging)
        {
            
        }
        else
        {
            //since not dragging we will check if the click is still on top of the item they are clicking.
            if (Collider_Working_With == Find_Highest_Collider_At_Mouse(true))
            {
                //now we perform each clicks items be them a tower click, button click, ect.

                //BUTTONS
                if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Part_Button))
                {

                }
                //TOWERS
            }

        }

        //Set Defaults
        b_Is_Dragging = false;
        Collider_Working_With = null;
    }

    //This will find the highest layered (by sprite level) at the mouses location. It does not include what is being dragged.
    Collider2D Find_Highest_Collider_At_Mouse(bool Check_Self)
    {
        //get where the mouse is on the screen.
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //This is the list of all the objects that overlap with the point on the screen.
        Collider2D[] col = Physics2D.OverlapPointAll(v);

        //this is the highest layered collider.
        Collider2D Highest_Collider = null;


        //for self checking to see if youself is still highestest.
        Collider2D Ensure_Not_This = Collider_Working_With;
        if (Check_Self)
        {
            Ensure_Not_This = null;
        }


        //Now we go through each of the colliders.
        foreach (Collider2D c in col)
        {

            //this is to make sure it doesn't look at the dragging/working with collider.
            if (c != Ensure_Not_This)
            {
                //the sprite renderer attached to the collider object. Should be one for every 2d collider object.
                SpriteRenderer T_Sprite = c.gameObject.GetComponent<SpriteRenderer>();

                //check if there is a highest collider, and if not we set it. along with making sure there is a sprite for that collider.
                if (Highest_Collider == null && T_Sprite != null)
                {
                    Highest_Collider = c;
                }

                //here is the highest collider's sprite layer order.
                int Layer_Number = Highest_Collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

                //Make sure there is a sprite renderer before attempting to do anything with it.
                if (T_Sprite != null)
                {
                    //check if the sprite has a higher layer.
                    if (T_Sprite.sortingOrder > Layer_Number)
                    {

                        //does have a higher layer so we change the highest collider.
                        Highest_Collider = c;
                    }
                }
            }
        }

        //we return the highest
        return Highest_Collider;

    }

}
