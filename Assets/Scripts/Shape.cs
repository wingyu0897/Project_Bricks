using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeSO shape;
	public GameObject previewBlock;
	public Transform previewParent;

	private Camera cam;

	public void Init(ShapeSO shape)
	{
		cam = Camera.main;
		this.shape = shape;
		StartCoroutine(MakePreview());
	}

	private IEnumerator MakePreview()
	{
		cam.orthographicSize = 5;

		for (int i = 0; i < shape.blocks.Count; i++)
		{
			GameObject preview = Instantiate(previewBlock, shape.blocks[i], Quaternion.identity);
			preview.transform.SetParent(previewParent);
			yield return new WaitForSeconds(0.1f);
		}

		cam.orthographicSize = 6;
		cam.transform.parent.position = new Vector3(0, 0.5f, 0);
		FindObjectOfType<StartAction>().OnStart();
	}

    public bool Check(List<Vector3Int> blocks)
	{
		return shape.CheckEquals(blocks);
	}
}
