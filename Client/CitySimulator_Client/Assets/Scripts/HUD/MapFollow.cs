using UnityEngine;

/// <summary>
/// Module: MapFollow
/// Team: Client
/// Description: Make Map follow the main camera
/// Author: Benjamin Hao Date: Nov. 23rd, 2017
/// Modified by: N/A
/// Based on:  Creating a Basic MiniMap in Unity 3D
///            https://www.youtube.com/watch?v=ZuV9Xlt-l6g&t=29s
/// </summary>
/// 
public class MapFollow : MonoBehaviour
{
    public Transform Target;

    private void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
    }
}