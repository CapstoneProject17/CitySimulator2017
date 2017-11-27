using UnityEngine;
using System.Collections;

/// <summary>
/// Module: ShowUnitInfo
/// Team: Client
/// Description: Show information of units
/// Author: Benjamin Hao Date: Oct. 28th, 2017
/// Modified by:
///     Name: Benjamin Hao   Change: modified Select() method  Date: Oct.29th, 2017
///     Name: Benjamin Hao   Change: Removed Thumbnails for information bubble Date: 2017-11-25
/// Based on:  N/A
/// </summary>

public class GUIObjectShowUnitInfo : Interaction
{

    public string Name;  // the name of units
    public string info;   // the information of units
    public string info2; // TODO: something needs to be added
    //public Sprite ProfilePic;    // the profile picture of units

    /// <summary>
    /// override Select() method
    /// </summary>
    public override void Select()
    {
        //GUIObjectInfoManager.Current.SetPic(ProfilePic);
        GUIObjectInfoManager.Current.SetLines(
            Name,
            info,
            info2);
    }

    /// <summary>
    /// override Deselect() method.
    /// </summary>
    public override void Deselect()
    {
        //GUIObjectInfoManager.Current.ClearPic();
        GUIObjectInfoManager.Current.ClearLines();
    }
}
