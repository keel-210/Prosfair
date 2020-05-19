using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class EnhanceOathButton : OathButtonBase
{
	void Start()
	{
		base.Start();
	}
	protected override OathUIData PrepareEffect()
	{
		OathUIData p = new OathUIData(oath.pieces.OrderBy(x => x.Stage).First());
		return p;
	}
}
