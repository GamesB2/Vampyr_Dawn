using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zakaria Hamdi-Pacha (14028617)

public class MapData {

	private static MapData _instance = null;

	private MapRegion _currentRegion = null;
	private MapPointOfInterest _currentPointOfInterest = null;
	private int _currentCrowdSize = 0;

	private bool debug = false;

	public static MapData GetInstance() {
		if (_instance == null)
			_instance = new MapData ();

		return _instance;
	}

	public MapRegion GetRegionData() {
		if (debug)
			_currentRegion = new MapRegion ();

		return _currentRegion;
	}

	public MapPointOfInterest GetPointOfInterestData() {
		if (debug) {
			_currentPointOfInterest = new MapPointOfInterest ();
			_currentPointOfInterest.m_PointOfInterestType = MapObject.PointOfInterestType.CLIFF_EDGE;
		}

		return _currentPointOfInterest;
	}

	public int GetCrowdSize () {
		return _currentCrowdSize;
	}

	public void SetData(MapRegion currentR, MapPointOfInterest currentPOI, int crowdSize) {
		_currentRegion = currentR;
		_currentPointOfInterest = currentPOI;
		_currentCrowdSize = crowdSize;
	}

}
