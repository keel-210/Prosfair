using System.Collections.Generic;
using UnityEngine;
public class Chelstaminus : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Chelstaminus;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		return t;
	}
}