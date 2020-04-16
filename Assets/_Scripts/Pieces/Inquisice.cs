using System.Collections.Generic;
using UnityEngine;
public class Inquisice : PieceBase
{
	protected override void Initialize()
	{
		pieceType = PieceType.Inquisice;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		if (board.pieces[PositionOnBoard.x, PositionOnBoard.y + 1] == null)
		{
			t.Add(new Vector2Int(0, 1) + PositionOnBoard);
			return t;
		}

		if (board.pieces[PositionOnBoard.x, PositionOnBoard.y + 1].Stage <= Stage)
			t.Add(new Vector2Int(0, 1) + PositionOnBoard);
		return t;
	}
}