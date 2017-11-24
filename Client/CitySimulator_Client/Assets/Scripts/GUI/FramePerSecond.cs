using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Frame per second.
/// Team: Client
/// Description: Shows FPS for the simulator
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-10-19
/// Modified by:	
///	 Name: N/A   Change: N/A	Fix bug Date: N/A
/// Based on:  N/A
/// http://answers.unity3d.com/questions/46745/how-do-i-find-the-frames-per-second-of-my-game.html
/// </summary>
public class FramePerSecond : MonoBehaviour {
	
	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI(){

        // update the frame rate
		GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());        
	}
}
