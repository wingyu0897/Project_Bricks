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

	public GameObject stageUI;

	private Camera cam;

	private void Start()
	{
		cam = Camera.main;
		nextBtn.onClick.AddListener(NextBtn);
		prevBtn.onClick.AddListener(PrevBtn);
		playBtn.onClick.AddListener(GameManager.Instance.StartGame);
	}

	public void ShowStage(bool show)
	{
		StopAllCoroutines();
		if (show)
		{
			print(stageUI.transform.position);
			StartCoroutine(MoveStage(stageUI.transform.position, new Vector3(cam.pixelWidth * 0.5f, cam.pixelHeight * 0.5f)));
		}
		else
		{
			StartCoroutine(MoveStage(stageUI.transform.position, new Vector3(cam.pixelWidth * 0.5f, cam.pixelHeight + 600)));
		}
	}

	private IEnumerator MoveStage(Vector3 origin, Vector3 target)
	{
		float time = 0.9f;
		float cur = 0;
		float progress = 0;
		float x = cam.pixelWidth * 0.5f;
		while (progress < 1f)
		{
			cur += Time.deltaTime;
			progress = cur / time;

			stageUI.transform.position = origin + (target - origin) * EaseOutBack(progress);

			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

	private float EaseOutBack(float x)
	{
		return 1 + 2.70158f * Mathf.Pow(x - 1, 3) + 1.70158f * Mathf.Pow(x - 1, 2);
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
