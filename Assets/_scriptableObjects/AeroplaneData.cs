using UnityEngine;

[CreateAssetMenu(fileName = "AeroplaneData", menuName = "AeroplaneData", order = 1)]
public class AeroplaneData : ScriptableObject
{
	[SerializeField]
	protected string aircraftType;
	public string AircraftType
	{
		get => aircraftType;
		protected set => aircraftType = value;
	}

	[Header("Velocity")]
	[SerializeField]
	protected float maxSpeedInKnots;
	public float MaxSpeedInKnots
	{
		get => maxSpeedInKnots;
		protected set => maxSpeedInKnots = value;
	}

	[SerializeField]
	protected float maxAccelerationInNauticalMilesPerHourPerSecond;
	public float MaxAccelerationInNauticalMilesPerHourPerSecond
	{
		get => maxAccelerationInNauticalMilesPerHourPerSecond;
		protected set => maxAccelerationInNauticalMilesPerHourPerSecond = value;
	}

	[SerializeField]
	protected float defaultAccelerationInNauticalMilesPerHourPerSecond;
	public float DefaultAccelerationInNauticalMilesPerHourPerSecond
	{
		get => defaultAccelerationInNauticalMilesPerHourPerSecond;
		protected set => defaultAccelerationInNauticalMilesPerHourPerSecond = value;
	}

	[SerializeField]
	protected float maxDecelerationInNauticalMilesPerHourPerSecond;
	public float MaxDecelerationInNauticalMilesPerHourPerSecond
	{
		get => maxDecelerationInNauticalMilesPerHourPerSecond;
		protected set => maxDecelerationInNauticalMilesPerHourPerSecond = value;
	}

	[SerializeField]
	protected float defaultDecelerationInNauticalMilesPerHourPerSecond;
	public float DefaultDecelerationInNauticalMilesPerHourPerSecond
	{
		get => defaultDecelerationInNauticalMilesPerHourPerSecond;
		protected set => defaultDecelerationInNauticalMilesPerHourPerSecond = value;
	}

	[Header("Vertical position")]
	[SerializeField]
	protected float maxHeightInFeet;
	public float MaxHeightInFeet
	{
		get => maxHeightInFeet;
		protected set => maxHeightInFeet = value;
	}

	[SerializeField]
	protected float maxClimbRateInFeetPerMinute;
	public float MaxClimbRateInFeetPerMinute
	{
		get => maxClimbRateInFeetPerMinute;
		protected set => maxClimbRateInFeetPerMinute = value;
	}

	[SerializeField]
	protected float defaultClimbRateInFeetPerMinute;
	public float DefaultClimbRateInFeetPerMinute
	{
		get => defaultClimbRateInFeetPerMinute;
		protected set => defaultClimbRateInFeetPerMinute = value;
	}

	[SerializeField]
	protected float maxDescendRateInFeetPerMinute;
	public float MaxDescendRateInFeetPerMinute
	{
		get => maxDescendRateInFeetPerMinute;
		protected set => maxDescendRateInFeetPerMinute = value;
	}

	[SerializeField]
	protected float defaultDescendRateInFeetPerMinute;
	public float DefaultDescendRateInFeetPerMinute
	{
		get => defaultDescendRateInFeetPerMinute;
		protected set => defaultDescendRateInFeetPerMinute = value;
	}

	[Header("Turning")]
	[SerializeField]
	protected float maxTurnRateInDegreesPerSecond;
	public float MaxTurnRateInDegreesPerSecond
	{
		get => maxTurnRateInDegreesPerSecond;
		protected set => maxTurnRateInDegreesPerSecond = value;
	}

	[SerializeField]
	protected float defaultTurnRateInDegreesPerSecond;
	public float DefaultTurnRateInDegreesPerSecond
	{
		get => defaultTurnRateInDegreesPerSecond;
		protected set => defaultTurnRateInDegreesPerSecond = value;
	}
}
