using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class FieldOathButton : OathButtonBase
{
	[SerializeField] Dropdown attributeDD, timeDD;
	void Start()
	{
		string[] tempEnums = Enum.GetNames(typeof(BoardAttribute));
		var names = new List<string>(tempEnums);
		attributeDD.AddOptions(names);

		tempEnums = Enum.GetNames(typeof(BoardTime));
		names = new List<string>(tempEnums);
		timeDD.AddOptions(names);
	}
	IPiece piece;
	protected override OathPrepare PrepareEffect()
	{
		BoardAttribute b = (BoardAttribute)Enum.ToObject(typeof(BoardAttribute), attributeDD.value);
		BoardTime t = (BoardTime)Enum.ToObject(typeof(BoardTime), timeDD.value);
		OathPrepare p = new OathPrepare(b, t);
		return p;
	}
}
