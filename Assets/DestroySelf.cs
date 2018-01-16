using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {


	public float delay;

	// Use this for initialization
	void Start () {
		Invoke ("deletePrefab", delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void deletePrefab()
	{
		Destroy (this.gameObject);
	}
}
