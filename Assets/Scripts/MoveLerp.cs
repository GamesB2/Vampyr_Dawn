using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour{
    public bool Visible = false;
   
   
    public Transform StartPositionGO;
    public Transform EndPositionGO;
    public float Speed;
    public float StartTime;
    public float TotalDistanceToDestination;
    
	// Use this for initialization
	void Start ()
    {     
        StartTime = Time.time;
        TotalDistanceToDestination = Vector3.Distance(StartPositionGO.position, EndPositionGO.position);
	}

    // Update is called once per frame
    void Update()
    {
            float currentDuration = (Time.time - StartTime) * Speed;
            float journeyFraction = currentDuration / TotalDistanceToDestination;
            transform.position = Vector3.Lerp(StartPositionGO.position, EndPositionGO.position, journeyFraction);
    }
}
