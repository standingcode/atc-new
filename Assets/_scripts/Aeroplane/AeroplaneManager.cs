using NativeSerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AeroplaneManager : MonoBehaviour
{
	[SerializeField]
	private GameObject aeroplanePrefab;

	[SerializeField]
	private SerializableDictionary<string, AeroplaneData> aeroplaneData = new();

	[SerializeField]
	private SerializableDictionary<string, AeroplaneController> allAeroplanes = new();

	private Action setTrail;
	public Action SetTrail { get { return setTrail; } set { setTrail = value; } }

	public int numOfPlanes = 1;


	void Start()
	{
		List<string> callsigns = new() { "BAW", "ALA" };

		for (int i = 0; i < numOfPlanes; i++)

		{
			// Main instantiation (could pool this later)

			GameObject aeroplanePrefab = Instantiate(this.aeroplanePrefab);
			AeroplaneController aeroplaneController = aeroplanePrefab.GetComponent<AeroplaneController>();
			AeroplaneTrailManager aeroplaneTrailManager = aeroplanePrefab.GetComponentInChildren<AeroplaneTrailManager>();

			// Set data and subscribe to delegates
			SetStartingData(aeroplaneController);
			SubscribeToDelegates(aeroplaneController, aeroplaneTrailManager);

			string callsignNumber = i.ToString("000");

			// Add to the overall list of aeroplanes
			allAeroplanes.Add(callsigns[i % 2] + callsignNumber, aeroplaneController);
		}

		// Create the number of trail objects which will be needed initially

		Manager.PoolManager.InstantiateObjectsDisabled("trail", numOfPlanes * Manager.Settings.NumberOfAeroplaneTrailObjectsToShow);
	}

	public void SetStartingData(AeroplaneController aeroplaneController)
	{
		// Set all the data
		aeroplaneController.AeroplaneData = aeroplaneData["737"];

		// Random until there are maps and exercises
		aeroplaneController.VerticalPositionInFeet
		= aeroplaneController.TargetVerticalPositionInFeet
		= Extensions.RoundToInterval(Random.Range(10000, 30001), 1000);

		aeroplaneController.SpeedInKnots = aeroplaneController.TargetSpeedInKnots = Random.Range(250, 501);
		aeroplaneController.HorizontalPositionInNauticalMiles = new Vector2(
			Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize),
			Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));

		aeroplaneController.HeadingInDegrees = aeroplaneController.TargetHeadingInDegrees = Random.Range(1, 361);

		aeroplaneController.ClimbRateInFeetPerMinute = aeroplaneController.AeroplaneData.DefaultClimbRateInFeetPerMinute;
		aeroplaneController.DescendRateInFeetPerMinute = aeroplaneController.AeroplaneData.DefaultDescendRateInFeetPerMinute;

		aeroplaneController.AccelerationInNauticalMilesPerHourPerSecond = aeroplaneController.AeroplaneData.DefaultAccelerationInNauticalMilesPerHourPerSecond;
		aeroplaneController.DecelerationInNauticalMilesPerHourPerSecond = aeroplaneController.AeroplaneData.DefaultDecelerationInNauticalMilesPerHourPerSecond;

		aeroplaneController.TurnRateInDegreesPerSecond = aeroplaneController.AeroplaneData.DefaultTurnRateInDegreesPerSecond;
	}

	public void SubscribeToDelegates(AeroplaneController aeroplaneController, AeroplaneTrailManager aeroplaneTrailManager)
	{
		// Aeroplane controller updates the horizontal position when radar updates
		RadarManager.RadarPositionSpeedAndHeightUpdated += aeroplaneController.UpdateAeroplaneHorizontalPositionOnRadar;

		// Visible trails are updated separately
		RadarManager.TrailUpdated += aeroplaneTrailManager.SetTrail;

		// First positions
		aeroplaneTrailManager.SetTrail();
		aeroplaneController.UpdateAeroplaneHorizontalPositionOnRadar();
	}
}
