using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//h028617e

public class SizeBoxToScreen : MonoBehaviour {

	private Vector3[] screenCorners;

	[SerializeField]
	private Transform tx;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetCornersOfScreen(float distance) {
		Vector3[] corners = new Vector3 [4];

		float halfFOV = (Camera.main.fieldOfView * 0.5f) * Mathf.Deg2Rad;
		float aspect = Camera.main.aspect;

		float height = distance * Mathf.Tan (halfFOV);
		float width = height * aspect;


	}
}
