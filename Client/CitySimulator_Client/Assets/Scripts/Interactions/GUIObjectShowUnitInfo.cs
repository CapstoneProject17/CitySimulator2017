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
///     Name: Benjamin Hao   Change: Added InstanceID. Date: 2017-11-28
/// Based on:  N/A
/// </summary>

public class GUIObjectShowUnitInfo : Interaction
{
    public TextMesh GUID;  // the name of units
    //public TextMesh info1;   // the information of units
    //public TextMesh info2; // TODO: something needs to be added
    //public Sprite ProfilePic;    // the profile picture of units

    /// <summary>
    /// override Select() method
    /// </summary>
    public override void Select()
    {
        //GUIObjectInfoManager.Current.SetPic(ProfilePic);
        //GUIObjectInfoManager.Current.SetLines(
        //    GUID,
        //    info1,
        //    info2);
        //GUID = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //info1 = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //info2 = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //string guid = GetInstanceID().ToString();
        //GUID.text = guid;
        //info1.text = "test2";
        //info2.text = "test3";
        Guid g = Guid.NewGuid();
        GUID.text = g.ToString();
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