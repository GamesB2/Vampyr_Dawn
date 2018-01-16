using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zakaria Hamdi-Pacha (14028617)

[System.Serializable]
public struct Location {

	public float map_x, map_y;

	public Vector3 GetLocation() {
		return new Vector3 (map_x, map_y, 0);
	}

	public void SetLocation(Vector2 vec) {
		SetLocation (vec.x, vec.y);
	}

	public void SetLocation(Vector3 vec) {
		SetLocation (vec.x, vec.y);
	}

	public void SetLocation(float x, float y) {
		map_x = x;
		map_y = y;
	}

}

[System.Serializable]
public class SaveData {

	public string m_CharacterName;
	public float m_Time;
	public int m_Score;
	public int m_EnemiesKilled;
	public int m_FightsCompleted;
	public float[] m_BossChances = new float[5];
	public Location m_Location;
	public Location m_RegionLocation;

	public SaveData() {
		m_CharacterName = "Momo";
		m_Time = 0;
		m_Score = 0;
		m_EnemiesKilled = 0;
		m_FightsCompleted = 0;
		m_Location = new Location ();
		m_Location.map_x = 0; m_Location.map_y = 0;
		m_RegionLocation = new Location ();
		m_RegionLocation.map_x = 0; m_RegionLocation.map_y = 0;
	}

	public void IncreaseFightsCompleted() {
		m_FightsCompleted += 1;
	}

	public void IncreaseBossChance1() {
		m_BossChances [0] += 10;
	}

	public float GetBossChance1() {
		return m_BossChances [0];
	}

}
