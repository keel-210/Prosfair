using System.Collections.Generic;
using UnityEngine;
public class Backtorce : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Backtorce;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		return t;
	}
}