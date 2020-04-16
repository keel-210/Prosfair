using System.Collections.Generic;
using UnityEngine;
public class Cain : PieceBase
{
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		RowCheck(t, new Vector2Int(1, 1));
		RowCheck(t, new Vector2Int(1, -1));
		RowCheck(t, new Vector2Int(-1, 1));
		RowCheck(t, new Vector2Int(-1, -1));
		return t;
	}
}