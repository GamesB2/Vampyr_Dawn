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
		m_Target = GameManager.GetPlayer ().GetComponent<Character2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 CameraTarget = m_Target.transform.position;
		CameraTarget.x += m_Target.m_FacingRight == true ? -m_XOffset : m_XOffset;
		CameraTarget.z = m_ZDepth;
		Vector3 moveBy = SteeringBehaviours.Steering.Arrive (CameraTarget, transform.position, m_CameraSpeed, m_CameraSensitivity);
		transform.position += moveBy * Time.deltaTime;
	}
}
