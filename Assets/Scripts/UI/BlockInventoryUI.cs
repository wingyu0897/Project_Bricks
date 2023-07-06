using UnityEngine;
using UnityEngine.UI;

public class BlockInventoryUI : MonoBehaviour
{
    public Image currentBlock;
    public BlockSlot slotPrefab;
    public Transform parent;

    public void MakeUI(BrickItem item)
	{
        BlockSlot bs = Instantiate(slotPrefab, parent);
        bs.SetDate(item.prefab.image, item.count);
        item.slot = bs;
	}
}
