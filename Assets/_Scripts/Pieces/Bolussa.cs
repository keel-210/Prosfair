using System.Collections.Generic;
using UnityEngine;
public class Bolussa : PieceBase
{
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		Check(t, new Vector2Int(0, 1));
		Check(t, new Vector2Int(0, -1));
		Check(t, new Vector2Int(1, 0));
		Check(t, new Vector2Int(-1, 0));
		Check(t, new Vector2Int(1, 1));
		Check(t, new Vector2Int(1, -1));
		Check(t, new Vector2Int(-1, 1));
		Check(t, new Vector2Int(-1, -1));
		return t;
	}
}