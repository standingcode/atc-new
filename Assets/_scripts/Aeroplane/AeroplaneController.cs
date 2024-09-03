using UnityEngine;

public class AeroplaneController : MonoBehaviour
{
	[SerializeField]
	protected string callsign;
	public string Callsign { get => callsign; set => callsign = value; }

	[SerializeField]
	protected Transform MainBlipTransform;

	[Header("Horizontal position \n")]
	[SerializeField]
	protected Vector2 horizontalPositionInNauticalMiles;
	public Vector2 HorizontalPositionInNauticalMiles { get => horizontalPositionInNauticalMiles; set => horizontalPositionInNauticalMiles = value; }

	[Header("Vertical position \n")]
	[SerializeField]
	protected float verticalPositionInFeet;
	public float VerticalPositionInFeet { get => verticalPositionInFeet; set => verticalPositionInFeet = value; }

	[SerializeField]
	protected float targetVerticalPositionInFeet;
	public float TargetVerticalPositionInFeet { get => targetVerticalPositionInFeet; set => targetVerticalPositionInFeet = value; }

	[SerializeField]
	protected float climbRateInFeetPerMinute;
	public float ClimbRateInFeetPerMinute { get => climbRateInFeetPerMinute; set => climbRateInFeetPerMinute = value; }

	[SerializeField]
	protected float descendRateInFeetPerMinute;
	public float DescendRateInFeetPerMinute { get => descendRateInFeetPerMinute; set => descendRateInFeetPerMinute = value; }

	[Header("Velocity \n")]
	[SerializeField]
	protected float speedInKnots;
	public float SpeedInKnots { get => speedInKnots; set => speedInKnots = value; }

	[SerializeField]
	protected float targetSpeedInKnots;
	public float TargetSpeedInKnots { get => targetSpeedInKnots; set => targetSpeedInKnots = value; }

	[SerializeField]
	protected float accelerationInNauticalMilesPerHourPerSecond;
	public float AccelerationInNauticalMilesPerHourPerSecond { get => accelerationInNauticalMilesPerHourPerSecond; set => accelerationInNauticalMilesPerHourPerSecond = value; }

	[SerializeField]
	protected float decelerationInNauticalMilesPerHourPerSecond;
	public float DecelerationInNauticalMilesPerHourPerSecond { get => decelerationInNauticalMilesPerHourPerSecond; set => decelerationInNauticalMilesPerHourPerSecond = value; }


	[Header("Heading \n")]
	[SerializeField]
	protected float headingInDegrees;
	public float HeadingInDegrees
	{
		get => headingInDegrees;
		set
		{
			headingInDegrees = value % 360f;
			headingInDegrees = headingInDegrees < 0 ? 360 - Mathf.Abs(headingInDegrees) : headingInDegrees;
		}
	}

	[SerializeField]
	protected float targetHeadingInDegrees;
	public float TargetHeadingInDegrees
	{
		get => targetHeadingInDegrees;
		set
		{
			targetHeadingInDegrees = value % 360f;
			targetHeadingInDegrees = targetHeadingInDegrees < 0 ? 360 - Mathf.Abs(targetHeadingInDegrees) : targetHeadingInDegrees;
		}
	}

	[SerializeField]
	protected float turnRateInDegreesPerSecond;
	public float TurnRateInDegreesPerSecond { get => turnRateInDegreesPerSecond; set => turnRateInDegreesPerSecond = value; }

	[SerializeField]
	protected RequestedTurnDirection requestedTurnDirection = RequestedTurnDirection.Nearest;
	public RequestedTurnDirection RequestedTurnDirection
	{
		get => requestedTurnDirection;
		set
		{
			currentlyTurning = false;
			requestedTurnDirection = value;
		}
	}

	protected TurnDirection turnDirection = TurnDirection.Left;
	protected float previousTargetHeading = -1;
	protected float numberOfDegreesToRotate = -1;
	protected bool currentlyTurning = false;
	protected float headingDifference = -1;


	[Header("Other \n")]
	[SerializeField]
	protected AeroplaneData aeroplaneData;
	public AeroplaneData AeroplaneData { get => aeroplaneData; set => aeroplaneData = value; }

	// Whether aircraft is on flight levels or flying on adjusted air pressure heights above sea level
	[SerializeField]
	protected HeightOrFlightLevel heightOrFlightLevel = HeightOrFlightLevel.FlightLevel;
	public HeightOrFlightLevel HeightOrFlightLevel { get => heightOrFlightLevel; set => heightOrFlightLevel = value; }

	// METHODS

	public void Update()
	{
		ChangeSpeed();
		ChangeVerticalPosition();
		ChangeHeading();

		UpdateAircraftRealHorizontalPosition();
	}

	// Changing heading

	private void ChangeHeading()
	{
		if (HeadingInDegrees == TargetHeadingInDegrees)
		{
			currentlyTurning = false;
			return;
		}

		if (!currentlyTurning || previousTargetHeading != TargetHeadingInDegrees)
		{
			currentlyTurning = true;
			previousTargetHeading = TargetHeadingInDegrees;
			numberOfDegreesToRotate = GetNumberOfDegreesToRotateAndSetDirection();
		}

		MakeTurn();
	}

	private float GetNumberOfDegreesToRotateAndSetDirection()
	{
		if (requestedTurnDirection == RequestedTurnDirection.Nearest)
		{
			return GetNumberOfDegreesAndSetNearestTurn();
		}

		if (requestedTurnDirection == RequestedTurnDirection.Left)
		{
			return GetNumberOfDegreesAndSetLeftTurn();
		}

		return GetNumberOfDegreesAndSetRightTurn();
	}

	private float GetNumberOfDegreesAndSetLeftTurn()
	{
		turnDirection = TurnDirection.Left;

		// If we will turn through 0/360
		if (TargetHeadingInDegrees > HeadingInDegrees)
			return HeadingInDegrees + (360 - TargetHeadingInDegrees);

		return HeadingInDegrees - TargetHeadingInDegrees;
	}

	private float GetNumberOfDegreesAndSetRightTurn()
	{
		turnDirection = TurnDirection.Right;

		// If we will turn through 0/360
		if (TargetHeadingInDegrees < HeadingInDegrees)
			return TargetHeadingInDegrees + (360 - HeadingInDegrees);

		return TargetHeadingInDegrees - HeadingInDegrees;
	}

	private float GetNumberOfDegreesAndSetNearestTurn()
	{
		headingDifference = HeadingInDegrees - TargetHeadingInDegrees;

		if (headingDifference > 0)
		{
			if (headingDifference > 180)
			{
				turnDirection = TurnDirection.Right;
				return 360 - headingDifference;
			}

			turnDirection = TurnDirection.Left;
			return headingDifference;

		}

		headingDifference = Mathf.Abs(headingDifference);

		if (headingDifference > 180)
		{
			turnDirection = TurnDirection.Left;
			return 360 - headingDifference;
		}

		turnDirection = TurnDirection.Right;
		return headingDifference;
	}

	private void MakeTurn()
	{
		float degreesToTurnThisFrame = TurnRateInDegreesPerSecond * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier();
		numberOfDegreesToRotate -= degreesToTurnThisFrame;

		if (numberOfDegreesToRotate <= 0)
		{
			HeadingInDegrees = TargetHeadingInDegrees;
			currentlyTurning = false;
			return;
		}

		if (turnDirection == TurnDirection.Left)
			HeadingInDegrees -= degreesToTurnThisFrame;
		else
			HeadingInDegrees += degreesToTurnThisFrame;
	}

	// Changing speed

	private void ChangeSpeed()
	{
		// Required to decelerate
		if (SpeedInKnots > TargetSpeedInKnots)
		{
			Decelerate();
		}
		// Required to Accelerate
		else if (SpeedInKnots < TargetSpeedInKnots)
		{
			Accelerate();
		}
	}

	private void Accelerate()
	{
		if (SpeedInKnots + (AccelerationInNauticalMilesPerHourPerSecond * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier()) > TargetSpeedInKnots)
		{
			SpeedInKnots = TargetSpeedInKnots;
			return;
		}

		SpeedInKnots += AccelerationInNauticalMilesPerHourPerSecond * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier();
	}

	private void Decelerate()
	{
		if (SpeedInKnots - (DecelerationInNauticalMilesPerHourPerSecond * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier()) < TargetSpeedInKnots)
		{
			SpeedInKnots = TargetSpeedInKnots;
			return;
		}

		SpeedInKnots -= AccelerationInNauticalMilesPerHourPerSecond * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier();
	}


	// Changing vertical position

	private void ChangeVerticalPosition()
	{
		// Required to climb
		if (VerticalPositionInFeet < TargetVerticalPositionInFeet)
		{
			Climb();
		}
		else if (VerticalPositionInFeet > TargetVerticalPositionInFeet)
		{
			Descend();
		}
	}

	private void Climb()
	{
		if (VerticalPositionInFeet + (ClimbRateInFeetPerMinute / 60 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier()) > TargetVerticalPositionInFeet)
		{
			VerticalPositionInFeet = TargetVerticalPositionInFeet;
			return;
		}

		VerticalPositionInFeet += ClimbRateInFeetPerMinute / 60 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier();
	}

	private void Descend()
	{
		if (VerticalPositionInFeet - (ClimbRateInFeetPerMinute / 60 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier()) < TargetVerticalPositionInFeet)
		{
			VerticalPositionInFeet = TargetVerticalPositionInFeet;
			return;
		}

		VerticalPositionInFeet -= ClimbRateInFeetPerMinute / 60 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier();
	}


	// Update horizontal position
	protected void UpdateAircraftRealHorizontalPosition()
	{
		HorizontalPositionInNauticalMiles += new Vector2(
			SpeedInKnots / 3600 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier() * Mathf.Sin(HeadingInDegrees * Mathf.Deg2Rad),
			SpeedInKnots / 3600 * Manager.TimeManager.GetDeltaTimeWithTimeMultiplier() * Mathf.Cos(HeadingInDegrees * Mathf.Deg2Rad));
	}

	// Update on radar
	public void UpdateAeroplaneHorizontalPositionOnRadar()
	{
		MainBlipTransform.position = new Vector3(HorizontalPositionInNauticalMiles.x, horizontalPositionInNauticalMiles.y, 0.01f);
	}
}

public enum TurnDirection
{
	Left, Right
}

public enum RequestedTurnDirection
{
	Nearest, Left, Right
}

public enum HeightOrFlightLevel
{
	Height, FlightLevel
}