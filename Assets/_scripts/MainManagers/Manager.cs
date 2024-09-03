using UnityEngine;

public class Manager : MonoBehaviour
{
	public static Manager Instance { get; private set; }
	public static Settings Settings { get; private set; }
	public static AeroplaneManager AeroplaneManager { get; private set; }
	public static TimeManager TimeManager { get; private set; }
	public static RadarManager RadarManager { get; private set; }
	public static PoolManager PoolManager { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}

		Instance = this;

		Settings = GetComponent<Settings>();
		AeroplaneManager = GetComponent<AeroplaneManager>();
		TimeManager = GetComponent<TimeManager>();
		RadarManager = GetComponent<RadarManager>();
		PoolManager = GetComponent<PoolManager>();
	}

	private void OnDestroy()
	{
		Instance = null;
	}
}
