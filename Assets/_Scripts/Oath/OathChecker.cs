using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class OathChecker
{
	//managerと共依存なので改善する
	//staticにできるところだけ分けて残りはmanagerに突っ込むべきでは？
	public static List<List<Vector2Int>> RelativeCoordinates;
	static OathChecker()
	{
		RelativeCoordinates = new List<List<Vector2Int>>();
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates3);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates4);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates5);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates6);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates7);
	}

	public static Oath OathTypeInstantiate(BoardManager boards, Board board, List<IPiece> pieces, bool IsWhite)
	{
		var check = FieldOathCheck(board, pieces, IsWhite);
		if (check != null && check.FieldSize < board.size)
		{
			Debug.Log((check != null).ToString() + " " + check.WhitePieceCount + " " + check.BlackPieceCount);
			var f = new FieldOath(OathType.Field, boards, board, pieces, IsWhite);
			f.Initialize(check);
			return f;
		}
		if (pieces.Count >= 9)
			if (board.OccupiedPlayer != BoardOccupation.NonOccupied)
				return new FieldAbandonmentOath(OathType.TypeEnhance, boards, board, pieces, IsWhite);
			else
				return new TypeEnhanceOath(OathType.TypeEnhance, boards, board, pieces, IsWhite);
		else
			return new EnhanceOath(OathType.Enhance, boards, board, pieces, IsWhite);
	}
	public static FieldCheck FieldOathCheck(Board board, List<IPiece> pieces, bool IsWhite)
	{
		//位相の範囲内に相手駒が2つ以上ある->true
		//完成位相を含む範囲が最も小さく最も多くの相手の駒を含む領域が最適
		//5*5(3rd Oath~),7*7(7th Oath~),13*13(17th Oath~)の範囲を調査
		int fieldSize = OathUtils.FieldSize(pieces.Count);
		if (fieldSize == 13)
			return FullsizeField(board, fieldSize);

		Vector2Int minPieces = Vector2IntUtils.RegionMin(pieces);
		Vector2Int maxPieces = Vector2IntUtils.RegionMax(pieces);
		List<List<Vector2Int>> checkPoses = Vector2IntUtils.PossibleRegionPos(fieldSize, board.size, minPieces, maxPieces);

		FieldCheck f = RegionCheck(board, checkPoses, IsWhite, fieldSize);
		if (f.AllPieces.Count == 0)
			return null;
		else
			return f;
	}
	//指定領域の調査
	//条件に合致するなら範囲内の全ての駒を含んだFieldCheckを返す
	//条件に合致しないなら例外値として何も含まないFieldCheckを返す
	public static FieldCheck RegionCheck(Board board, List<List<Vector2Int>> checkPoses, bool IsWhite, int fieldSize)
	{
		FieldCheck check = new FieldCheck(new List<IPiece>(), 0, Vector2Int.zero);
		foreach (List<Vector2Int> v in checkPoses)
		{
			List<IPiece> piecesInOathRegion = new List<IPiece>();
			Vector2Int minRegion = v[0], maxRegion = v[1];
			foreach (IPiece p in board.pieces)
				if (p != null)
					if (InRegionPiece(p, minRegion, maxRegion))
						piecesInOathRegion.Add(p);
			int OldopponentPieceCount = (IsWhite ? check.BlackPieceCount : check.WhitePieceCount);
			int opponentPieceCount = piecesInOathRegion.Where(x => x.IsWhitePlayer == !IsWhite).Count();
			if (opponentPieceCount > 1 && opponentPieceCount > OldopponentPieceCount)
				check = new FieldCheck(piecesInOathRegion, fieldSize, minRegion);
		}
		return check;
	}
	static bool InRegionPiece(IPiece piece, Vector2Int _min, Vector2Int _max)
	{
		return _min.x <= piece.PositionOnBoard.x && _min.y <= piece.PositionOnBoard.y && piece.PositionOnBoard.x <= _max.x && piece.PositionOnBoard.y <= _max.y;
	}
	static FieldCheck FullsizeField(Board board, int fieldSize)
	{
		List<IPiece> AllPieces = new List<IPiece>();
		foreach (IPiece p in board.pieces)
			if (p != null)
				AllPieces.Add(p);
		return new FieldCheck(AllPieces, fieldSize, Vector2Int.zero);
	}
}
public class FieldCheck
{
	public List<IPiece> AllPieces { get; set; } = new List<IPiece>();
	public int FieldSize { get; set; }
	public Vector2Int FieldPos { get; set; }
	public int WhitePieceCount { get; private set; }
	public int BlackPieceCount { get; private set; }
	public FieldCheck(List<IPiece> p, int size, Vector2Int fieldPos)
	{
		AllPieces = p;
		FieldSize = size;
		FieldPos = fieldPos;
		WhitePieceCount = AllPieces.Where(x => x.IsWhitePlayer == true).Count();
		BlackPieceCount = AllPieces.Count() - WhitePieceCount;
	}
}