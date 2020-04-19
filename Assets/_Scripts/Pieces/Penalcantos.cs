using System.Collections.Generic;
using UnityEngine;
public class Penalcantos : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Penalcantos;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		return t;
	}
}