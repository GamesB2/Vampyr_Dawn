using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour {
	// needs to take in varying factors for the map and enemies - Where is the fight, What Gang.
	public string SceneType;
	public int GangType;
	public int MoonPosition;
	public GameObject City;
	public GameObject Club;
	public GameObject Graveyard;
	public GameObject Subway;
	public GameObject Cliff;

	public GameObject GangHQ;
	public GameObject Park;

	// Use this for initialization
	void Start () 
	{
		MapData currentMapData = MapData.GetInstance ();
		MapRegion currentRegion = currentMapData.GetRegionData ();
		MapPointOfInterest currentPOI = currentMapData.GetPointOfInterestData ();

		int crowdSize = 0;

		if (currentPOI == null)
			crowdSize = crowdSize = currentMapData.GetCrowdSize(); //Crowd of enemies, 
		else
			crowdSize = Random.Range(1, 8); //Points of interest - set random crowd size?


		if (currentPOI == null) { // ran into crowd of people.
			//enable generic backround
			SpriteRenderer ParkR = Park.GetComponent<SpriteRenderer> ();
			ParkR.enabled = true;
		} else {
			//Specific (port/graveyard etc)
			MapObject.PointOfInterestType type = currentPOI.m_PointOfInterestType;
			switch (type) {
			case MapObject.PointOfInterestType.CLIFF_EDGE:
				SpriteRenderer CliffR = Cliff.GetComponent<SpriteRenderer> ();
				CliffR.enabled = true;
				break;

			case MapObject.PointOfInterestType.GRAVEYARD_CHURCH:
				SpriteRenderer GraveyardR = Graveyard.GetComponent<SpriteRenderer> ();
				GraveyardR.enabled = true;
				break;

			case MapObject.PointOfInterestType.GANG_HEADQUARTERS:
				SpriteRenderer GangHQR = GangHQ.GetComponent<SpriteRenderer> ();
				GangHQR.enabled = true;
				break;

			case MapObject.PointOfInterestType.NIGHTCLUB:
				SpriteRenderer NightclubR = Club.GetComponent<SpriteRenderer> ();
				NightclubR.enabled = true;
				break;

			case MapObject.PointOfInterestType.PARK:
				SpriteRenderer ParkR = Park.GetComponent<SpriteRenderer> ();
				ParkR.enabled = true;
				break;

			case MapObject.PointOfInterestType.TRAIN_STATION:
				SpriteRenderer SubwayR = Subway.GetComponent<SpriteRenderer> ();
				SubwayR.enabled = true;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


}
