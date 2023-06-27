using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeSO shape;
	public GameObject previewBlock;
	public Transform previewParent;

	public void Init(ShapeSO shape)
	{
		this.shape = shape;

		foreach (Vector3Int pos in shape.blocks)
		{
			GameObject preview = Instantiate(previewBlock, pos, Quaternion.identity);
			preview.transform.SetParent(previewParent);
		}
	}

    public bool Check(List<Vector3Int> blocks)
	{
		return shape.CheckEquals(blocks);
	}
}
