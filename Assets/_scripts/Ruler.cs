using UnityEngine;

public class Ruler : MonoBehaviour
{
	void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.yellow;

		for (int i = 0; i < 200; i++)
		{
			for (int j = 0; j < 200; j++)
			{
				Gizmos.DrawCube(new Vector2(i, j), new Vector3(0.1f, 0.1f, 0.1f));
			}
		}
	}
}
