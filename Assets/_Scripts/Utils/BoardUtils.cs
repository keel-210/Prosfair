using UnityEngine;
using System.Collections.Generic;

public static class BoardUtils
{
	public static List<IPiece> GetPiecesInRegion(Board board, Vector2Int minRegion, Vector2Int maxRegion)
	{
		List<IPiece> list = new List<IPiece>();
		foreach (IPiece p in board.pieces)
			if (p != null)
				if (Vector2IntUtils.IsWhithinRangePos(p.PositionOnBoard, minRegion, maxRegion))
					list.Add(p);
		return list;
	}
}