using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseFightSceneBackground : MonoBehaviour {

	[SerializeField]
	public Sprite parkSprite, clubSprite, cliffSprite, citySprite, graveSprite, hqSprite;
	SpriteRenderer spriteRenderer;


	void Start () 
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

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
			spriteRenderer.sprite = citySprite;
		} else {
			//Specific (port/graveyard etc)
			MapObject.PointOfInterestType type = currentPOI.m_PointOfInterestType;
			switch (type) {
			case MapObject.PointOfInterestType.CLIFF_EDGE:
				spriteRenderer.sprite = cliffSprite;
				break;

			case MapObject.PointOfInterestType.GRAVEYARD_CHURCH:
				spriteRenderer.sprite = graveSprite;
				break;

			case MapObject.PointOfInterestType.GANG_HEADQUARTERS:
				spriteRenderer.sprite = hqSprite;
				break;

			case MapObject.PointOfInterestType.NIGHTCLUB:
				spriteRenderer.sprite = clubSprite;
				break;

			case MapObject.PointOfInterestType.PARK:
				spriteRenderer.sprite = parkSprite;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
