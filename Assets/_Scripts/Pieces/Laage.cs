using System.Collections.Generic;
using UnityEngine;
public class Laage : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Laage;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		RowCheck(t, new Vector2Int(0, 1));
		return t;
	}
}