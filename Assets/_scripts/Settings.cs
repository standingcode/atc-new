using UnityEngine;

public class Settings : MonoBehaviour
{
	[SerializeField]
	private float timeMultiplier = 1f;
	public float TimeMultiplier { get => timeMultiplier; set => timeMultiplier = value; }

	[SerializeField]
	protected float minimumTrailUpdateTime = 6000f;
	public float MinimumTrailUpdateTime { get => minimumTrailUpdateTime; set => minimumTrailUpdateTime = value; }

	public float RadarUpdateTimeInMilliseconds { get => radarRPM == 0 ? 0f : (60f / radarRPM) * 1000; }

	public float TrailUpdateTimeInMilliseconds => RadarUpdateTimeInMilliseconds >= MinimumTrailUpdateTime ? RadarUpdateTimeInMilliseconds : MinimumTrailUpdateTime;

	[SerializeField]
	protected float radarRPM = 6f;

	[SerializeField]
	protected int numberOfAeroplaneTrailObjectsToShow;
	public int NumberOfAeroplaneTrailObjectsToShow { get => numberOfAeroplaneTrailObjectsToShow; set => numberOfAeroplaneTrailObjectsToShow = value; }

	[SerializeField]
	private SimulatorInstanceType simulatorInstanceType = SimulatorInstanceType.Pilot;
	public SimulatorInstanceType SimulatorInstanceType { get => SimulatorInstanceType; set => SimulatorInstanceType = value; }
}

public enum SimulatorInstanceType
{
	Pilot, Controller
}
