using System.Collections.Generic;
using UnityEngine;
public class Ozoa : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Ozoa;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		Check(t, new Vector2Int(1, 2));
		Check(t, new Vector2Int(-1, 2));
		Check(t, new Vector2Int(2, 1));
		Check(t, new Vector2Int(-2, 1));
		Check(t, new Vector2Int(1, -2));
		Check(t, new Vector2Int(-1, -2));
		Check(t, new Vector2Int(2, 1));
		Check(t, new Vector2Int(-2, 1));
		return t;
	}
}