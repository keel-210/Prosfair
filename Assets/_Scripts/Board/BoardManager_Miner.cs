using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager_Miner : BoardManager
{
	protected override void Start()
	{
		Debug.Log("Init Board");
		mainBoard = new Board();
		SetInitPiece(true);
		SetInitPiece(false);
	}
}
