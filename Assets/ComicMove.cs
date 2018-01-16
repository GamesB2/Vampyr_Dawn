using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Author T016546e Daniel Timms
public class ComicMove : MonoBehaviour {
	
	public int level; 
	public GameObject skipScene;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			skipScene.SetActive (true);
		}
		if (gameObject.transform.position == new Vector3(1.2f, -1.5f, 0))
		{
			SceneManager.LoadScene (level);
		}
	}
}
