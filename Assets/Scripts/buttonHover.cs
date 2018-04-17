using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
	GameObject AudioObject;
	AudioSource SFXSource;

	public AudioClip hoverSound;
	public AudioClip clickSound;

	void Awake(){
		
		AudioObject = GameObject.FindGameObjectWithTag ("SFXSource");
		if (AudioObject != null)
			SFXSource = AudioObject.GetComponent<AudioSource> ();
		else
			Debug.Log ("Audio Source is null");

		hoverSound = (AudioClip)Resources.Load("AudioClips/Button_001");
		if (hoverSound == null)
			Debug.Log ("hover sound is null");

		clickSound = (AudioClip)Resources.Load("AudioClips/Screamer_001");
		if (clickSound == null)
			Debug.Log ("click sound is null");
	}

	//Handle mouse sounds
	public void OnPointerEnter (PointerEventData ped)
	{
		SFXSource.clip = (hoverSound);
		SFXSource.PlayOneShot (SFXSource.clip, 0.5f);
	}
	public void OnPointerDown (PointerEventData ped)
	{
		SFXSource.clip = (clickSound);
		SFXSource.PlayOneShot (SFXSource.clip, 0.5f);	
	}


}
