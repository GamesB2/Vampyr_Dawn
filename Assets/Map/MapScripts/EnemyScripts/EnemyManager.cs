using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zakaria Hamdi-Pacha (14028617)

public class EnemyManager : MonoBehaviour {

	private MapRegion m_Region;
	private MapObject m_MapManager;

	private int m_MaxEnemies = 8;
	private int m_MaxEnemiesOffset = 2;
	private int m_MaxCrowdSize = 3;

	private int requestedSize;

	private int m_CurrentEnemies = 0;

	private GameObject enemyContainer;

	public Sprite enemySprite;

	// Use this for initialization
	void Start () {
		enemyContainer = new GameObject ("Enemy container (" + m_Region.m_RegionName + ")");
		RectTransform m_EnemiesRectTransform = enemyContainer.AddComponent<RectTransform> ();
		CanvasRenderer m_EnemiesCanvasRenderer = enemyContainer.AddComponent<CanvasRenderer>();

		enemyContainer.transform.SetParent (m_Region.m_RegionGameObject.transform);

		m_EnemiesRectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
		m_EnemiesRectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
		m_EnemiesRectTransform.pivot = new Vector2 (0.5f, 0.5f);

		m_EnemiesRectTransform.offsetMin = new Vector2 (0, 0);
		m_EnemiesRectTransform.offsetMax = new Vector2 (0, 0);

		m_MaxEnemies = Random.Range (m_MaxEnemies - m_MaxEnemiesOffset, m_MaxEnemies + m_MaxEnemiesOffset);
		requestedSize = Random.Range(1, m_MaxCrowdSize + 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_CurrentEnemies < m_MaxEnemies) { //haven't reached max enemies. spawn...
			//decide size of crowd.
			int crowdSize = requestedSize;
			int sizeLeft = m_MaxEnemies - m_CurrentEnemies;
			if (crowdSize > sizeLeft) {
				//crowdSize = sizeLeft;
				crowdSize = 0;
			}

			if (crowdSize > 0) {
				requestedSize = Random.Range(1, m_MaxCrowdSize + 1);

				GameObject newEnemies = createEnemies (crowdSize);
				m_CurrentEnemies += crowdSize;
			}
		}
	}

	public void SetRegion(MapRegion region) {
		m_Region = region;
	}

	public void SetMapManager(MapObject mapManager) {
		m_MapManager = mapManager;
	}

	public MapRegion GetRegion() {
		return m_Region;
	}

	public MapObject GetMapManager () {
		return m_MapManager;
	}

	public void AddToSize(int amount) {
		m_CurrentEnemies += amount;
	}

	public GameObject createEnemies(int size) {
		GameObject enemyCrowd = new GameObject (m_Region.m_RegionName + " Enemies (size: " + size + ")");

		//Set enemy info
		MapEnemy mapEnemyScript = enemyCrowd.AddComponent<MapEnemy>();
		mapEnemyScript.m_EnemyManager = this;
		mapEnemyScript.m_CrowdSize = size;
		mapEnemyScript.m_AliveTime = (float)Random.Range (15, 25);
		enemyCrowd.transform.SetParent (enemyContainer.transform);
		mapEnemyScript.SetEnemyImage ();



		return enemyCrowd;
	}

}
