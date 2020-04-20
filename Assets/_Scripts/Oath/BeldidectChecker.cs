using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeldidectChecker : MonoBehaviour
{
	bool IsWhitePlayer;
	public List<IBeldidect> Beldidects = new List<IBeldidect>();
	// 堕天使
	// 背教徒
	// 戦塔
	// 混沌
	// 傀儡
	// 淫后
	// 暴君
	List<IPiece> GolcleoCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		RowCheck(target, new Vector2Int(0, 1), board, t);
		RowCheck(target, new Vector2Int(1, 0), board, t);
		RowCheck(target, new Vector2Int(0, -1), board, t);
		RowCheck(target, new Vector2Int(-1, 0), board, t);
		RowCheck(target, new Vector2Int(1, 1), board, t);
		RowCheck(target, new Vector2Int(1, -1), board, t);
		RowCheck(target, new Vector2Int(-1, 1), board, t);
		RowCheck(target, new Vector2Int(-1, -1), board, t);
		return t;
	}
	List<IPiece> CainCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		RowCheck(target, new Vector2Int(1, 1), board, t);
		RowCheck(target, new Vector2Int(1, -1), board, t);
		RowCheck(target, new Vector2Int(-1, 1), board, t);
		RowCheck(target, new Vector2Int(-1, -1), board, t);
		return t;
	}
	List<IPiece> WoofeinCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		RowCheck(target, new Vector2Int(0, 1), board, t);
		RowCheck(target, new Vector2Int(1, 0), board, t);
		RowCheck(target, new Vector2Int(0, -1), board, t);
		RowCheck(target, new Vector2Int(-1, 0), board, t);
		return t;
	}
	List<IPiece> OzoaCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		Check(target, new Vector2Int(1, 2), board, t);
		Check(target, new Vector2Int(-1, 2), board, t);
		Check(target, new Vector2Int(2, 1), board, t);
		Check(target, new Vector2Int(-2, 1), board, t);
		Check(target, new Vector2Int(1, -2), board, t);
		Check(target, new Vector2Int(-1, -2), board, t);
		Check(target, new Vector2Int(2, 1), board, t);
		Check(target, new Vector2Int(-2, 1), board, t);
		return t;
	}
	List<IPiece> RemagoguuCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		Check(target, new Vector2Int(0, 1), board, t);
		Check(target, new Vector2Int(1, 1), board, t);
		Check(target, new Vector2Int(-1, 1), board, t);
		Check(target, new Vector2Int(1, -1), board, t);
		Check(target, new Vector2Int(-1, -1), board, t);
		return t;
	}
	List<IPiece> MechetCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		Check(target, new Vector2Int(0, 1), board, t);
		Check(target, new Vector2Int(0, -1), board, t);
		Check(target, new Vector2Int(1, 1), board, t);
		Check(target, new Vector2Int(-1, 1), board, t);
		Check(target, new Vector2Int(1, 0), board, t);
		Check(target, new Vector2Int(-1, 0), board, t);
		return t;
	}
	List<IPiece> BolussaCheck(IPiece target, Board board)
	{
		List<IPiece> t = new List<IPiece>();
		Check(target, new Vector2Int(0, 1), board, t);
		Check(target, new Vector2Int(0, -1), board, t);
		Check(target, new Vector2Int(1, 0), board, t);
		Check(target, new Vector2Int(-1, 0), board, t);
		Check(target, new Vector2Int(1, 1), board, t);
		Check(target, new Vector2Int(1, -1), board, t);
		Check(target, new Vector2Int(-1, 1), board, t);
		Check(target, new Vector2Int(-1, -1), board, t);
		return t;
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