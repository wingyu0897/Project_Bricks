using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearAction : MonoBehaviour
{
    public ClearPanel cp;
    public GameObject invenPanel;
    public GameObject ground;
    public Transform cameraRig;

    public void OnClear()
	{
        cameraRig.GetComponent<CameraMovement>().enabled = false;
        invenPanel.transform.DOMoveY(-300, 2f);
        Camera.main.DOOrthoSize(4f, 2f);
        cameraRig.DOMoveY(1.5f, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
		{
            ground.SetActive(false);
            cameraRig.DORotate(new Vector3(0, 720, 0), 5f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic)
                .OnComplete(GameManager.Instance.ClearGame);
		});
	}
}
