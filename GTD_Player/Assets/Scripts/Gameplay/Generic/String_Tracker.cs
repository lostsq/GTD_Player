public class String_Tracker
{

    //This entire class is used to just keep track of the strings so if one changes we don't worry about where to change it.

    public string

        //GAME OBJECT NAMES
        //Name: This is the name of the gameobject holding the main script that holds all the information in the player.
        Name_Main_Script_Holder = "Main_Script",
        Name_Hotbar_Parent = "Hotbar_Parent",
        Name_Inventory_Parent = "Inventory_Parent",
        Name_Map_Parent = "Map_Parent",
        Name_Map_Start = "Start_Point",
        Name_Map_Temple = "Temple",
        Name_Confirmation_Box = "Confirmation_Box",

        //PREFAB NAMES WITH FULL ADDRESSES
        //UI
    Prefab_Hotbar_Box = "Prefabs/UI/HotBarBox_Empty",
        Prefab_Hotbar_Middle = "Prefabs/UI/HotBarMiddle",
        Prefab_I_Bot = "Prefabs/UI/Inventory_Boxes/I_Bottom",
        Prefab_I_Bot_Left = "Prefabs/UI/Inventory_Boxes/I_Bottom_Left",
        Prefab_I_Bot_Right = "Prefabs/UI/Inventory_Boxes/I_Bottom_Right",
        Prefab_I_Empty = "Prefabs/UI/Inventory_Boxes/I_Empty",
        Prefab_I_Left = "Prefabs/UI/Inventory_Boxes/I_Left",
        Prefab_I_Right = "Prefabs/UI/Inventory_Boxes/I_Right",
        Prefab_I_Top = "Prefabs/UI/Inventory_Boxes/I_Top",
        Prefab_I_Top_Left = "Prefabs/UI/Inventory_Boxes/I_Top_Left",
        Prefab_I_Top_Right = "Prefabs/UI/Inventory_Boxes/I_Top_Right",
        Prefab_Tower_Template = "Prefabs/Towers/Tower_Template",

        //TOWERS
        Prefab_Tower_Ruby = "Prefabs/Towers/Ruby",

        //ENEMIES
        Prefab_Enemy_Location = "Prefabs/Enemies/",

        //ATTACKS
        Prefab_Attack_Tower_Location = "Prefabs/Attacks/Attack_Tower_",
        Prefab_Attack_Enemy_Location = "Prefabs/Attacks/Attack_Enemy_",

        //MAP ITEMS
        Prefab_MI_Decorations_Location = "Prefabs/Enviroment/Decorations/",
        Prefab_MI_Spots_Start = "Prefabs/Enviroment/Map_Spots/Map_Spot_Start",
        Prefab_MI_Spots_Temple = "Prefabs/Enviroment/Map_Spots/Temple",
        Prefab_MI_Path_Template = "Prefabs/Enviroment/Map_Spots/Map_Spot_Path",
        Prefab_MI_Map_Space = "Prefabs/Enviroment/Map_Spots/Map_Space",

        //PATH SPRITES
        Sprite_Up_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/Up_Basic",
        Sprite_Down_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/Down_Basic",
        Sprite_Left_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/Left_Basic",
        Sprite_Right_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/Right_Basic",
        Sprite_Corner_TL_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/TL_Corner_Basic",
        Sprite_Corner_TR_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/TR_Corner_Basic",
        Sprite_Corner_BL_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/BL_Corner_Basic",
        Sprite_Corner_BR_Basic = "Raw_Textures/Enviroment/Map_Spots/Path/BR_Corner_Basic",

        //CONFIRMATION ACTIONS
        Confirm_Tower_To_Field = "Tower_To_Field_From_Hotbar",
        Confirm_Tower_To_Hotbar = "Tower_To_Hotbar_From_Field",


    //TAG PARTS
    Tag_Part_Drag = "Drag",
        Tag_Part_Button = "Btn",

    //TAGS
    //Buttons
        Tag_Button_Inventory_UI = "Btn_Inventory",
        Tag_Button_Create_UI = "Btn_Creator",
        Tag_Button_Enemies_UI = "Btn_Enemies",
        Tag_Button_Confirmation_Yes = "Btn_Confirm_Yes",
        Tag_Button_Confirmation_No = "Btn_Confirm_No",
    //Non-Buttons
        Tag_Inventory_Bag = "Inventory_Bag",
        Tag_Hotbar_Spot = "Hotbar_Spot",

        Tag_Empty_Map_Spot = "Empty_Map_Spot_Drag",
        Tag_Start_Spawn = "Start_Spawn_Map_Spot_Drag",
        Tag_Finish_Temple = "Finish_Temple_Map_Spot_Drag",
        Tag_Tower = "Twr_Drag",
        Tag_Tower_On_Map = "Twr_Drag_On_Map",

        Tag_Enemy = "Enemy",

        Tag_Path_Maker_Field_Move = "Drag_Spot_Field";



}
