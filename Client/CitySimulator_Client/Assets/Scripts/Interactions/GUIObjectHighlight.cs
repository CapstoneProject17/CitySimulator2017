using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Highlight
/// Team: Client
/// Description: Highlight unit when it is selected
/// Author: Benjamin Hao Date: Oct. 24th, 2017
/// Modified by: Benjamin Hao   Change: Added DisplayInfo   Date: Nov.25th, 2017
/// Based on:  N/A
/// </summary>

public class GUIObjectHighlight : Interaction
{

    public GameObject DisplayItem; // Display unit when it is selected
    public GameObject DisplayInfo; // Display unit when it is selected


    public override void Deselect()
    {
        DisplayItem.SetActive(false);
        DisplayInfo.SetActive(false);
    }

    public override void Select()
    {
        DisplayItem.SetActive(true);
        DisplayInfo.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        DisplayItem.SetActive(false);
        DisplayInfo.SetActive(false);
    }
}