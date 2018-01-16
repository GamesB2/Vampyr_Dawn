using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Zakaria Hamdi-Pacha (14028617)

public class MapEnemy : MonoBehaviour {

	public int m_CrowdSize = 1;
	public float m_AliveTime = 10;
	public EnemyManager m_EnemyManager;

	private RectTransform m_RectTransform;
	private CanvasRenderer m_CanvasRenderer;
	private TrailRenderer m_TrailRenderer;

	private PointInfo m_PointInfo;


	private RectTransform regionsTransform;

	private Vector2 m_DynamicPosition;
	private Vector2 m_DynamicDestination;

	private bool m_IsImageSet = false;

	private float m_TravelTime = 0;

	public float differenceXMax;//prevent dynamic region leaving bounds of the screen.
	public float differenceXMin;
	public float differenceYMax;
	public float differenceYMin;

	void Awake() {
		m_PointInfo = gameObject.AddComponent<PointInfo> ();
	}

	// Use this for initialization
	void Start () {
		m_RectTransform = gameObject.AddComponent<RectTransform> ();
		m_CanvasRenderer =  gameObject.AddComponent<CanvasRenderer> ();

		m_TrailRenderer = gameObject.AddComponent<TrailRenderer>();
		m_TrailRenderer.material = new Material(Shader.Find("Sprites/Default"));
		m_TrailRenderer.startColor = Color.black;
		m_TrailRenderer.endColor = Color.black;

		m_PointInfo.region = m_EnemyManager.GetRegion ();
		m_PointInfo.pointOfInterest = null;

		m_TravelTime = Random.Range (7, 12);

		CircleCollider2D m_IconCircleCollider = gameObject.AddComponent<CircleCollider2D> ();
		m_IconCircleCollider.radius = m_EnemyManager.GetMapManager ().GetPreferredIconWidth () / 2;
		m_IconCircleCollider.offset = Vector2.zero;
		m_IconCircleCollider.isTrigger = true;

		m_RectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
		m_RectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
		m_RectTransform.pivot = new Vector2 (0.5f, 0.5f);

		m_RectTransform.offsetMin = new Vector2 (0, 0);
		m_RectTransform.offsetMax = new Vector2 (0, 0);

		m_RectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, m_EnemyManager.GetMapManager ().GetPreferredIconWidth ());
		m_RectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, m_EnemyManager.GetMapManager ().GetPreferredIconHeight ());

		regionsTransform = m_EnemyManager.GetRegion ().m_RegionGameObject.GetComponent<RectTransform> ();
	
		differenceXMax = (regionsTransform.sizeDelta.x / 2); //prevent dynamic region leaving bounds of the screen.
		differenceXMin = (differenceXMax *- 1);
		differenceYMax = (regionsTransform.sizeDelta.y / 2);
		differenceYMin = (differenceYMax *- 1);

		Vector2 randomPos = new Vector2(

			Random.Range(
				differenceXMin,
				differenceXMax
			),

			Random.Range(
				differenceYMin,
				differenceYMax
			)

		);
		m_RectTransform.anchoredPosition = randomPos;
		m_DynamicPosition = randomPos;
	}

	public void SetEnemyImage() {
		if (m_IsImageSet)
			return;

		m_PointInfo.crowdSize = m_CrowdSize;

		int step = 8; //gap between the icons
		int offset = -step * (m_CrowdSize / 2);

		for (int i = 0; i < m_CrowdSize; i++) {
			GameObject newIconObject = new GameObject ("Enemy Icon: " + m_EnemyManager.GetRegion().m_RegionName);

			RectTransform mrt = newIconObject.AddComponent<RectTransform> ();
			CanvasRenderer mcr = newIconObject.AddComponent<CanvasRenderer> ();

			mrt.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, m_EnemyManager.GetMapManager ().GetPreferredIconWidth ());
			mrt.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, m_EnemyManager.GetMapManager ().GetPreferredIconHeight ());

			mrt.localPosition = new Vector3(offset, offset, 0);

			Image m_Image = newIconObject.AddComponent<Image> ();
			m_Image.sprite = m_EnemyManager.GetMapManager ().EnemyHeadIcon;
			m_Image.color = m_EnemyManager.GetRegion ().m_RegionColor;

			newIconObject.transform.SetParent (gameObject.transform);
			offset += step;
		}
		m_IsImageSet = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (m_DynamicPosition, m_DynamicDestination) < 100) {
			//if we have reached the target.
			//set a new target.

			//Get random position on screen size.
			m_DynamicDestination = new Vector2(
				
				Random.Range(
					differenceXMin,
					differenceXMax
				),

				Random.Range(
					differenceYMin,
					differenceYMax
				)

			);

			m_TravelTime = Random.Range (20, 30);
		} else {
			//if we have not reached the target.
			//Lerp move towards destination.
			Vector2 pos = Vector2.Lerp(m_DynamicPosition, m_DynamicDestination,
				Time.fixedDeltaTime / (float) m_TravelTime);
			m_RectTransform.anchoredPosition = pos;
			m_DynamicPosition = pos;
		}

		m_AliveTime -= Time.deltaTime;

		if (m_AliveTime <= 0)
			SelfDestroy ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		SelfDestroy ();
	}

	void SelfDestroy () {
		m_EnemyManager.AddToSize(-m_CrowdSize);
		Destroy (gameObject);
	}

	public void SetDynamicPosition(Vector2 newPosition) {
		m_DynamicPosition = newPosition;
	}

	public void MoveDynamicPosition(Vector2 addPosition) {
		m_DynamicPosition += addPosition;
	}

	public Vector2 GetDynamicPosition() {
		return m_DynamicPosition;
	}

	public void SetDynamicDestination(Vector2 newPosition) {
		m_DynamicDestination = newPosition;
	}

	public Vector2 GetDynamicDestination() {
		return m_DynamicDestination;
	}
}
