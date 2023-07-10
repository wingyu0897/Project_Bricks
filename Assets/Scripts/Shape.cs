using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeSO shape;
	public GameObject previewBlock;
	public Transform previewParent;

	private float wait = 0.1f;
	private bool making = false;

	private Camera cam;

	public void Init(ShapeSO shape)
	{
		wait = 0.1f;
		making = false;

		cam = Camera.main;
		this.shape = shape;
		StartCoroutine(MakePreview());
	}

	private void Update()
	{
		if (making == true && Input.anyKeyDown)
		{
			wait = 0f;
		}
	}

	private IEnumerator MakePreview()
	{
		cam.orthographicSize = 5;
		making = true;

		for (int i = 0; i < shape.blocks.Count; i++)
		{
			GameObject preview = Instantiate(previewBlock, shape.blocks[i], Quaternion.identity);
			preview.transform.SetParent(previewParent);
			if (wait != 0f)
				yield return new WaitForSeconds(0f);
		}

		making = false;
		cam.orthographicSize = 6;
		cam.transform.parent.position = new Vector3(0, 0.5f, 0);
		FindObjectOfType<StartAction>().OnStart();

		yield return null;
	}

    public bool Check(List<Vector3Int> blocks)
	{
		return shape.CheckEquals(blocks);
	}
}
