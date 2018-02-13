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
	public Animator animM;
	bool isPaused;


	void Awake()
	{
		animR = GetComponent<Animator> ();
		animS = GetComponent<Animator> ();
		animL = GetComponent<Animator> ();
		animO = GetComponent<Animator> ();
		animQ = GetComponent<Animator> ();
		animM = GetComponent<Animator> ();
		isPaused = false;
	}

	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if (!isPaused) 
			{
				isPaused = true;
				Pause ();
			}

			else if (isPaused == true)
			{
				isPaused = false;
				Resume ();
			}
		}
	}

	public void Pause()
	{
		StartCoroutine (waitAndSetScale ());

	}

	public void Resume()
	{
		GameManager.Resume ();
		StartCoroutine (resumeGameScale());
	}

	IEnumerator waitAndSetScale() 
	{
		pauseMenu.SetActive (true);
		animR.SetTrigger ("Normal");	
		animS.SetBool ("Normal", true);
		animL.SetBool ("Normal", true);
		animO.SetBool ("Normal", true);
		animQ.SetBool ("Normal", true);
		animM.SetTrigger ("MainMenu");	

			yield return new WaitForSeconds (2);

		GameManager.Pause ();
	}
		 
	IEnumerator resumeGameScale()
	{
		animR.SetTrigger ("Return");
		animS.SetBool ("Return", true);
		animL.SetBool ("Normal 0", true);
		animO.SetBool ("Normal 0", true);
		animQ.SetBool ("Normal 0", true);
		animM.SetTrigger ("MainMenu");	

			yield return new WaitForSeconds(0f);
		pauseMenu.SetActive (false);

	}



}

