using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class FieldOathButton : OathButtonBase
{
	[SerializeField] Dropdown attributeDD, timeDD;
	OathButtons Buttons;
	void Start()
	{
		base.Start();

		string[] tempEnums = Enum.GetNames(typeof(BoardAttribute));
		var names = new List<string>(tempEnums);
		attributeDD.ClearOptions();
		attributeDD.AddOptions(names);

		tempEnums = Enum.GetNames(typeof(BoardTime));
		names = new List<string>(tempEnums);
		timeDD.ClearOptions();
		timeDD.AddOptions(names);

		Buttons = transform.parent.GetComponent<OathButtons>();
	}
	protected override OathUIData PrepareEffect()
	{
		BoardAttribute b = (BoardAttribute)Enum.ToObject(typeof(BoardAttribute), attributeDD.value);
		BoardTime t = (BoardTime)Enum.ToObject(typeof(BoardTime), timeDD.value);
		OathUIData p = new OathUIData(b, t);
		return p;
	}
	public override void OnClick()
	{
		oath.OathEffect(PrepareEffect());
		line.enabled = false;
		button.onClick.RemoveListener(OnClick);
		Buttons.FieldButtonAllClear();
		Destroy(this.gameObject);
	}
}
