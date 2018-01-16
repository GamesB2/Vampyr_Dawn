using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	public class Soundonoff : MonoBehaviour {
	
		public Slider volumeSlider;
	public AudioSource audio;
		

		void Start () {
			//Make sure the entry on the Prefs exist or proceed to create it
		audio.volume = volumeSlider.value;
		Save ();
			}

		void Save(){
		PlayerPrefs.SetFloat ("Volume", volumeSlider.value);
			PlayerPrefs.Save ();
		}
	}

