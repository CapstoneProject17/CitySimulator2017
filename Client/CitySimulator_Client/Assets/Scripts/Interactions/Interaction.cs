using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Interaction
/// Team: Client
/// Description: A abstract class that handles interactions between users and units
/// Author: Benjamin Hao Date: Oct. 23th, 2017
/// Modified by: N/A
/// Based on:  N/A
/// </summary>

public abstract class Interaction : MonoBehaviour
{

    public abstract void Select();
    public abstract void Deselect();
}