using UnityEngine;
using System.Collections;

/// <summary>
/// Module: Rotate
/// Team: Client
/// Description: Originally, to make Quad rotate. Maybe can be implemented by other units.
/// Author: Benjamin Hao Date: Oct. 24th, 2017
/// Modified by: N/A
/// Based on: https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
/// </summary>

public class Rotate : MonoBehaviour
{
    public Vector3 Rotation = Vector3.zero;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rotation * Time.deltaTime);
    }
}
