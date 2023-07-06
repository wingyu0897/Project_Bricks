using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public Sprite image;
    public Vector3Int position;
    public GameObject transMode;
    public GameObject transPrefab;

	public List<Vector3Int> GetLocalPositions(Vector3Int origin)
	{
		List<Vector3Int> cubes = new List<Vector3Int>();

		foreach (Transform trm in transform)
		{
			cubes.Add(origin + Vector3Int.RoundToInt(trm.localPosition));
			print(origin + Vector3Int.RoundToInt(trm.localPosition));
		}

		return cubes;
	}

	public List<Vector3Int> GetWorldPositions()
	{
		List<Vector3Int> cubes = new List<Vector3Int>();

		foreach (Transform trm in transform)
		{
			cubes.Add(Vector3Int.RoundToInt(trm.position));
		}

		return cubes;
	}
}
