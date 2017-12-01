using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System;

/// <summary>
/// Module: ShowUnitInfo
/// Team: Client
/// Description: Show information of units
/// Author: Benjamin Hao Date: Oct. 28th, 2017
/// Modified by:
///     Name: Benjamin Hao   Change: modified Select() method  Date: Oct.29th, 2017
///     Name: Benjamin Hao   Change: Removed Thumbnails for information bubble Date: 2017-11-25
///     Name: Benjamin Hao   Change: Added GUID for each object. Date: 2017-11-28
/// Based on:  N/A
/// </summary>

public class GUIObjectShowUnitInfo : Interaction
{
    public TextMesh Information;  // the name of units
    public String objectGUID; // the GUID of objects

    /// <summary>
    /// override Select() method
    /// </summary>
    public override void Select()
    {

        // TODO: interact with database
        objectGUID = this.gameObject.name;
        //get the data here

        Information.text = objectGUID;
    }

    /// <summary>
    /// override Deselect() method.
    /// </summary>
    public override void Deselect()
    {
        //GUIObjectInfoManager.Current.ClearPic();
        //GUIObjectInfoManager.Current.ClearLines();
    }
}