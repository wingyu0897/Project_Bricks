using System.Collections;
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
				inven?.currentBrick?.transMode?.transform.SetPositionAndRotation(placePosition, Quaternion.identity);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			inven.SelectNextBrick();
		}

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0) && inven.selectedBrick?.count > 0 && CanBuild(placePosition))
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
	}

	public bool CanBuild(Vector3Int startPos)
	{
		foreach (Vector3Int pos in inven.currentBrick?.data.positions)
		{
			if (placed.Contains(startPos + pos))
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
		down.transform.position = placePosition;
		down.position = placePosition;
		down.gameObject.name = down.name.Replace("(Clone)", "");

		foreach (Vector3Int pos in inven.currentBrick.data.positions)
		{
			placed.Add(placePosition + pos);
		}
		inven.PlaceBrick();

		CheckClear();
	}

	public void RemoveBlock(Brick brick)
	{
		Vector3Int position = brick.position;
		foreach (Vector3Int pos in brick.data.positions)
		{
			placed.Remove(position + pos);
		}

		inven.AddBrick(brick);
		Destroy(brick.gameObject);

		CheckClear();
	}

	public void CheckClear()
	{
		if (target.Check(placed))
		{
			if (inven.currentBrick?.transMode != null)
				inven.currentBrick.transMode.SetActive(false);
			OnClear?.Invoke();
			GameManager.Instance.ClearGame();
			this.enabled = false;
		}
	}
}
