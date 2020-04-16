using System.Collections.Generic;
using UnityEngine;
public class Gisharl : PieceBase
{
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		Check(t, new Vector2Int(1, 2));
		Check(t, new Vector2Int(-1, 2));
		return t;
	}
}