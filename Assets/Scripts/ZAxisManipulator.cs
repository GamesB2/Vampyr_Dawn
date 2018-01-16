using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisManipulator : MonoBehaviour {
	private Transform m_MyTransform;

	// Use this for initialization
	void Awake ()
	{
		m_MyTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 pos = m_MyTransform.position;
		pos.z = pos.y * 0.5f;
		m_MyTransform.position = pos;
		//m_MyTransform.position.z = m_MyTransform.position.y * -1;
	}
}
