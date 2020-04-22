using System.Collections.Generic;
using UnityEngine;
public class Shamain : PieceBase
{
	bool IsFirstMoved = false;
	protected override void Initialize()
	{
		pieceType = PieceType.Shamain;
	}
	public override List<Vector2Int> CheckMovement()
	{
		List<Vector2Int> t = new List<Vector2Int>();
		ShamainCheck(t);
		return t;
	}
	void ShamainCheck(List<Vector2Int> temp)
	{
		List<Vector2Int> ShamainMovement = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 1) };
		for (int i = 0; i < ShamainMovement.Count; i++)
			if (!IsWhitePlayer)
				ShamainMovement[i] = new Vector2Int(-1 * ShamainMovement[i].x, -1 * ShamainMovement[i].y);

		Vector2Int Forward = PositionOnBoard + ShamainMovement[0];
		Vector2Int RightForward = PositionOnBoard + ShamainMovement[1];
		Vector2Int LeftForward = PositionOnBoard + ShamainMovement[2];

		if (BoardSanityCheck(Forward) || BoardSanityCheck(RightForward) || BoardSanityCheck(LeftForward))
			return;

		IPiece f = board.pieces[Forward.x, Forward.y], r = board.pieces[RightForward.x, RightForward.y], l = board.pieces[LeftForward.x, LeftForward.y];
		if (f == null)
		{
			temp.Add(Forward);
			// if (!IsFirstMoved)
			// {
			// 	temp.Add(Forward + Forward + PositionOnBoard);
			// 	IsFirstMoved = true;
			// }
		}
		if (r != null && r.Stage <= Stage && (IsWhitePlayer ^ r.IsWhitePlayer))
			temp.Add(RightForward);
		if (l != null && l.Stage <= Stage && (IsWhitePlayer ^ l.IsWhitePlayer))
			temp.Add(LeftForward);
	}
	bool BoardSanityCheck(Vector2Int v)
	{
		return v.x < 0 || board.size - 1 < v.x || v.y < 0 || board.size - 1 < v.y;
	}
}