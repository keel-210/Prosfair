using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class DrawerUI : MonoBehaviour
{
	[SerializeField] Vector3 AppearancePos, HidePos;
	Rect rect;
	public bool IsAppearance;
	void Start()
	{
		IsAppearance = false;
		rect = GetComponent<RectTransform>().rect;
	}
	public void AppearanceRect()
	{
		if (IsAppearance)
			return;
		IsAppearance = true;
		GetComponent<RectTransform>().DOAnchorPos(AppearancePos, 0.1f);
	}
	public void DisappearanceRect()
	{
		if (!IsAppearance)
			return;
		IsAppearance = false;
		GetComponent<RectTransform>().DOAnchorPos(HidePos, 0.1f);
	}
}