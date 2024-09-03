using UnityEngine;

public class TimeManager : MonoBehaviour
{
	private static float millisecondsSinceStart;
	public static float MillisecondsSinceStart { get => millisecondsSinceStart; }

	private float beforeValue;

	private void Update()
	{
		UpdateTimeAndTriggerTimeEvents();
	}

	private void UpdateTimeAndTriggerTimeEvents()
	{
		beforeValue = millisecondsSinceStart;

		millisecondsSinceStart += (1000 * GetDeltaTimeWithTimeMultiplier());

		Manager.RadarManager.CheckForUpdateRadar(beforeValue, millisecondsSinceStart);
		Manager.RadarManager.CheckForUpdateTrail(beforeValue, millisecondsSinceStart);
	}

	public float GetDeltaTimeWithTimeMultiplier()
	{
		return Time.deltaTime * Manager.Settings.TimeMultiplier;
	}
}
