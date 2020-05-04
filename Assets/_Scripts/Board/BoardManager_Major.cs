using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager_Major : BoardManager
{
	protected override void Start()
	{
		Debug.Log("Init Board");
		SetInitPiece(true);
		SetInitPiece(false);
	}
}
