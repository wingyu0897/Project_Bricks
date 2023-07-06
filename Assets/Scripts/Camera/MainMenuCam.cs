using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour
{
	public float speed;
	public Camera cam;

	private Color prevColor;
	private Color nextColor;
	public float changeTime;
	private float currTime = 0;

	private void Awake()
	{
		prevColor = cam.backgroundColor;
		nextColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
	}

	private void Update()
	{
		transform.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime * speed);
		SetColor();
	}

	private void SetColor()
	{
		currTime += Time.deltaTime;
		float progress = currTime / changeTime;
		cam.backgroundColor = Color.Lerp(prevColor, nextColor, progress);
		if (progress >= 1f)
		{
			currTime = 0;
			prevColor = nextColor;
			nextColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.9f, 0.9f, 1f, 1f);
		}
	}
}
