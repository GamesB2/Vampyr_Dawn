using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour 
{

	public float speed;
	public Transform startMarker, endMarker;
	private float startTime;
	private float journeyLength;


	void Start () 
	{
		startTime = Time.time;
		journeyLength = Vector3.Distance (startMarker.position, endMarker.position);

	}

	void Update () 
	{
		float dist = (Time.time - startTime) * speed;
		float fracJourney = dist / journeyLength;
		transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);
	}
}
