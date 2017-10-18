using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/// <summary>
/// Module: Sound Play Control
/// Team: Client
/// Description: play different clip depends on the status Functionality: play sound clip
/// Author: 
///	 Name: Dongwon(Shawn) Kim    Date: 2017-09-30
/// Modified by:	
///	 Name: Dongwon(Shawn) Kim   Change:	Created Date: 2017-09-30
/// Based on:  https://support.unity3d.com/hc/en-us/articles/206116386-How-do-I-play-multiple-Audio-Sources-from-one-GameObject-
/// </summary>
public class SoundPlayControl : MonoBehaviour {

	public bool 	bossDead;			// is boss dead?
	public bool[] 	trackStatus;		// tracks status
	AudioSource[] 	tracks;				// tracks

	// Use this for initialization
	void Awake () {
		bossDead = false;						// set default
		trackStatus = new bool[2]{true, false}; // set default
		tracks = GetComponents<AudioSource>();	// get audio sources
	}
	
	// Update is called once per frame
	void Update () {

		try{
			if(tracks.Length >= 2)							// safe code
			if (bossDead			 						// boss is dead?
			&& trackStatus[0]) {							// first track is on?
				trackStatus [1] = true;						// switch the status
				trackStatus [0] = false;					// switch the status

				tracks[0].Stop();							// stop play the first track
				tracks[1].PlayOneShot (tracks[1].clip);		// play second track
			}
		} catch (Exception ex ){							// exception handling
			Debug.Log ("SoundPlayControl:" + ex.Message);
		}
		
	}
}
