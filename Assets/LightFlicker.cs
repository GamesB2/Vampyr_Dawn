using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Danny Timms T016546E
public class LightFlicker : MonoBehaviour {
	public Light light = new Light();

	private float change = 0;

	void Update () { 

		 float on = Random.Range(0.1f,1);
		 float off = Random.Range(0.1f,1);
		if(Time.time > change){
			light.enabled = !light.enabled;
			if (light.enabled) {
				change = Time.time + on;
			} else {
				change = Time.time + off;
			}
		}
	}
}
