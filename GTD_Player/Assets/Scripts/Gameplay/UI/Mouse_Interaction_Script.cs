using UnityEngine;
using System.Collections;

public class Mouse_Interaction_Script : MonoBehaviour {

    String_Tracker Current_Strings = new String_Tracker();

    Player_Main_Script Main_Script;

    //This is if dragging is occuring.
    bool b_Is_Dragging = false;
    //used to know if a menu is open.
    GameObject Cur_Open_Menu = null;
    //This is the object that we are working with when dragging.
    Collider2D Collider_Working_With = null;


    //used for dragging
    Vector2 V_Offset;
    Vector2 Pos_Object_Start;
    Vector2 Pos_Start;
    GameObject Old_Parent = null;
    bool b_Moving_Tower_Field = false;

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

        //setting the scroll for zoom and movement on menuss will need to add logic for mouse down with towers to return them to default spot or something.
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Collider_Working_With = null;
            Collider_Working_With = Find_Highest_Collider_At_Mouse(false);

            if (Collider_Working_With != null && Collider_Working_With.gameObject.tag == Current_Strings.Tag_Empty_Map_Spot)
            {
                float tsx = Collider_Working_With.gameObject.transform.parent.transform.localScale.x;
                float tsy = Collider_Working_With.gameObject.transform.parent.transform.localScale.y;

                //Debug.Log("cur x = " + tsx);
                //Debug.Log("cur y = " + tsy);
                //Debug.Log("Mouse_Scroll_Amount = " + Input.GetAxis("Mouse ScrollWheel"));

                //zoom in 10%
                float Temp_Zoom_Amount = .10f;
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    Temp_Zoom_Amount *= -1;
                }


                Collider_Working_With.gameObject.transform.parent.transform.localScale = new Vector2(tsx + (tsx * Temp_Zoom_Amount), tsy + (tsy * Temp_Zoom_Amount));
                Collider_Working_With = null;

            }


        }


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

                Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                Vector3 Update_Spot = curPosition - Pos_Start;
                Update_Spot.Set(Update_Spot.x, Update_Spot.y, 0);

                //check what is under it before we confirm placement.
                Collider2D Under_This = Find_Highest_Collider_At_Mouse(false);
                
                //the sprite renderers.
                SpriteRenderer Cur_SR = Collider_Working_With.gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer Under_SR = null;
                if (Under_This != null)
                {
                    Under_SR = Under_This.gameObject.GetComponent<SpriteRenderer>();
                    //in the bag
                    if (Under_This.gameObject.tag == Current_Strings.Tag_Inventory_Bag)
                    {
                        Pos_Start = curPosition;
                        Collider_Working_With.gameObject.transform.position += Update_Spot;
                        Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                        Collider_Working_With.gameObject.transform.localScale = new Vector3(1,1);
                        //sprite render stuff.
                        Cur_SR.sortingOrder = Under_SR.sortingOrder + 1;
                    }

                    //in the hotbox Need GHOST!
                    if (Under_This.gameObject.tag == Current_Strings.Tag_Hotbar_Spot)
                    {
                        //used to make sure if it is being dragged to map or from map to hotbar or just hotbar to hotbar.
                        if (Old_Parent != null)
                        {
                            if (Old_Parent.tag.Contains(Current_Strings.Tag_Empty_Map_Spot))
                            {

                                b_Moving_Tower_Field = true;
                            }
                            else
                            {
                                b_Moving_Tower_Field = false;
                            }
                        }
                        //nothing else in the hotbar.
                        if (Under_This.gameObject.transform.childCount == 0)
                        {
                            Pos_Start = Under_This.gameObject.transform.position;
                            Collider_Working_With.gameObject.transform.position = Under_This.gameObject.transform.position;
                            Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                            Collider_Working_With.gameObject.transform.localScale = Under_This.gameObject.transform.localScale;
                            //sprite render stuff.
                            Cur_SR.sortingOrder = Under_SR.sortingOrder + 1;
                        }
                    }
                    //in the map NEED GHOST!
                    if (Under_This.gameObject.tag == Current_Strings.Tag_Empty_Map_Spot)
                    {
                        //used to make sure if it is being dragged to map or from map to hotbar or just hotbar to hotbar.
                        b_Moving_Tower_Field = true;
                        //nothing else in the hotbar.
                        if (Under_This.gameObject.transform.childCount == 0)
                        {
                            //used for test
                            //Main_Script.Move_Tower_Field(Under_This.gameObject, Under_This.gameObject, Under_This.gameObject);

                            Pos_Start = Under_This.gameObject.transform.position;
                            Collider_Working_With.gameObject.transform.position = Under_This.gameObject.transform.position;
                            Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                            Collider_Working_With.gameObject.transform.localScale = Under_This.gameObject.transform.localScale;
                            //sprite render stuff.
                            Cur_SR.sortingOrder = Under_SR.sortingOrder + 1;
                        }
                    }

                }
                
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
        //Debug.Log("Mouse_Down");

        //clear things out.
        Collider_Working_With = null;
        b_Is_Dragging = false;

        //we get the highest collider at the mouses location.
        Collider2D Highest_Collider = Find_Highest_Collider_At_Mouse(false);

        //set the old parent here and reset it to null on mouse up/trigger.
        if (Old_Parent == null)
        {
            Old_Parent = Highest_Collider.gameObject.transform.parent.gameObject;
        }
        

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

        //Debug.Log("Mouse_Up");

        //need to tell if the item was being dragged or not.
        if (b_Is_Dragging)
        {
            //Debug.Log("Drag Reached Mouse Up");
            //the tower is being moved to a spot on the field.
            if (b_Moving_Tower_Field)
            {
                

                //Debug.Log("Moving Tower Reached Mouse Up");
                //enable the confirmation
                Main_Script.b_Confirmation_Open = true;
                //get the from spot and the tower we are moving.
                Main_Script.Confirmation_Objects = new GameObject[2]{Old_Parent, Collider_Working_With.gameObject};
                //set the action for if it is good.
                if (Collider_Working_With.transform.parent.gameObject.tag.Contains(Current_Strings.Tag_Hotbar_Spot))
                {
                    Main_Script.s_Confirmation_Action = Current_Strings.Confirm_Tower_To_Hotbar;
                }
                else
                {
                    Main_Script.s_Confirmation_Action = Current_Strings.Confirm_Tower_To_Field;
                }

                //move the confirmation to the spot where the item is. want it directly above the item but for now middle.
                GameObject.Find(Current_Strings.Name_Confirmation_Box).transform.position = new Vector3(0, 0);
            }
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
                    //press the button that was clicked.
                    Button_Clicked(Collider_Working_With);
                }
                
                //TOWERS
            }

        }

        //Set Defaults
        b_Is_Dragging = false;
        Collider_Working_With = null;
        b_Moving_Tower_Field = false;
        if (!Main_Script.b_Confirmation_Open)
        {
            Old_Parent = null;
        }
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

    //All button presses go through here and the proper action is taken either here or in the main script.
    //only for the menu items, not clicking a single tower, but yes the up buttons for towers and such.
    void Button_Clicked(Collider2D But_Col)
    {
        //used to easily close open menus without searching.
        string Last_Menu_Tag_Open = "none";

        //menu is open so we must now close. only for the large menus like invin, enemies, create
        if (Cur_Open_Menu != null)
        {
            //move the open menu to the off screen.
            Cur_Open_Menu.gameObject.transform.position = new Vector3(-500, -550);
            Last_Menu_Tag_Open = Cur_Open_Menu.tag;
            Cur_Open_Menu = null;
        }

        //invintory button click.
        if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Inventory_UI))
        {
            if (GameObject.Find(Current_Strings.Name_Inventory_Parent).tag != Last_Menu_Tag_Open)
            {
                GameObject.Find(Current_Strings.Name_Inventory_Parent).transform.position = new Vector3(0, 0);
                Cur_Open_Menu = GameObject.Find(Current_Strings.Name_Inventory_Parent);
            }
        }

        //confirm yes
        if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Confirmation_Yes))
        {
            Main_Script.b_Confirmation_True = true;
            Main_Script.b_Confirmation_Open = false;
        }
        //confirm no
        if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Confirmation_No))
        {
            Main_Script.b_Confirmation_True = false;
            Main_Script.b_Confirmation_Open = false;
        }

    }

}
