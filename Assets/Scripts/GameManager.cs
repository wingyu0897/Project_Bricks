using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public bool a;
	public StageSO b;

	public StagesSO stages;
	public int stageIndex = 0;

	private StageUI menustage;
	private StageSO currentStage;

	private float start;

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
		menustage?.SetDate(stages.stages[stageIndex].stageImage, stages.stages[stageIndex].stageName);
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
		SceneManager.LoadScene(2);

		float delay = UnityEngine.Random.Range(1.0f, 4.0f);

		yield return new WaitForSeconds(delay);

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

		start = Time.time;
		currentStage = stage;

		CameraMovement camMove = FindObjectOfType<CameraMovement>();
		camMove.Init();

		camMove.cam.backgroundColor = (currentStage.difficult == 0) ? Color.green : (currentStage.difficult == 1) ? new Color(255, 187, 0, 0) : Color.red;

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
		int clearTime = (int)(Time.time - start);
		ClearPanel cp = FindObjectOfType<ClearPanel>();
		cp.ActivePanel(currentStage.stageImage, currentStage.stageName, clearTime);
	}

	public void ReturnMenu()
	{
		StartCoroutine(LoadSceneAsync(0, () => StartMenu()));
	}
	#endregion
}
