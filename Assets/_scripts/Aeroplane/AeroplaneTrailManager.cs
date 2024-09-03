using System.Collections.Generic;
using UnityEngine;

public class AeroplaneTrailManager : MonoBehaviour
{
	[SerializeField]
	protected AeroplaneController aeroplaneController;

	[SerializeField]
	protected List<Vector2> trailPositions = new();

	[SerializeField]
	protected List<PoolObject> trailObjects = new();

	protected int numberOfTrailsToShow => Manager.Settings.NumberOfAeroplaneTrailObjectsToShow;

	public void SetTrail()
	{
		trailPositions.Add(aeroplaneController!.HorizontalPositionInNauticalMiles);

		CheckIfPoolingNeeded();
	}

	public void CheckIfPoolingNeeded()
	{
		if (trailPositions.Count < 2)
			return;

		int numberOfTrailObjectsRequired = numberOfTrailsToShow - transform.childCount;

		// Add one at a time when adding
		if (numberOfTrailObjectsRequired > 0)
		{
			UpdateNewlyPooledTrail(Manager.PoolManager.GetObjectFromPool("trail"));
			return;
		}

		// Remove many at once when removing
		if (numberOfTrailObjectsRequired < 0)
		{
			int lastIndex;
			for (int i = 0; i < numberOfTrailObjectsRequired; i++)
			{
				lastIndex = trailObjects.Count - 1;
				Manager.PoolManager.ReturnObjectToPool(trailObjects[lastIndex]);
				trailObjects.RemoveAt(lastIndex);
			}
		}

		MoveLastTrailToFirst();
	}

	public void UpdateNewlyPooledTrail(PoolObject trailObject)
	{
		trailObject.transform.position = trailPositions[trailPositions.Count - 2];
		trailObject.transform.parent = transform;
		trailObjects.Add(trailObject);
		trailObject.gameObject.SetActive(true);
	}

	public void MoveLastTrailToFirst()
	{
		PoolObject firstTrail = trailObjects[0];
		trailObjects.RemoveAt(0);
		firstTrail.transform.position = trailPositions[trailPositions.Count - 2];
		trailObjects.Add(firstTrail);
	}
}
