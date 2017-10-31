using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Highlight
/// Team: Client
/// Description: Highlight unit when it is selected
/// Author: Benjamin Hao Date: Oct. 24th, 2017
/// Modified by: N/A
/// Based on:  N/A
/// </summary>

public class Highlight : Interaction
{

    public GameObject DisplayItem; // Display unit when it is selected

    public override void Deselect()
    {
        DisplayItem.SetActive(false);
    }

    public override void Select()
    {
        DisplayItem.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        DisplayItem.SetActive(false);
    }
}