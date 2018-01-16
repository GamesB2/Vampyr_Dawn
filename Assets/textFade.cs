using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class textFade : MonoBehaviour {

	public Image obj;

	private Color alphaColor;
	public float fadeTime;

	void Start () {
		obj.color = new Color (1, 1, 1, 0);
		StartCoroutine (FadeImage (true));

	}
	
	IEnumerator FadeImage(bool fadeAway)
	{
		// fade from opaque to transparent
		if (fadeAway) {
			// loop over 1 second backwards
			for (float i = fadeTime; i >= 0; i -= Time.deltaTime) {
				// set color with i as alpha
				obj.color = new Color (1, 1, 1, i);
				yield return null;
			}
		} else {
			for (float i = fadeTime; i <= 0; i += Time.deltaTime) {
				// set color with i as alpha
				obj.color = new Color (1, 1, 1, i);
				yield return null;
			}
		}
	}
//		IEnumerator FadeIn(bool fadeAway){
//			// fade from opaque to transparent
//			if (fadeAway)
//			{
//				for (float i = 0; i <= fadeTime; i += Time.deltaTime)
//				{
//					// set color with i as alpha
//					obj.color = new Color(1, 1, 1, i);
//				StartCoroutine (FadeImage (true));
//					yield return null;
//				}
//
//			}
//
//			else
//			{
//				
//				for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
//				{
//					// set color with i as alpha
//					obj.color = new Color(1, 1, 1, i);
//				StartCoroutine (FadeImage (true));
//					yield return null;
//				}
//			}
			
}

