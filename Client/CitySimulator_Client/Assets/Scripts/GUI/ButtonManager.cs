using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Button Manager.
/// Menu button functions
/// Functionality: start new scene, open Setting menu, and quit application
/// Resource: https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
/// Author: Andrew Lam
/// Update by: Benjamin Hao
/// </summary>
public class ButtonManager : MonoBehaviour {

	public Transform mainMenu, gameSettings;
	public string levelToLoad;

	// start game
	public void playGameBtn(string scene) {
		SceneManager.LoadScene(levelToLoad);
	}

//	// Open setting panel
//	public void settingBtn(bool clicked) {
//		if (clicked) {
//			gameSettings.gameObject.SetActive (clicked);
//			mainMenu.gameObject.SetActive (false);
//		} else {
//			gameSettings.gameObject.SetActive (clicked);
//			mainMenu.gameObject.SetActive (true);
//		}
//	}

    // go back to previous scene
    public void backToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

	// reload the current scene
	public void refreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	// close application
	public void exitGameBtn() {
		Application.Quit ();
	}
}