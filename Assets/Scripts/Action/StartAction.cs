using UnityEngine;
using DG.Tweening;

public class StartAction : MonoBehaviour
{
	public RectTransform invenTrm;

    public void OnStart()
	{
		invenTrm.DOMoveY(0, 3f).SetEase(Ease.OutExpo);
	}
}
