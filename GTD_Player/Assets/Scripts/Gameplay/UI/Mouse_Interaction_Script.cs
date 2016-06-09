using UnityEngine;
using System.Collections;

public class Mouse_Interaction_Script : MonoBehaviour {

    String_Tracker Current_Strings = new String_Tracker();

    Player_Main_Script Main_Script;

    //This is if dragging is occuring.
    bool b_Is_Dragging = false;
    //used to know if a menu is open. if there is then the tower drag is changed.
    GameObject Cur_Open_Menu = null;
    //This is the object that we are working with when dragging.
    Collider2D Collider_Working_With = null;
    Vector2 Scale_Working_With_Collider = new Vector2(1, 1);
    //what it currently is over.
    Collider2D Under_This = null;

    //are we just hovering?
    bool Hovering = true;


    //used for dragging
    Vector2 V_Offset;
    Vector2 Pos_Object_Start;
    Vector2 Pos_Start;
    GameObject Old_Parent = null;
    bool b_Moving_Tower_Field = false;

    bool b_Purchasing_Tower = false;

    //the on gui/mouse over bools.
    bool Running_GUI = false;

    // Use this for initialization
    void Start () {
        Main_Script = GameObject.Find(Current_Strings.Name_Main_Script_Holder).GetComponent<Player_Main_Script>();
    }
	
	// Update is called once per frame
	void Update () {

        Hovering = true;
        
        //tower/stats is open no need to check for hover.
        if (Main_Script.b_Tower_Open)
        {
            Hovering = false;
        }

        //Process
        //Use clicks, we check if they are over something that is dragable.
        //If over dragable then we see if a move occurs, if it does then we drag.
        //if no move occurs we wait for mouse up to perform a click action.
        //click action occurs with a mouse down on non-drag items, and mouse up on drag items.

        //scrolling only occurs in 2 spots. the map and the enemy list area.
        //transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel"));

        //perform a scroll check.
        Scroll_Check();


        //need to check if a drag has occured and if it has and the item is the map or a tower then stuff.
        //if not tower we just keep it going until mouse up and then double check to make sure we are still in the collider area.

        //Check if Collider_Working_With is not null.
        if (Collider_Working_With != null && !b_Is_Dragging)
        {
            Hovering = false;
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
            Hovering = false;

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
        if (Input.GetMouseButtonDown(0) && Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            Hovering = false;
            Mouse_Down_Fired();
        }

        //Check and see if the left mouse click as been released.
        if (Input.GetMouseButtonUp(0) && Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            Hovering = false;
            Mouse_Up_Fired();
        }


        //this is for hovering over a tower or enemy.
        //first we need to check if the collider is null, if it is then we can hover.
        if (Hovering == true)
        {
            //scan for a collider and if it's a tower we enable to hover/stats.
            Main_Script.Hover_Collider = Find_Highest_Collider_At_Mouse(false);
        }
        else
        {
            //set the collider to null so it doesn't show up.
            Main_Script.Hover_Collider = null;
        }


    }


    void Scroll_Check()
    {
        //setting the scroll for zoom and movement on menuss will need to add logic for mouse down with towers to return them to default spot or something.
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Collider_Working_With == null)
        {
            Collider_Working_With = null;
            Collider_Working_With = Find_Highest_Collider_At_Mouse(false);

            if (Collider_Working_With != null)
            {
                string C_Tag = Collider_Working_With.gameObject.tag;
                //scroll on map.
                if (C_Tag == Current_Strings.Tag_Empty_Map_Spot)
                {
                    float tsx = Collider_Working_With.gameObject.transform.parent.transform.localScale.x;
                    float tsy = Collider_Working_With.gameObject.transform.parent.transform.localScale.y;

                    //zoom in 10%
                    //float Temp_Zoom_Amount = .10f;
                    if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        //Temp_Zoom_Amount *= -1;
                        Main_Script.f_Zoom_Level /= .9f;
                    }
                    else
                    {
                        Main_Script.f_Zoom_Level *= .9f;
                    }

                    Collider_Working_With.gameObject.transform.parent.transform.localScale = new Vector2(Main_Script.f_Zoom_Level, Main_Script.f_Zoom_Level);
                    //Collider_Working_With.gameObject.transform.parent.transform.localScale = new Vector2(tsx + (tsx * Temp_Zoom_Amount), tsy + (tsy * Temp_Zoom_Amount));
                }
                //scroll on purchase menu
                else if (C_Tag == Current_Strings.Tag_Purchase_Background || C_Tag == Current_Strings.Tag_Tower_Display)
                {
                    float tX = GameObject.Find(Current_Strings.Name_Purchase_Parent).transform.position.x;
                    float tY = GameObject.Find(Current_Strings.Name_Purchase_Parent).transform.position.y;
                    tY += Input.GetAxis("Mouse ScrollWheel") * -2f;
                    GameObject.Find(Current_Strings.Name_Purchase_Parent).transform.position = new Vector2(tX, tY);
                }
            }

            //reset the collider back to null since scroll is ifnished.
            Collider_Working_With = null;
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
            if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Drag))
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
        if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Drag) || Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Display))
        {
            //check if a menu is open/blocking screen like invintory and such.
            if (Cur_Open_Menu == null || Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Display))
            {
                //ghost dragging.
                Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                Vector3 Update_Spot = curPosition - Pos_Start;
                Update_Spot.Set(Update_Spot.x, Update_Spot.y, 0);

                //check what is under it before we confirm placement.
                Under_This = Find_Highest_Collider_At_Mouse(true);

                //the sprite renderers.
                SpriteRenderer Cur_SR = Collider_Working_With.gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer Under_SR = null;

                if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Display))
                {
                    //set the ghost to visible.
                    Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .6f);
                    Pos_Start = Collider_Working_With.gameObject.transform.GetChild(0).position;
                    Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 100;

                    bool b_Nothing_Under = true;

                    if (Under_This != null)
                    {
                        //Debug.Log(Under_This.gameObject.tag);

                        //if under it is an empty hotspot we place it there.
                        if (Under_This.gameObject.tag == Current_Strings.Tag_Hotbar_Spot && Under_This.gameObject.transform.childCount == 0)
                        {
                            b_Nothing_Under = false;

                            b_Purchasing_Tower = true;
                            //snap it to the hotbar spot.
                            Collider_Working_With.gameObject.transform.GetChild(0).position = Under_This.gameObject.transform.position;
                            
                        }
                    }


                    //not over a hotspot and we just have it roam around.
                    if( b_Nothing_Under)
                    {
                        b_Purchasing_Tower = false;

                        Vector2 curScreenPointT = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        Vector2 curPositionT = Camera.main.ScreenToWorldPoint(curScreenPointT);

                        Vector3 Update_SpotT = curPositionT - Pos_Start;
                        Update_SpotT.Set(Update_SpotT.x, Update_SpotT.y, 0);

                        Pos_Start = curPositionT;
                        Collider_Working_With.gameObject.transform.GetChild(0).position += Update_SpotT;


                    }

                }
                else
                {
                    if (Under_This != null)
                    {
                        Under_SR = Under_This.gameObject.GetComponent<SpriteRenderer>();

                        //Same cell/spot
                        if (Under_This.gameObject == Collider_Working_With.gameObject)
                        {
                            //set ghost transparenty.
                            Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                            b_Moving_Tower_Field = false;
                        }
                        //hotspot.
                        else if (Under_This.gameObject.tag == Current_Strings.Tag_Hotbar_Spot && Under_This.gameObject.transform.childCount == 0)
                        {

                            //used to make sure if it is being dragged to map or from map to hotbar or just hotbar to hotbar.
                            if (Old_Parent != null)
                            {
                                if (Old_Parent.tag.Contains(Current_Strings.Tag_Empty_Map_Spot) && Under_This.gameObject.transform.childCount == 0)
                                {
                                    b_Moving_Tower_Field = true;
                                }
                                else
                                {
                                    b_Moving_Tower_Field = false;
                                }



                                //need to determain if it's moving around hotbar or going from map to hotbar.
                                if (b_Moving_Tower_Field)
                                {
                                    Pos_Start = Under_This.gameObject.transform.position;
                                    Collider_Working_With.gameObject.transform.GetChild(0).position = Under_This.gameObject.transform.position;

                                    //set the ghost's scale by taking what's under it's local scall and parent's local scale and timesing them together.
                                    float t_scale = Under_This.gameObject.transform.parent.transform.localScale.x / Collider_Working_With.transform.parent.parent.localScale.x;
                                    Collider_Working_With.gameObject.transform.GetChild(0).transform.localScale = new Vector2(t_scale, t_scale);

                                    //sprite render stuff.
                                    Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = Under_SR.sortingOrder + 1;
                                    //set ghost transparenty.
                                    Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .6f);
                                }
                                else
                                {
                                    Pos_Start = Under_This.gameObject.transform.position;

                                    Collider_Working_With.gameObject.transform.position = Under_This.gameObject.transform.position;
                                    Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                                    Collider_Working_With.gameObject.transform.localScale = Scale_Working_With_Collider;


                                    //sprite render stuff.
                                    Cur_SR.sortingOrder = Under_SR.sortingOrder + 2;
                                    //set ghost transparenty.
                                    Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                                }
                            }
                        }
                        //On map
                        else if (Under_This.gameObject.tag == Current_Strings.Tag_Empty_Map_Spot && Under_This.gameObject.transform.childCount == 0)
                        {

                            //Debug.Log("First");
                            //used to make sure if it is being dragged to map or from map to hotbar or just hotbar to hotbar.
                            b_Moving_Tower_Field = true;
                            //nothing else in the hotbar.
                            //used for test
                            //Main_Script.Move_Tower_Field(Under_This.gameObject, Under_This.gameObject, Under_This.gameObject);

                            Pos_Start = Under_This.gameObject.transform.position;
                            Collider_Working_With.gameObject.transform.GetChild(0).position = Under_This.gameObject.transform.position;
                            //Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;

                            //set the ghost's scale by taking what's under it's local scall and parent's local scale and timesing them together.
                            //float t_scale = Under_This.gameObject.transform.parent.transform.localScale.x;// * Collider_Working_With.gameObject.transform.localScale.x;
                            float t_scale = Under_This.gameObject.transform.parent.transform.localScale.x / Collider_Working_With.transform.parent.parent.localScale.x;

                            Collider_Working_With.gameObject.transform.GetChild(0).transform.localScale = new Vector2(t_scale, t_scale);

                            //sprite render stuff.
                            Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = Under_SR.sortingOrder + 1;
                            //Cur_SR.sortingOrder = Under_SR.sortingOrder + 2;

                            //set ghost transparenty.
                            Collider_Working_With.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .6f);
                        }
                        else
                        {
                            //Can;t move here so false on move tower.
                            b_Moving_Tower_Field = false;

                        }
                    }
                }
            }
            //non-ghost dragging.
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
                        Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform.parent;
                        Collider_Working_With.gameObject.transform.localScale = Scale_Working_With_Collider;
                        //sprite render stuff.
                        Cur_SR.sortingOrder = Under_SR.sortingOrder + 2;
                    }

                    //in the hotbox Need GHOST!
                    if (Under_This.gameObject.tag == Current_Strings.Tag_Hotbar_Spot && Under_This.gameObject.transform.childCount == 0)
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
                        Pos_Start = Under_This.gameObject.transform.position;
                        Collider_Working_With.gameObject.transform.position = Under_This.gameObject.transform.position;
                        Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                        Collider_Working_With.gameObject.transform.localScale = Scale_Working_With_Collider;
                        //sprite render stuff.
                        Cur_SR.sortingOrder = Under_SR.sortingOrder + 2;
                    }
                    //in the map NEED GHOST! need to make sure it's correctly set for the attacks and such.. could be difficult.
                    if (Under_This.gameObject.tag == Current_Strings.Tag_Empty_Map_Spot && Under_This.gameObject.transform.childCount == 0)
                    {
                        //used to make sure if it is being dragged to map or from map to hotbar or just hotbar to hotbar.
                        b_Moving_Tower_Field = true;
                        //nothing else in the hotbar.
                        //used for test
                        //Main_Script.Move_Tower_Field(Under_This.gameObject, Under_This.gameObject, Under_This.gameObject);

                        Pos_Start = Under_This.gameObject.transform.position;
                        Collider_Working_With.gameObject.transform.position = Under_This.gameObject.transform.position;
                        Collider_Working_With.gameObject.transform.parent = Under_This.gameObject.transform;
                        Collider_Working_With.gameObject.transform.localScale = Scale_Working_With_Collider;
                        //sprite render stuff.
                        Cur_SR.sortingOrder = Under_SR.sortingOrder + 2;
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

        if (Highest_Collider.gameObject.transform.parent != null)
        {
            //set the old parent here and reset it to null on mouse up/trigger.
            if (Old_Parent == null && Highest_Collider != null)
            {
                Old_Parent = Highest_Collider.gameObject.transform.parent.gameObject;
            }
        }

        //Verify that there is a highest collider to work with.
        if (Highest_Collider != null)
        {
            //we now wait for a mouse up to perform actions or a drag so we have our object we are interacting with.
            //get the vector 3 of the spot the mouse is at in the world.
            Pos_Start = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Collider_Working_With = Highest_Collider;
            Scale_Working_With_Collider = Highest_Collider.transform.localScale;
        }
    }

    void Mouse_Up_Fired()
    {

        //Debug.Log("Mouse_Up");

        //need to tell if the item was being dragged or not.
        if (b_Is_Dragging)
        {
            

            if (b_Purchasing_Tower)
            {
                //set it back to false since we did it.
                b_Purchasing_Tower = false;
                //Debug.Log("Moving Tower Reached Mouse Up");
                //enable the confirmation
                Main_Script.b_Confirmation_Open = true;
                //Just need the tower, and the hotspot we are going to.
                Main_Script.Confirmation_Objects = new GameObject[2] {Collider_Working_With.gameObject, Under_This.gameObject };


                Main_Script.s_Confirmation_Action = Current_Strings.Confirm_Tower_Purchase;

                //move the confirmation to the spot where the item is. want it directly above the item but for now middle.
                GameObject.Find(Current_Strings.Name_Confirmation_Box).transform.position = new Vector3(0, 0);

                
            }

            //Debug.Log("Drag Reached Mouse Up");
            //the tower is being moved to a spot on the field.
            else if (b_Moving_Tower_Field)
            {
                b_Moving_Tower_Field = false;

                //Debug.Log("Moving Tower Reached Mouse Up");
                //enable the confirmation
                Main_Script.b_Confirmation_Open = true;
                //get the from spot and the tower we are moving.
                Main_Script.Confirmation_Objects = new GameObject[3]{Old_Parent, Collider_Working_With.gameObject, Under_This.gameObject};
               
                
                //set the action for if it is good.
                if (Old_Parent.tag.Contains(Current_Strings.Tag_Empty_Map_Spot) && Under_This.gameObject.tag.Contains(Current_Strings.Tag_Hotbar_Spot))
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
            //for resetting the ghost on drag.
            else if (Collider_Working_With.tag.Contains(Current_Strings.Tag_Tower))
            {
                //we just set the ghost back to no opacity.
                Collider_Working_With.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

            }
        }
        else
        {
            if (Collider_Working_With != null)
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
                    //clicked tower.
                    if (Collider_Working_With.gameObject.tag.Contains(Current_Strings.Tag_Tower_Drag))
                    {
                        Main_Script.b_Tower_Open = true;
                        Main_Script.Tower_Open_Collider = Collider_Working_With;
                    }
                }
            }

        }

        //Set Defaults
        b_Is_Dragging = false;
        Collider_Working_With = null;
        Under_This = null;
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

        //confirm yes
        if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Confirmation_Yes))
        {
            Main_Script.b_Confirmation_True = true;
            Main_Script.b_Confirmation_Open = false;
        }
        //confirm no
        else if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Confirmation_No))
        {
            Main_Script.b_Confirmation_True = false;
            Main_Script.b_Confirmation_Open = false;
        }

        //when you click outside the box of the various pop-ups. it closes and cancels if necssary.
        else if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Background_Fade_Item))
        {
            Main_Script.b_Confirmation_True = false;
            Main_Script.b_Confirmation_Open = false;

            Main_Script.b_Tower_Open = false;
        }
        else if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Plus_Icon))
        {
            Main_Script.Plus_Icon_Clicked(But_Col);
        }

        //we are acessing a new menu item or something.
        else
        {
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

            //Purchase button click.
            if (But_Col.gameObject.tag.Contains(Current_Strings.Tag_Button_Purchase_UI))
            {
                if (GameObject.Find(Current_Strings.Name_Purchase_Parent).tag != Last_Menu_Tag_Open)
                {
                    GameObject.Find(Current_Strings.Name_Purchase_Parent).transform.position = new Vector3(0, 0);
                    Cur_Open_Menu = GameObject.Find(Current_Strings.Name_Purchase_Parent);
                }
            }
        }
       

        

    }

}
