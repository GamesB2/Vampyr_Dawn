using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbPathFinder : MonoBehaviour
{
	/* this is just a testing script to prove that the pathfinder works
	 * keep it and refer to it whenever you change the pathfinding system.
	*/
	private List<Waypoint> myPath;
	// Use this for initialization
	void Start ()
	{
		myPath = PathFinding.DumbSearch.findPath (new Vector3(-14,-3,0), new Vector3(12,7,0));
	}
	
	// Update is called once per frame
	void Update ()
	{
		for(int i = 0; i < myPath.Count - 1; i++)
		{
			Debug.DrawLine(myPath[i].transform.position, myPath[i + 1].transform.position, Color.red);
		}
	}
}
