using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Anamations
/// Team: Client
/// Description: Originally, to make Quad rotate. Maybe can be implemented by other units.
///     Update: Now this class handles all animations.
/// Author: Benjamin Hao Date: 24.10.2017
/// Modified by:
///     Author: Benjamin Hao Change: Change this class from Rotate to Animation, which will
///         handle all the animation in the future. Date: 31.10.2017
///     Author: Benjamin Hao Change: Added facingCamera animation   Date: 2017-11-25
/// Based on: https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
///           https://www.youtube.com/watch?v=SmG52MBWAy4
/// </summary>

public class Animations : MonoBehaviour
{
    // Animation choices
    public enum Animation
    {
        None,
        Rotate,
        facingCamera
    } // can add other animations later, eg. scaling

    public Animation Type;
    public Vector3 Action = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (Type)
        {
            default:
            case Animation.None:
                break;
            case Animation.Rotate:
                transform.Rotate(Action * Time.deltaTime);
                break;
            case Animation.facingCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up);
                break;
                //case Animation.Scale:
        }
    }
}
