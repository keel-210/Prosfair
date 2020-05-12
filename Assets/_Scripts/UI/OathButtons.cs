using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
public class OathButtons : MonoBehaviour
{
	public OathManager manager;
	public GameObject button, subButton;
	public GameObject EnhanceButton, FieldButton, TypeEnhanceButton, FieldAbandonmentButton;
	public Button SkipButton;
	List<GameObject> Buttons = new List<GameObject>();
	void Start()
	{
		SkipButton.onClick.AddListener(manager.ResetCheckStatus);
		SkipButton.onClick.AddListener(manager.phaseManager.WhitePlayer.OathSkip);
		SkipButton.onClick.AddListener(manager.phaseManager.BlackPlayer.OathSkip);
	}
	void Update()
	{
		int ButtonCountCashe = Buttons.Count;
		Buttons = Buttons.Where(x => x != null).ToList();
		SetButtonPos();
		if (ButtonCountCashe != 0 && Buttons.Count == 0)
			SkipButton.onClick.Invoke();
	}
	public void LoadOaths(bool IsWhitePlaying)
	{
		Clear();
		List<Oath> oaths = IsWhitePlaying ? manager.WhiteOaths : manager.BlackOaths;
		if (oaths.Count == 0)
			SkipButton.onClick.Invoke();
		foreach (Oath o in oaths)
		{
			Buttons.Add(AddButton(o));
		}
		SetButtonPos();
	}
	void SetButtonPos()
	{
		Rect ButtonsRect = GetComponent<RectTransform>().rect;
		for (int i = 0; i < Buttons.Count; i++)
		{
			Rect r = Buttons[i].GetComponent<RectTransform>().rect;
			Vector3 p = new Vector3(r.width * 0.5f, -r.height * 2 * (i + 0.5f), 0);
			Buttons[i].transform.localPosition = p;
		}
	}
	GameObject AddButton(Oath o)
	{
		GameObject obj = default;
		switch (o.type)
		{
			case OathType.Enhance: obj = EnhanceButton; break;
			case OathType.Field: obj = FieldButton; break;
			case OathType.TypeEnhance: obj = EnhanceButton; break;
			case OathType.FieldAbandonment: obj = EnhanceButton; break;
		}
		obj = Instantiate(obj, transform.position, Quaternion.identity, transform);
		obj.GetComponent<OathButtonBase>().oath = o;
		return obj;
	}
	public void Clear()
	{
		Buttons.ForEach(x => Destroy(x));
		Buttons.Clear();
	}
}