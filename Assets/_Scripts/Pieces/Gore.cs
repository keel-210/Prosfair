using System.Collections.Generic;
using UnityEngine;
public class Gore : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Gore;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		Check(t, new Vector2Int(0, 1));
		return t;
	}
}