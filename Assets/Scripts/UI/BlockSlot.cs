using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlockSlot : MonoBehaviour
{
    public Image blockImage;
    public TextMeshProUGUI countText;
    public Button btn;

    public void SetDate(Sprite image, int count)
	{
        blockImage.sprite = image;
        countText.text = count.ToString();
	}

    public void OnClick(UnityAction onClick)
	{
        btn.onClick.AddListener(onClick);
	}
}
