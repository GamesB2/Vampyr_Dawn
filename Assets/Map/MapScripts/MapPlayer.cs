using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Zakaria Hamdi-Pacha (14028617)

public class MapPlayer : MonoBehaviour {

	private List<KeyCode> keys = new List<KeyCode>();
	private Image m_Image;
	private Rigidbody2D m_RigidBody;
	private RectTransform m_RectTransform;
	private float playerSpeedX = 20f;
	private float playerSpeedY = 20f;

	private string actionSceneName = "Wills_Scene";
	private LoadSceneMode loadMode = LoadSceneMode.Single;

	private int aspectratiox = 800;
	private int aspectratioy = 600;

	// Use this for initialization
	void Start () {
		//Ensure player is in front of all other objects by setting last in hierarchy.
		transform.SetAsLastSibling();
		keys.AddRange (new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow });

		m_RigidBody = GetComponent<Rigidbody2D> ();

		//scale movement speed with screen size
		float speedratiox = playerSpeedX / aspectratiox;
		float speedratioy = playerSpeedY / aspectratioy;

		playerSpeedX = speedratiox * (float) Screen.width;
		playerSpeedY = speedratioy * (float) Screen.height;

		bool firstInit = SaveManager.GetInstance().GetSelectedData().m_Time == 0;
		if (firstInit) {
			//set random position.
			transform.localPosition += new Vector3(-150, -150, 0);
			SaveManager.GetInstance ().GetSelectedData ().m_Time += 1;
		} else {
			//load previous position.
			transform.localPosition += SaveManager.GetInstance().GetSelectedData().m_Location.GetLocation();
		}		

	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyCode kc in keys) {
			if (!Input.GetKeyDown (kc))
				continue;
			switch (kc) {
			case KeyCode.W: //up
			case KeyCode.UpArrow:
			case KeyCode.A: //left
			case KeyCode.LeftArrow:
			case KeyCode.S: //down
			case KeyCode.DownArrow:
			case KeyCode.D: //right
			case KeyCode.RightArrow:
				SaveManager.GetInstance ().GetSelectedData ().m_Location.SetLocation (transform.localPosition); //save location when player moves.
				break;
			}
		}
		SaveManager.GetInstance ().GetSelectedData ().m_Time += Time.deltaTime;
	}
		
	void FixedUpdate () {
		Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeedX, Input.GetAxisRaw("Vertical") * playerSpeedY);
		m_RigidBody.velocity = targetVelocity;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject collidedObject = collider.gameObject;
		//TODO: add bounds so player cannot walk out of map.
		PointInfo info = collidedObject.GetComponent<PointInfo>();
		MapData.GetInstance ().SetData (info.region, info.pointOfInterest, info.crowdSize);
		SceneManager.LoadScene(actionSceneName, loadMode);
	}
}
