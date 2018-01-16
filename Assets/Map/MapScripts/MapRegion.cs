using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zakaria Hamdi-Pacha (14028617)

[System.Serializable]
public class MapRegion {

	public string m_RegionName;
	public MapObject.MapRegions m_Region;
	public Color m_RegionColor;
	public MapPointOfInterest[] m_PointsOfInterest;
	public GameObject m_RegionGameObject;

	private Vector2 m_DynamicPosition;
	private Vector2 m_DynamicDestination;

	public void SetDynamicPosition(Vector2 newPosition) {
		m_DynamicPosition = newPosition;
	}

	public void MoveDynamicPosition(Vector2 addPosition) {
		m_DynamicPosition += addPosition;
	}

	public Vector2 GetDynamicPosition() {
		return m_DynamicPosition;
	}

	public void SetDynamicDestination(Vector2 newPosition) {
		m_DynamicDestination = newPosition;
	}

	public Vector2 GetDynamicDestination() {
		return m_DynamicDestination;
	}

}
