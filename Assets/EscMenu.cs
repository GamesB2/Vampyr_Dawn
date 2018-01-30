using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour 
{

	public GameObject pauseMenu;
	public Animator animR; 
	public Animator animS; 
	public Animator animL; 
	public Animator animO; 
	public Animator animQ; 

	void Awake()
	{
		animR = GetComponent<Animator> ();
		animS = GetComponent<Animator> ();
		animL = GetComponent<Animator> ();
		animO = GetComponent<Animator> ();
		animQ = GetComponent<Animator> ();
	}

	void Update () 
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				animR.SetTrigger ("Normal");	
				animS.SetTrigger ("Normal");
				animL.SetTrigger ("Normal");
				animO.SetTrigger ("Normal");
				animQ.SetTrigger ("Normal");
				Time.timeScale = 0;
				pauseMenu.SetActive (true);
			}
			else
			{
				Time.timeScale = 1;
				pauseMenu.SetActive (false);
			}
		}
	}
}

