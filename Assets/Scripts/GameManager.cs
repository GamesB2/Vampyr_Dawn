using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_GameManager = null;
    private static GameObject m_PlayerReference = null;
	private static List<GameObject> m_EnemyAiList = null;

	Color darkest = Color.black, brightest = Color.white;
	public Slider brightnessSlider;
	public Slider audioSlider;
	public AudioSource mainAudio;
	public Toggle muteToggle;
	int toggleValue;

    public void Awake()
    {
		brightnessSlider.value = PlayerPrefs.GetFloat ("brightnessValue");
		audioSlider.value = PlayerPrefs.GetFloat ("audioValue");
		toggleValue = PlayerPrefs.GetInt ("toggleValue");

		if (toggleValue == 1)
			muteToggle.isOn = true;
		else
			muteToggle.isOn = false;

        m_PlayerReference = GameObject.Find("Player");
		m_EnemyAiList = new List<GameObject> ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("EnemyAi");
		foreach (GameObject enemy in enemies)
		{
			m_EnemyAiList.Add (enemy);
		}
    }

    static public GameManager GetInstance()
    {
        if (m_GameManager == null) m_GameManager = new GameManager();
        return m_GameManager;
    }

    static public GameObject GetPlayer()
    {
        return m_PlayerReference;
    }

	public static void Resume()
	{
		
		if (Time.timeScale == 0) 
		{
			Time.timeScale = 1;
		} else 
		{
			Time.timeScale = 0;
		}

	}
	public static void Pause()
	{

		if (Time.timeScale == 1) 
		{
			Time.timeScale = 0;
		} else 
		{
			Time.timeScale = 1;
		}

	}

	public void NewGame() {
		SaveManager _instance = SaveManager.GetInstance ();

		SaveData[] savedDatas = _instance.GetData ();
		if (savedDatas.Length >= 0)
			_instance.ClearData ();

		SaveData newData = new SaveData ();
		newData.m_CharacterName = "Bob";
		_instance.SetSelectedData (newData);


		SceneManager.LoadScene (2);

	}

	public static void Save()
	{
		SaveManager.GetInstance ().SaveSelectedData();
	}

	public void LoadGame()
	{
		SaveManager _instance = SaveManager.GetInstance ();
		SaveData[] savedDatas = _instance.GetData ();
		if (savedDatas.Length > 0) 
		{
			//Save data exists
			SaveData data = savedDatas [0];
			_instance.SetSelectedData (data);

			SceneManager.LoadScene (2, LoadSceneMode.Single);
		} else 
		{
			NewGame ();
		}

	}

	public void LoadLevel(int level)
	{
		SceneManager.LoadScene (level);
	}

	public void LeaveActionScene() 
	{
		SaveManager.GetInstance ().GetSelectedData ().IncreaseFightsCompleted ();
		SceneManager.LoadScene ("MapScene", LoadSceneMode.Single);
	}
	public void ApplicationExit()
	{
		Application.Quit();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightControl))
		{
			foreach (GameObject enemy in m_EnemyAiList)
			{
				if (enemy == null)
				{
					m_EnemyAiList.Remove (enemy);
					m_EnemyAiList.TrimExcess ();
				}
				Health hp = enemy.GetComponent<Health>();
				if (hp != null)
				{
					hp.TakeDamage (10);
				}
			}
		}

		RenderSettings.ambientLight = Color.Lerp(darkest, brightest, brightnessSlider.value);
		PlayerPrefs.SetFloat ("brightnessValue", brightnessSlider.value);
		AudioListener.volume = audioSlider.value;
		PlayerPrefs.SetFloat ("audioValue", audioSlider.value);

		if (muteToggle.isOn == true) {
			toggleValue = 1;
			AudioListener.volume = 0;
		} else {
			toggleValue = 0;
			AudioListener.volume = audioSlider.value;
		}
		PlayerPrefs.SetInt ("toggleValue", toggleValue);
	}
}
