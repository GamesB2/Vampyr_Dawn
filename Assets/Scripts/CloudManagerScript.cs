using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManagerScript : MonoBehaviour
{
    //Set this variable to your Cloud Prefab through the Inspector
    public GameObject[] cloudPrefab;




    //Set this variable to how often you want the Cloud Manager to make clouds in seconds.
    //For Example, I have this set to 2
    public float delay;

    //If you ever need the clouds to stop spawning, set this variable to false, by doing: CloudManagerScript.spawnClouds = false;
    public static bool spawnClouds = true;

	public static int currentClouds = 0;
	public int maxClouds = 10;

	public float timer = 0;
    // Use this for initialization
    void Start()
    {
        //Begin SpawnClouds Coroutine
        
    }

	void Update() {

		if (timer > delay) {
			if (currentClouds < maxClouds) {
				StartCoroutine(SpawnClouds());
			}

			timer = 0;
		}


		timer += Time.deltaTime;
	}
		
    IEnumerator SpawnClouds()
	{
		//Instantiate Cloud Prefab and then wait for specified delay, and then repeat
		Instantiate (cloudPrefab[Random.Range(0, cloudPrefab.Length)]);
		currentClouds++;
		Debug.Log (currentClouds);
					
		yield return new WaitForSeconds (0);//delay);
	}
}