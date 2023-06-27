using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public bool a;
	public StageSO b;

	public StagesSO stages;
	public int stageIndex = 0;

	private StageUI menustage;
	private StageSO currentStage;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		if (a)
		{
			StartStage(b);
		}
		else
		{
			StartMenu();
		}

		NextStageIndex(stageIndex);
	}

	#region InMenu
	public void NextStageIndex(int i)
	{
		int n = (stageIndex + i) % stages.stages.Count;
		if (n < 0)
		{
			n = stages.stages.Count - 1;
		}
		SetStageIndex(n);
	}

	public void SetStageIndex(int i)
	{
		if (stages.stages.Count < i + 1)
			return;
		stageIndex = i;
		menustage.SetDate(stages.stages[stageIndex].stageImage, stages.stages[stageIndex].stageName);
	}

	public void StartGame()
	{
		StartCoroutine(LoadSceneAsync(1, () => StartStage(stages.stages[stageIndex])));
	}

	public void StartMenu()
	{
		menustage = FindObjectOfType<StageUI>();
		SetStageIndex(stageIndex);
	}
	#endregion

	private IEnumerator LoadSceneAsync(int n, Action callBack)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync(n);

		while (!ao.isDone)
		{
			yield return null;
		}

		callBack?.Invoke();
	}

	#region InStage
	public void StartStage(StageSO stage = null)
	{
		if (stage == null)
			stage = stages.stages[stageIndex];

		currentStage = stage;

		CameraMovement camMove = FindObjectOfType<CameraMovement>();
		camMove.Init();

		BrickInventory inv = FindObjectOfType<BrickInventory>();
		inv.Init();
		foreach (BrickItem brk in stage.brickItems)
		{
			inv.AddNewBrick(brk.prefab, brk.count);
		}
		inv.SelectBrick();

		Shape shape = FindObjectOfType<Shape>();
		shape.Init(stage.shape);
	}

	public void ClearGame()
	{
		ClearPanel cp = FindObjectOfType<ClearPanel>();
		cp.ActivePanel(currentStage.stageImage, currentStage.stageName);
	}

	public void ReturnMenu()
	{
		StartCoroutine(LoadSceneAsync(0, () => StartMenu()));
	}
	#endregion
}
