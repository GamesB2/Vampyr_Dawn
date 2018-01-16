using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Danny Timms T016546E
public class LoadScene : MonoBehaviour {
	public int newLevel;
	public int loadLevel;

	public void NewGame() {
		SaveManager _instance = SaveManager.GetInstance ();

		SaveData[] savedDatas = _instance.GetData ();
		if (savedDatas.Length >= 0)
			_instance.ClearData ();

		SaveData newData = new SaveData ();
		newData.m_CharacterName = "Bob";
		_instance.SetSelectedData (newData);


		SceneManager.LoadScene (newLevel);

	}

	public void LoadGame()
	{
		SaveManager _instance = SaveManager.GetInstance ();
		SaveData[] savedDatas = _instance.GetData ();
		if (savedDatas.Length > 0) {
			//Save data exists
			SaveData data = savedDatas[0];
			_instance.SetSelectedData (data);

			SceneManager.LoadScene (loadLevel);
		} else {
			NewGame ();
		}
			
	}

	public void SaveGame()
	{
		SaveManager _instance = SaveManager.GetInstance ();

		SaveData[] savedDatas = _instance.GetData ();
		if (savedDatas.Length >= 0)
			_instance.ClearData ();

		SaveData newData = new SaveData ();
		newData.m_CharacterName = "Bob";
		_instance.SetSelectedData (newData);
	}

	public void SimpleLoadScene()
	{
		SceneManager.LoadScene (loadLevel);
	}

	public void LeaveActionScene() {
		SaveManager.GetInstance ().GetSelectedData ().IncreaseFightsCompleted ();
		SceneManager.LoadScene ("MapScene", LoadSceneMode.Single);
	}

}
