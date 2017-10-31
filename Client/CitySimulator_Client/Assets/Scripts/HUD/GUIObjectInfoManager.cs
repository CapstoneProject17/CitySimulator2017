using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Module: InfoManager
/// Team: Client
/// Description: Show Information for units 
/// Author: Benjamin Hao Date: Oct. 28th, 2017
/// Modified by:
///     Name: Benjamin Hao   Change: Fix bug  Date: Oct.29th, 2017
/// Based on:  N/A
/// </summary>

public class GUIObjectInfoManager : MonoBehaviour
{

    public static GUIObjectInfoManager Current;

	public Image ProfilePic;  // Thumbnails of selected unit
	public Text Line1, Line2, Line3;  // the information for selected unit

    public GUIObjectInfoManager()
	{
		Current = this;
	}
	/// <summary>  
	/// set up text
	/// </summary>  
	/// <param name="line1"></param>  
	/// <param name="line2"></param>  
	/// <param name="line3"></param>  
	public void SetLines(string line1, string line2, string line3)
	{
		Line1.text = line1;
		Line2.text = line2;
		Line3.text = line3;
	}
	/// <summary>  
	/// clear text
	/// </summary>  
	public void ClearLines()
	{
		SetLines("", "", "");
	}
	/// <summary>  
	/// set up thumbnails
	/// </summary>  
	/// <param name="pic"></param>  
	public void SetPic(Sprite pic)
	{
		ProfilePic.sprite = pic;
		ProfilePic.color = Color.white;
	}
	/// <summary>  
	/// clear thumbnails  
	/// </summary>  
	public void ClearPic()
	{
		ProfilePic.color = Color.clear;
	}

	// Use this for initialization  
	void Start()
	{
		ClearLines();
		ClearPic();
	}
}
