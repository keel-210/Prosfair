using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class EnhanceOathButton : OathButtonBase
{
	public Dropdown dropdown;
	List<PieceType> enumNames = new List<PieceType>();
	List<string> names;
	void Start()
	{
		base.Start();

		foreach (IPiece p in oath.pieces)
			if (!enumNames.Contains(p.pieceType))
				enumNames.Add(p.pieceType);
		string[] tempEnums = enumNames.Select(x => Enum.GetName(typeof(PieceType), x)).ToArray();
		names = new List<string>(tempEnums);

		dropdown.ClearOptions();
		dropdown.AddOptions(names);
	}
	protected override OathUIData PrepareEffect()
	{
		PieceType type = (PieceType)Enum.Parse(typeof(PieceType), names[dropdown.value], true);
		OathUIData p = new OathUIData(oath.pieces.Where(x => x.pieceType == type).First());
		return p;
	}
}
