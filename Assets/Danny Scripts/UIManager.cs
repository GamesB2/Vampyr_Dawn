using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider healthUI;
	public Text playerName;
	public Text livesText;

	public GameObject enemyUI;
	public Slider enemySlider;
	public Text enemyName;

	public float enemyUITime = 4f;

	private float enemyTimer;
	private Character2D player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Character2D> ();
		healthUI.maxValue = player.maxHealth;
		healthUI.value = player.currentHealth;
		playerName.text = "Vlad".ToString();
	}
	
	// Update is called once per frame
	void Update () {
		enemyTimer += Time.deltaTime;
		if (enemyTimer >= enemyUITime) {
			enemyUI.SetActive (false);
			enemyTimer = 0;
		}
	}

	public void UpdateHealth(int amount)
	{
		healthUI.value = amount;
	}

	public void UpdateEnemyUI(int maxHealth, int currentHealth, string name)
	{
		enemySlider.maxValue = maxHealth;
		enemySlider.value = currentHealth;
		enemyName.text = name;
		enemyTimer = 0;

		enemyUI.SetActive (true);
	}
}
