using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapButtonBehaviours : MonoBehaviour {

	private string menuSceneName = "Main menu";
	private LoadSceneMode loadMode = LoadSceneMode.Single;

	public void SaveAndExit() {
		SaveManager.GetInstance ().SaveSelectedData ();
		SceneManager.LoadScene(menuSceneName, loadMode);
	}

}
