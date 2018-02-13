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
	bool isPaused;


	void Awake()
	{
		animR = GetComponent<Animator> ();
		animS = GetComponent<Animator> ();
		animL = GetComponent<Animator> ();
		animO = GetComponent<Animator> ();
		animQ = GetComponent<Animator> ();
		isPaused = false;
	}

	void Update () 
	{

		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if (!isPaused)
			{
				StartCoroutine (waitAndSetScale());
				//isPaused = true;
			}
			else
			{
				StartCoroutine (resumeGameScale());
				//isPaused = false;
			}
		}

	}

	IEnumerator waitAndSetScale() 
	{
		pauseMenu.SetActive (true);
		animR.SetBool ("Normal", true);	
		animS.SetBool("Normal", true);
		animL.SetBool ("Normal", true);
		animO.SetBool ("Normal", true);
		animQ.SetBool ("Normal", true);

		while(animQ.GetComponent<Animator> ().isInitialized)
			yield return new WaitForSeconds(1);
		
		pauseMenu.SetActive (true);
		//Time.timeScale = 0;
	}
		 
	IEnumerator resumeGameScale()
	{
		Time.timeScale = 1;
		Debug.Log (animR.GetInteger("speed"));

		animR.SetBool ("Normal", false);
		animS.SetBool ("Normal 0", true);
		animL.SetBool ("Normal 0", true);
		animO.SetBool ("Normal 0", true);
		animQ.SetBool ("Normal 0", true);

		while(!animQ.GetComponent<Animator> ().isInitialized)
			yield return new WaitForSeconds(1);
		
		pauseMenu.SetActive (false);

	}
//
//

}

