using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {
	
    

	public int FightNumber;
	public int MovementIncrement = 1;
    public GameObject Moon;
	float camWidth;

	// Use this for initialization
	void Start ()
    {
		

		FightNumber = SaveManager.GetInstance ().GetSelectedData ().m_FightsCompleted;
		camWidth = Camera.main.orthographicSize * Camera.main.aspect;
		Moon.transform.position = new Vector3 (-camWidth + (FightNumber * MovementIncrement), transform.position.y, transform.position.z);


	}
	
	// Update is called once per frame
	void Update ()
    {
       
        
       
    }
}
