using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class EnhanceOathButton : OathButtonBase
{
	Dropdown dropdown;
	List<PieceType> enumNames;
	List<string> names;
	void Start()
	{
		foreach (IPiece p in oath.pieces)
			if (!enumNames.Contains(p.pieceType))
				enumNames.Add(p.pieceType);

		string[] tempEnums = enumNames.Select(x => Enum.GetName(typeof(PieceType), x)).ToArray();
		names = new List<string>(tempEnums);
		dropdown.AddOptions(names);
	}
	IPiece piece;
	protected override OathPrepare PrepareEffect()
	{
		PieceType type = (PieceType)Enum.Parse(typeof(PieceType), names[dropdown.value], true);
		OathPrepare p = new OathPrepare(oath.pieces.Where(x => x.pieceType == type).First());
		return p;
	}
}
