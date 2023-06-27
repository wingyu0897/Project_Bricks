using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DATA/Shape")]
public class ShapeSO : ScriptableObject
{
	public List<Vector3Int> blocks;

	public bool CheckEquals(List<Vector3Int> inputList)
	{
		if (inputList.Count != blocks.Count)
			return false;
		else
			foreach (Vector3Int pos in blocks)
				if (inputList.Contains(pos) == false)
					return false;

		return true;
	}
}
