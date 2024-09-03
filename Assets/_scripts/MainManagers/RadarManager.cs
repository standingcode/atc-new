using System;
using UnityEngine;

public class RadarManager : MonoBehaviour
{
	private static Action radarPositionSpeedAndHeightUpdated;
	public static Action RadarPositionSpeedAndHeightUpdated { get { return radarPositionSpeedAndHeightUpdated; } set { radarPositionSpeedAndHeightUpdated = value; } }

	protected static Action trailUpdated;
	public static Action TrailUpdated { get => trailUpdated; set => trailUpdated = value; }

	public void CheckForUpdateRadar(float beforeValue, float millisecondsSinceStart)
	{
		if (Manager.Settings.RadarUpdateTimeInMilliseconds == 0)
		{
			RadarPositionSpeedAndHeightUpdated.Invoke();
			return;
		}

		if ((int)(beforeValue / Manager.Settings.RadarUpdateTimeInMilliseconds) !=
			(int)(millisecondsSinceStart / Manager.Settings.RadarUpdateTimeInMilliseconds))
		{
			RadarPositionSpeedAndHeightUpdated.Invoke();
		}
	}

	public void CheckForUpdateTrail(float beforeValue, float millisecondsSinceStart)
	{
		if ((int)(beforeValue / Manager.Settings.TrailUpdateTimeInMilliseconds) !=
			(int)(millisecondsSinceStart / Manager.Settings.TrailUpdateTimeInMilliseconds))
		{
			TrailUpdated.Invoke();
		}
	}
}
