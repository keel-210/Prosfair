using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
public class OathButtons : MonoBehaviour
{
	public OathManager manager;
	public GameObject button, subButton;
	public Button SkipButton;
	List<GameObject> Buttons = new List<GameObject>();
	public void LoadOaths(bool IsWhitePlaying)
	{
		Clear();
		List<Oath> oaths = IsWhitePlaying ? manager.WhiteOaths : manager.BlackOaths;
		Debug.Log("Oath Field" + oaths.Count);
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
		RectTransform ButtonsRect = GetComponent<RectTransform>();
		for (int i = 0; i < Buttons.Count; i++)
		{
			RectTransform r = Buttons[i].GetComponent<RectTransform>();
			Buttons[i].transform.position = new Vector3(ButtonsRect.position.x - r.rect.width * 0.5f, ButtonsRect.position.y + ButtonsRect.rect.height - r.rect.height * (i + 0.5f), 0);
		}
	}
	GameObject AddButton(Oath o)
	{
		GameObject obj = Instantiate(button, transform.position, Quaternion.identity, transform);
		obj.GetComponent<OathButtonBase>().oath = o;
		return obj;
	}
	public void Clear()
	{
		Buttons.ForEach(x => Destroy(x));
		Buttons.Clear();
	}
}