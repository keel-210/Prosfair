using System.Collections.Generic;
using UnityEngine;
public class Mechet : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Mechet;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		Check(t, new Vector2Int(0, 1));
		Check(t, new Vector2Int(0, -1));
		Check(t, new Vector2Int(1, 1));
		Check(t, new Vector2Int(-1, 1));
		Check(t, new Vector2Int(1, 0));
		Check(t, new Vector2Int(-1, 0));
		return t;
	}
}