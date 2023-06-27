using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamRotate : MonoBehaviour
{
	public float speed;

	private void Update()
	{
		transform.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime * speed);
	}
}
