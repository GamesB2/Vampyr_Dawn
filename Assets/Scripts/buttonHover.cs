using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class buttonHover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
	public AudioClip hoverSound;
	public AudioClip clickSound;
	public AudioClip downSound;

	SoundManager soundManager;

	void Start() 
	{
		soundManager = SoundManager.instance;
	}

	//Handle mouse sounds
	public void OnPointerEnter (PointerEventData ped)
	{
		soundManager.PlaySingle (hoverSound);
	}
	public void OnPointerDown (PointerEventData ped)
	{
		soundManager.PlaySingle (clickSound);
	}


}
