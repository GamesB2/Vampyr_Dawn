using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer {

	private int FightDurationMinutes = 10;

	private static Timer _instance;

	private Timer () {

	}

	public static Timer GetInstance() {
		if (_instance == null)
			_instance = new Timer ();
		return _instance;
	}

	public string GetTime() {
		TimeSpan timeSpan = GetTimeSpan ();
		return String.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}

	public TimeSpan GetTimeSpan() {
		TimeSpan timeSpan = TimeSpan.FromMinutes (SaveManager.GetInstance ().GetSelectedData ().m_FightsCompleted * FightDurationMinutes);
		return timeSpan;
	}

}
