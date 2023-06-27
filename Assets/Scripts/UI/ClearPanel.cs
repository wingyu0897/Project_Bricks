using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    public GameObject panel;
	public Image stageImage;
	public TextMeshProUGUI nameText;
	public Button returnBtn;
	public GameObject pausePanel;

	private void Start()
	{
		returnBtn.onClick.AddListener(GameManager.Instance.ReturnMenu);
	}

	public void ActivePanel(Sprite image, string stageName)
	{
		panel.SetActive(true);
		stageImage.sprite = image;
		nameText.text = $"{stageName} Clear!";
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
