using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearPanel : MonoBehaviour
{
	public Image panelImage;
    public GameObject panel;
	public Image stageImage;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI clearTimeText;
	public Button returnBtn;
	public GameObject pausePanel;

	private void Start()
	{
		returnBtn.onClick.AddListener(GameManager.Instance.ReturnMenu);
	}

	public void ActivePanel(Sprite image, string stageName, int clearTime)
	{
		//panel.SetActive(true);
		panelImage.DOFade(0.5f, 3f);
		panel.transform.DOMoveY(Camera.main.pixelHeight * 0.5f, 3f).SetEase(Ease.OutExpo);
		stageImage.sprite = image;
		nameText.text = $"{stageName} Clear!";
		int min = clearTime / 60;
		int sec = clearTime % 60;
		clearTimeText.text = $"{string.Format("{0:00}", min)}:{string.Format("{0:00}", sec)}";
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pausePanel.SetActive(!pausePanel.activeSelf);
		}
	}

	public void ReturnMenu()
	{
		GameManager.Instance.ReturnMenu();
	}
}
