using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Module: ButtonManager
/// Team: Client
/// Description: start new scene, open Setting menu, and quit application
/// Author: 
///	 Name: Dongwon(Shawn) Kim   Date: 2017-09-28
///  Name: Andrew Lam			Date: 2017-09-28
/// Modified by:	
///	 Name: Benjamin Hao   Change: comment out void settingBtn()				Date: 2017-10-17
///	 Name: Dongwon Kim    Change: add grid On button                        Date: 2017-12-04
/// Based on:  
/// https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
/// </summary>
public class ButtonManager : MonoBehaviour {

	/// <summary>
	/// The main menu.
	/// </summary>
	public Transform mainMenu;

	/// <summary>
	/// The scene to load.
	/// </summary>
	public string sceneToLoad;

	/// <summary>
	/// Plaies the game button.
	/// </summary>
	public void playGameBtn() {
		SceneManager.LoadScene(sceneToLoad);
	}

    /// <summary>
    /// Grid on trigger
    /// </summary>
    public bool gridOn = true;

//	/// <summary>
//	/// Open setting panel
//	/// </summary>
//	public void settingBtn(bool clicked) {
//		if (clicked) {
//			gameSettings.gameObject.SetActive (clicked);
//			mainMenu.gameObject.SetActive (false);
//		} else {
//			gameSettings.gameObject.SetActive (clicked);
//			mainMenu.gameObject.SetActive (true);
//		}
//	}

	/// <summary>
	/// Backs to menu.
	/// </summary>
    public void backToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

	/// <summary>
	/// Refreshs the scene.
	/// </summary>
	public void refreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gridTurnOn() {
        //Debug.Log("gridTurnOn clickeD");
        if (gridOn)
        {
            GameObject.Find("Grid").GetComponent<GridManager>().turnEntireGrid(true);
            gridOn = false;
        } else
        {
            GameObject.Find("Grid").GetComponent<GridManager>().turnEntireGrid(false);
            gridOn = true;
        }
    }

	/// <summary>
	/// Exits the game button.
	/// </summary>
	public void exitGameBtn() {
		Application.Quit ();
	}
}