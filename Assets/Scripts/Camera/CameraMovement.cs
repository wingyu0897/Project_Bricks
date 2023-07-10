using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraRig;
	public Camera cam;
	public float speed;

    public void Init()
	{
		cameraRig.rotation = Quaternion.Euler(Vector3.zero);
		cam = Camera.main;
	}

	private void Update()
	{
		if (cam == null)
			return;

		Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition);
		if (pos.x > 0.98f)
		{
			cameraRig.Rotate(new Vector3(0, -1f, 0) * Time.deltaTime * speed);
		}
		if (pos.x < 0.02f)
		{
			cameraRig.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime * speed);
		}
	}
}
