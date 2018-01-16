using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Zakaria Hamdi-Pacha (14028617)

public class AspectRatio : MonoBehaviour {

	private int m_RatioX = 800;
	private int m_RatioY = 600;

	public float m_IconSizeX = 32;
	public float m_IconSizeY = 32;

	private Image m_Image;
	private RectTransform m_RectTransform;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent<Image> ();
		m_RectTransform = GetComponent<RectTransform> ();

		float aspectratioWidth = m_IconSizeX / m_RatioX;
		float aspectratioHeight = m_IconSizeY / m_RatioY;

		m_IconSizeX = aspectratioWidth * (float) Screen.width;
		m_IconSizeY = aspectratioHeight * (float) Screen.height;

		m_RectTransform.sizeDelta = new Vector2 (m_IconSizeX, m_IconSizeY);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
