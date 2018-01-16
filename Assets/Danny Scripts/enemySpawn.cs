using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemySpawn : MonoBehaviour {

	public GameObject red, blue, yellow, green, dock;
	public Transform spawnTransformers; 
	public List<Transform> spawnPoints;

	float minX, maxX;
	float minY, maxY;

	float xx,yy;

	GameObject currentEnemy;

	int maxEnemies; //change to include difficulty

	void Start () 
	{
		spawnPoints = new List<Transform> ();

		minX = -7;
		maxX = 6.5f;

		minY = -3.5f;
		maxY = 3.5f;

		MapRegion currentRegion = MapData.GetInstance ().GetRegionData ();
		int crowdSize = MapData.GetInstance ().GetCrowdSize ();

		//Color _regionColour = currentRegion.m_RegionColor;

		maxEnemies = crowdSize;


		int Enemy = Random.Range (0, 4);

		switch (currentRegion.m_Region) 
		{

		case MapObject.MapRegions.TOPLEFT: 
			currentEnemy = red;
			break;

		case MapObject.MapRegions.BOTTOMLEFT:
			currentEnemy = blue;
			break;

		case MapObject.MapRegions.BOTTOMRIGHT:
			currentEnemy = green;
			break;

		case MapObject.MapRegions.TOPRIGHT: 
			currentEnemy = yellow;
			break;

		case MapObject.MapRegions.CENTER:
		case MapObject.MapRegions.OTHER:
			currentEnemy = dock;
			break;
		default:
			Debug.Log ("No enemy to spawn");
			break;
		}

		SpawnPoints ();

	}
	void SpawnPoints(){


		for (int i = 0; i < maxEnemies; i++) 
		{
			xx = Random.Range (minX, maxX);
			yy = Random.Range (minY, maxY);
			GameObject newSpawner = new GameObject ("New Spawner");//Instantiate (spawnTransformers, new Vector3 (xx, yy, 0), Quaternion.identity);
			Transform newTrans = newSpawner.GetComponent<Transform>();
			newTrans.position = new Vector3 (xx, yy, 0);

			spawnPoints.Add(newTrans);

		}
		SpawnEnemy ();
	}
	void SpawnEnemy()
	{
		Debug.Log ("SpawnEnemy");
		for (int i = 0; i < maxEnemies; i++) 
		{
			//int SpawnIndex = Random.Range (0, spawnPoints.Count);
			Instantiate (currentEnemy, spawnPoints.ToArray()[i].position, spawnPoints.ToArray()[i].rotation);
		}
	}

	void Update () 
	{
		 
	}
}
