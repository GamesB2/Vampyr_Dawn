using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField]
	private float m_CameraSpeed;
	[SerializeField]
	private float m_CameraSensitivity;
	[SerializeField]
	private float m_XOffset;
	private float m_ZDepth;

	private Character2D m_Target;

	void Start()
	{
		m_ZDepth = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
