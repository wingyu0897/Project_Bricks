using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickItem
{
    public BrickItem(Brick prefab, int count)
	{
        this.prefab = prefab;
        this.count = count;
	}

    public Brick prefab;
    public int count;
    public BlockSlot slot;
}

public class BrickInventory : MonoBehaviour
{
    [SerializeField]
    private List<BrickItem> ownBricks;

    public BrickItem selectedBrick;
    public Brick currentBrick => selectedBrick.prefab;
    private int selectIndex;

    public BlockInventoryUI invUI;

	public void Init()
	{
        ownBricks.Clear();
        selectIndex = 0;
        SelectBrick();
	}

    public void PlaceBrick()
	{
        if (selectedBrick == null) return;

        selectedBrick.count--;
        selectedBrick.slot.countText.text = selectedBrick.count.ToString();
	}

    public void AddBrick(Brick brick)
	{
        BrickItem item = FindBrick(brick);
        if (item == null) return;

        item.count++;
        item.slot.countText.text = item.count.ToString();
    }

    public void AddNewBrick(Brick brick, int count = 1)
	{
        if (FindBrick(brick) != null)
		{
            AddBrick(brick);
		}

        Brick inst = Instantiate(brick);
        inst.transform.position = Vector3.zero;
        inst.gameObject.SetActive(false);
        inst.gameObject.name = inst.name.Replace("(Clone)", "");
        BrickItem item = new BrickItem(inst, count);
        invUI.MakeUI(item);
        int n = ownBricks.Count;
        item.slot?.OnClick(() => SelectBrick(n));
        ownBricks.Add(item);
	}

    public BrickItem FindBrick(Brick brick)
	{
        return ownBricks.Find(x => x.prefab.gameObject.name == brick.gameObject.name);
	}

	#region Select
	public void SelectBrick()
	{
        if (ownBricks.Count == 0) return;

        SelectBrick(0);
	}

	public void SelectBrick(int index)
	{
        if (ownBricks.Count < index) return;

        if (selectedBrick?.prefab?.transMode != null)
            selectedBrick?.prefab?.transMode?.gameObject.SetActive(false);

        selectIndex = index;
        selectedBrick = ownBricks[selectIndex];
        invUI.currentBlock.sprite = selectedBrick?.prefab.image;

        if (selectedBrick.prefab.transMode == null && selectedBrick.prefab.transPrefab != null)
		{
            selectedBrick.prefab.transMode = Instantiate(selectedBrick.prefab.transPrefab);
            selectedBrick.prefab.transMode.SetActive(true);
		}
        else if (selectedBrick.prefab.transMode != null)
		{
            selectedBrick.prefab.transMode.SetActive(true);
		}
	}

    public void SelectNextBrick()
	{
        if (ownBricks.Count == 0) return;

        SelectBrick((selectIndex + 1) % ownBricks.Count);
    }
    #endregion
}
