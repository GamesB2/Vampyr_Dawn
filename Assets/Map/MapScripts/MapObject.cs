using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Zakaria Hamdi-Pacha (14028617)

public class MapObject : MonoBehaviour {

	[System.Serializable]
	public enum MapRegions
	{
		TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT, CENTER, OTHER
	};

	[System.Serializable]
	public enum PointOfInterestType
	{
		GANG_HEADQUARTERS, VILLAGE, DUNGEON,
		PORT, TRAIN_STATION, PARK, GRAVEYARD_CHURCH, NIGHTCLUB, CLIFF_EDGE
	};


	public Font m_Font;
	public Font m_EnemiesLabelFont;


	public GameObject BattlesCompletedLabel;
	public GameObject TimeLabel;

	public GameObject PlayerPrefab;

	public Sprite HeadquartersSprite;
	public Sprite DungeonSprite;
	public Sprite VillageSprite;
	public Sprite IconBackground;
	public Sprite EnemyHeadIcon;
		
	public MapRegion m_TopLeftRegion;
	public MapRegion m_TopRightRegion;
	public MapRegion m_BottomLeftRegion;
	public MapRegion m_BottomRightRegion;
	public MapRegion m_NonStaticRegion;

	private GameObject m_Player;

	private int aspectratiox = 800;
	private int aspectratioy = 600;

	private float m_PointOfInterestIconSizeX = 16;
	private float m_PointOfInterestIconSizeY = 16;

	private float requiredWidth, requiredHeight;


	private RectTransform m_NonStaticRegionRectTransform;
	private float m_NonStaticTravelTime;

	float differenceXMax, differenceXMin, differenceYMax, differenceYMin;

	void Awake() {
		differenceXMax = (Screen.width / 2); //prevent dynamic region leaving bounds of the screen.
		differenceXMin = (differenceXMax *- 1);
		differenceYMax = (Screen.height / 2);
		differenceYMin = (differenceYMax *- 1);
	}
	void Start () {
		//Spawn the player in
		m_Player = Instantiate(PlayerPrefab, transform.parent) as GameObject;

		Text battlesCompletedText = BattlesCompletedLabel.GetComponent<Text> ();
		battlesCompletedText.text = "Battles Completed: " + SaveManager.GetInstance().GetSelectedData().m_FightsCompleted;

		Text timeText = TimeLabel.GetComponent<Text> ();
		timeText.text = Timer.GetInstance ().GetTime ();

		m_NonStaticTravelTime = Random.Range (20, 30);
		//Get font for use.
		//m_Font = Resources.GetBuiltinResource<Font> ("Arial.ttf");

		float aspectratiowidth = m_PointOfInterestIconSizeX / aspectratiox;
		m_PointOfInterestIconSizeX = aspectratiowidth * (float) Screen.width;

		float aspectratioheight = m_PointOfInterestIconSizeY / aspectratioy;
		m_PointOfInterestIconSizeY = aspectratioheight * (float) Screen.height;

		//Add canvas renderer and rect transform.
		gameObject.AddComponent<CanvasRenderer> ();
		RectTransform m_MapObjectRectTransform = gameObject.AddComponent<RectTransform>();

		m_MapObjectRectTransform.localPosition = Vector3.zero;
		m_MapObjectRectTransform.anchorMin = new Vector2(0.5f, 0.5f); //set the anchors.
		m_MapObjectRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
		m_MapObjectRectTransform.pivot = new Vector2(0.5f, 0.5f);

		//Create game object which stores the separate regions.
		GameObject m_RegionsContainer = new GameObject ("Map Regions");
		m_RegionsContainer.AddComponent<CanvasRenderer> ();
		RectTransform m_RegionsContainerRectTransform = m_RegionsContainer.AddComponent<RectTransform> ();
		m_RegionsContainer.transform.SetParent (gameObject.transform.parent);

		//set anchors for region container.
		m_RegionsContainerRectTransform.anchorMin = new Vector2(0, 0); //set the anchors.
		m_RegionsContainerRectTransform.anchorMax = new Vector2(1, 1);
		m_RegionsContainerRectTransform.pivot = new Vector2(0.5f, 0.5f);
		m_RegionsContainerRectTransform.localPosition = Vector3.zero;
		m_RegionsContainerRectTransform.offsetMin = Vector2.zero;
		m_RegionsContainerRectTransform.offsetMax = Vector2.zero;

		//Get the size of the container.
		float containerWidth = m_RegionsContainerRectTransform.rect.width;
		float containerHeight = m_RegionsContainerRectTransform.rect.height;

		//Each region takes up a quarter of the container.
		requiredWidth = (containerWidth / 2) - 1; //(-1 to prevent overlapping)
		requiredHeight = (containerHeight / 2) - 1; //(-1 to prevent overlapping)


		//Create array of the 4 corners and the non-static region.
		MapRegion[] regions = new MapRegion[]{ m_TopLeftRegion, m_TopRightRegion, m_BottomLeftRegion, m_BottomRightRegion, m_NonStaticRegion };

		//Loop through each region and create UI elements for the map.
		//Create a highlighted area & draw the name of the region in the corner.
		foreach (MapRegion mRegion in regions) {
			//Create game object for region.
			GameObject m_NewRegion = new GameObject("Region: " + mRegion.m_RegionName);
			m_NewRegion.AddComponent<CanvasRenderer> ();

			RectTransform m_newRegionRectTransform = m_NewRegion.AddComponent<RectTransform> ();
			m_NewRegion.transform.SetParent (m_RegionsContainer.transform);

			mRegion.m_RegionGameObject = m_NewRegion;

			if (mRegion.m_Region == MapRegions.OTHER || mRegion.m_Region == MapRegions.CENTER)
			{ //non-locked regions should be smaller than the corners?
				m_newRegionRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, (requiredWidth/2));
				m_newRegionRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, (requiredHeight/2));
				//m_newRegionRectTransform.anchoredPosition = SaveManager.GetInstance ().GetSelectedData ().m_RegionLocation.GetLocation();
			}
			else
			{
				m_newRegionRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, requiredWidth);
				m_newRegionRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, requiredHeight);
				m_newRegionRectTransform.localPosition = Vector3.zero;
			}

			//Anchor the region correctly, based on the position on the map:
			switch (mRegion.m_Region)
			{
			case MapRegions.TOPLEFT:
				m_newRegionRectTransform.anchorMin = new Vector2 (0, 1);
				m_newRegionRectTransform.anchorMax = new Vector2 (0, 1);
				m_newRegionRectTransform.pivot = new Vector2 (0, 1);
				break;
			case MapRegions.TOPRIGHT:
				m_newRegionRectTransform.anchorMin = new Vector2 (1, 1);
				m_newRegionRectTransform.anchorMax = new Vector2 (1, 1);
				m_newRegionRectTransform.pivot = new Vector2 (1, 1);
				break;
			case MapRegions.BOTTOMLEFT:
				m_newRegionRectTransform.anchorMin = new Vector2 (0, 0);
				m_newRegionRectTransform.anchorMax = new Vector2 (0, 0);
				m_newRegionRectTransform.pivot = new Vector2 (0, 0);
				break;
			case MapRegions.BOTTOMRIGHT:
				m_newRegionRectTransform.anchorMin = new Vector2 (1, 0);
				m_newRegionRectTransform.anchorMax = new Vector2 (1, 0);
				m_newRegionRectTransform.pivot = new Vector2 (1, 0);
				break;
			case MapRegions.CENTER:
			case MapRegions.OTHER:
				//m_newRegionRectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
				//m_newRegionRectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
				//m_newRegionRectTransform.pivot = new Vector2 (0.5f, 0.5f);

				m_NonStaticRegionRectTransform = m_newRegionRectTransform;
				break;
			}

			Image i = m_NewRegion.AddComponent<Image> ();
			Color ic = i.color;
			ic.a = 0.1f;
			i.color = ic;
			//Add outline to object
			Outline m_RegionOutline = m_NewRegion.AddComponent<Outline>();
			Color colorToUse = mRegion.m_RegionColor;
			colorToUse.a = 0.3f;
			m_RegionOutline.effectColor = colorToUse;

			GameObject textObject = new GameObject ("Region Label: " + mRegion.m_RegionName);
			textObject.transform.SetParent (m_NewRegion.transform);

			RectTransform text_rt = textObject.AddComponent<RectTransform> ();

			text_rt.anchorMin = new Vector2 (0, 1);
			text_rt.anchorMax = new Vector2 (0, 1);
			text_rt.pivot = new Vector2 (0, 1);
			text_rt.offsetMin = new Vector2(0, -25);
			text_rt.offsetMax = new Vector2(200, 0);


			Text t = textObject.AddComponent<Text> ();
			t.font = m_Font;
			t.text = mRegion.m_RegionName;
			t.resizeTextForBestFit = true;

			Outline textOutline = textObject.AddComponent <Outline>();
			textOutline.effectColor = Color.black;

			//Load the saved points of interest from the serialized unity file
			MapPointOfInterest[] pointsOfInterest = mRegion.m_PointsOfInterest;
			foreach (MapPointOfInterest point in pointsOfInterest)
			{
				string m_PointName = point.m_PointOfInterestName;
				PointOfInterestType m_PointType = point.m_PointOfInterestType;
				MapRegions m_PointRegion = point.m_PointOfInterestRegion;


				GameObject m_IconBackground = new GameObject ("Point of interest (" + m_PointType + "): " + m_PointName);
				m_IconBackground.AddComponent<CanvasRenderer> ();
				RectTransform m_IconBackgroundRectTransform = m_IconBackground.AddComponent<RectTransform> ();
				Image m_IconBackgroundImage = m_IconBackground.AddComponent<Image> ();
				m_IconBackgroundImage.sprite = IconBackground;
				Color opaqueColor = colorToUse;
				opaqueColor.a = 1.0f;
				m_IconBackgroundImage.color = opaqueColor;

				m_IconBackgroundRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, m_PointOfInterestIconSizeX * 2);
				m_IconBackgroundRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, m_PointOfInterestIconSizeY * 2);

				m_IconBackground.transform.SetParent (m_NewRegion.transform);
				m_IconBackgroundRectTransform.anchoredPosition = Vector2.zero + point.m_PointOffset; //apply user set offset.


				GameObject m_PointGameObject = new GameObject (m_PointName + " icon");

				CircleCollider2D m_IconCircleCollider = m_PointGameObject.AddComponent<CircleCollider2D> ();
				m_IconCircleCollider.radius = m_PointOfInterestIconSizeX;
				m_IconCircleCollider.offset = Vector2.zero;
				m_IconCircleCollider.isTrigger = true;

				PointInfo poi_Info = m_PointGameObject.AddComponent<PointInfo> ();
				poi_Info.pointOfInterest = point;
				poi_Info.region = mRegion;

				m_PointGameObject.AddComponent<CanvasRenderer> ();
				RectTransform m_PointRectTransform = m_PointGameObject.AddComponent<RectTransform> ();
				Image m_PointImage = m_PointGameObject.AddComponent<Image> ();
				Outline m_PointOutline = m_PointGameObject.AddComponent<Outline> ();
				m_PointOutline.effectColor = colorToUse;

				//Set the image
				switch (m_PointType)
				{
				case PointOfInterestType.GANG_HEADQUARTERS:
					m_PointImage.sprite = HeadquartersSprite;
					break;
				case PointOfInterestType.VILLAGE:
					m_PointImage.sprite = VillageSprite;
					break;
				
				case PointOfInterestType.DUNGEON:
				default:
					m_PointImage.sprite = DungeonSprite;
					break;
				}

				m_PointGameObject.transform.SetParent (m_IconBackground.transform);

				//Set the position
				switch (m_PointRegion)
				{
				case MapRegions.TOPLEFT:
					m_IconBackgroundRectTransform.anchorMin = new Vector2 (0, 1);
					m_IconBackgroundRectTransform.anchorMax = new Vector2 (0, 1);
					m_IconBackgroundRectTransform.pivot = new Vector2 (0, 1);
					break;
				case MapRegions.TOPRIGHT:
					m_IconBackgroundRectTransform.anchorMin = new Vector2 (1, 1);
					m_IconBackgroundRectTransform.anchorMax = new Vector2 (1, 1);
					m_IconBackgroundRectTransform.pivot = new Vector2 (1, 1);
					break;
				case MapRegions.BOTTOMLEFT:
					m_IconBackgroundRectTransform.anchorMin = new Vector2 (0, 0);
					m_IconBackgroundRectTransform.anchorMax = new Vector2 (0, 0);
					m_IconBackgroundRectTransform.pivot = new Vector2 (0, 0);
					break;
				case MapRegions.BOTTOMRIGHT:
					m_IconBackgroundRectTransform.anchorMin = new Vector2 (1, 0);
					m_IconBackgroundRectTransform.anchorMax = new Vector2 (1, 0);
					m_IconBackgroundRectTransform.pivot = new Vector2 (1, 0);
					break;
				case MapRegions.CENTER:
				case MapRegions.OTHER:
					m_IconBackgroundRectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
					m_IconBackgroundRectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
					m_IconBackgroundRectTransform.pivot = new Vector2 (0.5f, 0.5f);

					break;
				}

				//m_PointRectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
				//m_PointRectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
				//m_PointRectTransform.pivot = new Vector2 (0.5f, 0.5f);

				m_PointRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, m_PointOfInterestIconSizeX);
				m_PointRectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, m_PointOfInterestIconSizeY);
				m_PointRectTransform.anchoredPosition = Vector2.zero;

			}

			EnemyManager m_EnemyManager = m_NewRegion.AddComponent<EnemyManager> ();
			m_EnemyManager.SetRegion (mRegion);
			m_EnemyManager.SetMapManager (this);

		}
		//Load the previously saved location of the non-static region.
		Location regionsLocation = SaveManager.GetInstance ().GetSelectedData ().m_RegionLocation;
		Vector2 posV2 = new Vector2 (regionsLocation.map_x, regionsLocation.map_y);
		m_NonStaticRegionRectTransform.localPosition = posV2;
		m_NonStaticRegion.SetDynamicPosition (posV2);

		m_RegionsContainer.transform.SetAsFirstSibling ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (m_NonStaticRegion.GetDynamicPosition (), m_NonStaticRegion.GetDynamicDestination ()) < 100) {
			//if we have reached the target.
			//set a new target.
			m_NonStaticRegion.SetDynamicDestination (new Vector2(
				Random.Range(differenceXMin + (m_NonStaticRegionRectTransform.sizeDelta.x / 2),
					differenceXMax - (m_NonStaticRegionRectTransform.sizeDelta.x / 2)),
				Random.Range(differenceYMin + (m_NonStaticRegionRectTransform.sizeDelta.y / 2),
					differenceYMax - (m_NonStaticRegionRectTransform.sizeDelta.y / 2))
			));
			m_NonStaticTravelTime = Random.Range (20, 30);
		} else {
			//if we have not reached the target.
			//Lerp move towards destination.
			Vector2 pos = Vector2.Lerp(m_NonStaticRegion.GetDynamicPosition(),
				m_NonStaticRegion.GetDynamicDestination(),
				Time.fixedDeltaTime/(float)m_NonStaticTravelTime);
			m_NonStaticRegionRectTransform.anchoredPosition = pos;
			m_NonStaticRegion.SetDynamicPosition (pos);

			//Update the current save-data's non-static region location - ready for saving.
			SaveManager.GetInstance ().GetSelectedData ().m_RegionLocation.map_x = pos.x;
			SaveManager.GetInstance ().GetSelectedData ().m_RegionLocation.map_y = pos.y;
		}
	}

	public float GetPreferredIconWidth() {
		return m_PointOfInterestIconSizeX;
	}

	public float GetPreferredIconHeight() {
		return m_PointOfInterestIconSizeY;
	}

}
