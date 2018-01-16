using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour {

	private SpriteRenderer playerRenderer = null;
	private SpriteRenderer shadowRenderer = null;

	public Vector2 shadow_offset;

	// Use this for initialization
	void Start () {
        GameObject shadowObject = new GameObject("Player_shadow");
		shadowObject.transform.SetParent (gameObject.transform);
		shadowObject.transform.localPosition = new Vector2 (shadow_offset.x /10f, shadow_offset.y /10f);

		playerRenderer = gameObject.GetComponent<SpriteRenderer> ();
		shadowRenderer = shadowObject.AddComponent<SpriteRenderer> ();
		shadowRenderer.sortingOrder = 1;

		shadowRenderer.color = Color.black;
		shadowRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
		shadowObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.8f,
			gameObject.transform.localScale.y * 0.8f,
			gameObject.transform.localScale.z * 0.8f);
		shadowObject.transform.Rotate (new Vector3(0, 0,30));
	}
	
	// Update is called once per frame
	void Update () {
		if (playerRenderer == null || shadowRenderer == null)
			return;

		shadowRenderer.sprite = playerRenderer.sprite;
	}
}
