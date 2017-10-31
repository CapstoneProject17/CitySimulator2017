using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Interactive
/// Team: Client
/// Description: Interactive class is to handle the click of each unit. It does not handle the mouse click,
///     but will show some properties and methods, in order to be notifacated by other classes.
/// Author: Benjamin Hao Date: Oct. 23th, 2017
/// Modified by:
///     Name: Benjamin Hao   Change: Add Swap boolean  Date: Oct.29th, 2017
/// Based on:  https://www.youtube.com/watch?v=1GJWas9IyYc
/// </summary>

public class Interactive : MonoBehaviour
{

    private bool _Selected = false;   // check if the unit gets selected

    public bool Selected { get { return _Selected; } }

    public bool Swap = false; // to change selection status in editor

    public void Select()
    {
        _Selected = true;
        foreach (var selection in GetComponents<Interaction>())
        {
            selection.Select();
        }
    }

    public void Deselect()
    {
        _Selected = false;
        foreach (var selection in GetComponents<Interaction>())
        {
            selection.Deselect();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Swap)
        {
            Swap = false;
            if (_Selected) Deselect();
            else Select();
        }
    }
}