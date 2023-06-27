using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public Button nextBtn;
    public Button prevBtn;
    public Button playBtn;
	public TextMeshProUGUI stageName;
	public Image stageImage;

	private void Start()
	{
		nextBtn.onClick.AddListener(NextBtn);
		prevBtn.onClick.AddListener(PrevBtn);
		playBtn.onClick.AddListener(GameManager.Instance.StartGame);
	}

	public void NextBtn()
	{
		GameManager.Instance.NextStageIndex(1);
	}
	public void PrevBtn()
	{
		GameManager.Instance.NextStageIndex(-1);
	}

	public void SetDate(Sprite image, string text)
	{
		stageName.text = text;
		stageImage.sprite = image;
	}

	public void Quit()
	{
		Application.Quit();
	}
}
