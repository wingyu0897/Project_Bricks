using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlaceBrick : MonoBehaviour
{
	public Transform placeParent;

	private Shape target;
	private BrickInventory inven;
	private Camera mainCamera;
    private Vector3Int placePosition;
	private AudioSource audioSource;

	public List<Vector3Int> placed;

	public UnityEvent OnPlaceBrick = null;
	public UnityEvent OnRemoveBrick = null;
	public UnityEvent OnClear = null;

	private void Awake()
	{
		mainCamera = Camera.main;
		inven = GetComponent<BrickInventory>();
		target = GetComponent<Shape>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		RaycastHit hit;
		Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 50f);
		if (hit.collider != null)
		{
			if (hit.transform.parent != null && hit.transform.parent.CompareTag("Brick"))
			{
				placePosition = new Vector3Int(Mathf.RoundToInt(hit.point.x + hit.normal.x / 2), Mathf.RoundToInt(hit.point.y + hit.normal.y / 2), Mathf.RoundToInt(hit.point.z + hit.normal.z / 2));
			}
			else
			{
				placePosition = new Vector3Int(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
			}
			if (inven.currentBrick?.transMode != null)
			{
				inven.currentBrick.transMode.transform.position = placePosition;
			}
			if (inven.currentBrick != null)
			{
				inven.currentBrick.transform.position = placePosition;
				while (!CanBuild())
				{
					placePosition += new Vector3Int(0, 1, 0);
					inven.currentBrick.transMode.transform.position = placePosition;
					inven.currentBrick.transform.position = placePosition;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			inven.SelectNextBrick();
		}

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0) && inven.selectedBrick?.count > 0 && CanBuild())
		{
			BuildBlock();
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (hit.transform.parent != null && hit.collider != null)
			{
				if (hit.transform.parent.TryGetComponent(out Brick brick))
				{
					RemoveBlock(brick);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			inven.currentBrick?.transMode?.transform.Rotate(new Vector3(0, -90, 0), Space.World);
			inven.currentBrick?.transform.Rotate(new Vector3(0, -90, 0), Space.World);
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			inven.currentBrick?.transMode?.transform.Rotate(new Vector3(90, 0, 0), Space.World);
			inven.currentBrick?.transform.Rotate(new Vector3(90, 0, 0), Space.World);
		}
	}

	public bool CanBuild()
	{
		foreach (Vector3Int pos in inven.currentBrick.GetWorldPositions())
		{
			if (placed.Contains(pos) || pos.y < 0)
			{
				return false;
			}
		}

		return true;
	}

	public void BuildBlock()
	{
		audioSource.Play();
		Brick down = Instantiate(inven.currentBrick, placeParent);
		down.gameObject.SetActive(true);
		down.transform.position = placePosition;
		down.position = placePosition;
		down.gameObject.name = down.name.Replace("(Clone)", "");

		foreach (Vector3Int pos in down.GetWorldPositions())
		{
			placed.Add(pos);
		}
		//inven.currentBrick.transform.eulerAngles = Vector3.zero;
		inven.PlaceBrick();

		OnPlaceBrick?.Invoke();
		CheckClear();
	}

	public void RemoveBlock(Brick brick)
	{
		Vector3Int position = brick.position;
		foreach (Vector3Int pos in brick.GetWorldPositions())
		{
			placed.Remove(pos);
		}

		inven.AddBrick(brick);
		Destroy(brick.gameObject);

		OnRemoveBrick?.Invoke();
		CheckClear();
	}

	public void CheckClear()
	{
		if (target.Check(placed))
		{
			if (inven.currentBrick?.transMode != null)
				inven.currentBrick.transMode.SetActive(false);
			OnClear?.Invoke();
			FindObjectOfType<ClearAction>().OnClear();
			this.enabled = false;
		}
	}
}
