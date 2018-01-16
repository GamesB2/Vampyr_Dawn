using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Zakaria Hamdi-Pacha (14028617)

public class SaveManager {

	private static SaveManager _instance;
	private List<SaveData> m_SavedGames = new List<SaveData>();

	private SaveData m_SelectedSave;

	private string saveFolder = "savedata";
	private string saveFile = "s4v3-d4t4.l33tsp34k";
	private string fullSavePath;

	private SaveManager () {
		fullSavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + saveFolder + Path.DirectorySeparatorChar + saveFile;
		LoadData (); //load saved data;
	}

	public static SaveManager GetInstance() {
		if (_instance == null)
			_instance = new SaveManager ();
		return _instance;
	}

	public void SaveSelectedData() {
		AddSaveData (m_SelectedSave);
	}

	public void AddSaveData(SaveData save) {
		/**
		 * Currently only loading one save game, Normally add to list.
		 */
		SaveDataArray (new SaveData[] {save});
	}

	private void SaveDataArray(SaveData[] data) {
		m_SavedGames.AddRange (data);
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		Debug.Log ("Save location: " + fullSavePath);

		if (!Directory.Exists (Application.persistentDataPath + Path.DirectorySeparatorChar + saveFolder))
			Directory.CreateDirectory (Application.persistentDataPath + Path.DirectorySeparatorChar + saveFolder);
		
		FileStream file = File.Create (fullSavePath);
		binaryFormatter.Serialize (file, m_SavedGames);
		file.Close();
	}

	private void LoadData() {
		Debug.Log ("Load location: " + fullSavePath);
		if (File.Exists (fullSavePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (fullSavePath, FileMode.Open);
			m_SavedGames = (List<SaveData>)bf.Deserialize (file);
			file.Close ();
		} else {
			SaveData newSaveData = new SaveData ();
			SetSelectedData (newSaveData);
			//SaveDataArray (new SaveData[] {newSaveData});
		}
	}

	public SaveData[] GetData () {
		
		return GetData (false);
	}

	public SaveData[] GetData(bool reload) {
		/**
		 * Currently only loading one save game.
		 */
		if (reload)
			LoadData ();

		/*if (m_SavedGames.ToArray ().Length == 0) {
			SaveData newSaveData = new SaveData ();
			SetSelectedData (newSaveData);
		} else {
			SetSelectedData (m_SavedGames.ToArray () [0]);
		}*/

		return m_SavedGames.ToArray ();
	}

	public void SetSelectedData(SaveData selected) {
		m_SelectedSave = selected;
	}

	public SaveData GetSelectedData() {
		if (m_SelectedSave == null) {
		/*
		 * No save was selected but the something tried to access the data. 
		 * We'll check if we have loaded any data and load that, if not, we'll start a new game.
		 */
			if (m_SavedGames.ToArray ().Length == 0) {
				SaveData newSaveData = new SaveData ();
				SetSelectedData (newSaveData);
			} else {
				m_SelectedSave = m_SavedGames.ToArray () [0];
			}
		}
		return m_SelectedSave;
	}

	public void ClearData() {
		m_SavedGames.Clear ();
		SaveDataArray (m_SavedGames.ToArray());
	}


}
