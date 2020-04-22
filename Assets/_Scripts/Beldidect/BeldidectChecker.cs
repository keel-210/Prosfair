using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeldidectChecker : MonoBehaviour
{
	bool IsWhitePlayer;
	public List<IBeldidect> Beldidects = new List<IBeldidect>();
	bool GolcleoCheck(IPiece target, Board board)
	{
		// |堕天使陣形|混沌，逆賊，背教徒
		return true;
	}
	bool CainCheck(IPiece target, Board board)
	{
		// |背教徒陣形|堕天使，淫后，逆賊
		return true;
	}
	bool WoofeinCheck(IPiece target, Board board)
	{
		// |戦塔陣形|暴君，淫后，傀儡
		return true;
	}
	bool OzoaCheck(IPiece target, Board board)
	{
		// |混沌陣形|衝車，背教徒，逆賊
		return true;
	}
	bool RemagoguuCheck(IPiece target, Board board)
	{
		// |傀儡陣形|堕天使，戦塔，背教徒
		return true;
	}
	bool MechetCheck(IPiece target, Board board)
	{
		// |淫后陣形|暴君，傀儡，逆賊
		return true;
	}
	bool BolussaCheck(IPiece target, Board board)
	{
		// |暴君陣形|淫后，傀儡，兵鬼×3 まず兵鬼*3を探す
		if (target.pieceType != PieceType.Gore)
			return false;
		Vector2Int g0p = target.PositionOnBoard + new Vector2Int(1, 0), g1p = target.PositionOnBoard + new Vector2Int(2, 0);
		IPiece g0 = board.pieces[g0p.x, g0p.y], g1 = board.pieces[g1p.x, g1p.y];
		if (g0 == null || g1 == null || g0.pieceType != PieceType.Gore || g1.pieceType != PieceType.Gore)
			return false;
		//兵鬼*3が並んでないパターンは排除
		return true;
	}
	void Check(IPiece checkTarget, Vector2Int TragetPos, Board board, List<IPiece> t)
	{
		if (checkTarget.IsWhitePlayer)
			TragetPos *= -1;
		int X = checkTarget.PositionOnBoard.x + TragetPos.x, Y = checkTarget.PositionOnBoard.y + TragetPos.y;
		var p = CheckPos(checkTarget, X, Y, board);
		if (p != null)
			t.Add(p);
	}
	void RowCheck(IPiece checkTarget, Vector2Int dir, Board board, List<IPiece> t)
	{
		if (!checkTarget.IsWhitePlayer)
			dir *= -1;
		for (int i = 1; i < board.size; i++)
		{
			int X = checkTarget.PositionOnBoard.x + dir.x * i, Y = checkTarget.PositionOnBoard.y + dir.y * i;
			var p = CheckPos(checkTarget, X, Y, board);
			if (p != null)
				t.Add(p);
		}
	}
	IPiece CheckPos(IPiece checkTarget, int X, int Y, Board board)
	{
		if (X < 0 || board.size - 1 < X || Y < 0 || board.size - 1 < Y)
			return null;
		IPiece p = board.pieces[X, Y];
		if (p == null || (checkTarget.IsWhitePlayer != p.IsWhitePlayer))
			return null;
		else
			return p;
	}
}