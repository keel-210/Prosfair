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
	public static List<CompositeOathPrepare> ALLCompositeOathCheck(List<Oath> _4s, List<Oath> _5s, List<Oath> _6s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		prepares.AddRange(_9PiecesCheck(_4s, _5s));
		prepares.AddRange(_11PiecesCheck(_5s, _7s));
		prepares.AddRange(_13PiecesCheck(_6s, _7s));
		prepares.AddRange(_15PiecesCheck(_4s, _5s, _6s));
		prepares.AddRange(_17PiecesCheck(_4s, _5s));
		prepares.AddRange(_21PiecesCheck(_7s));
		prepares.AddRange(_26PiecesCheck(_5s, _7s));
		prepares.AddRange(_30PiecesCheck(_4s, _5s, _6s, _7s));
		return prepares;
	}
	// 		9駒位相 全ての戦域において成立 ただし特殊駒の発生は小戦域に限定される 
	// 4 + 5駒位相(駒重複不可) 11段階目まで
	//   殉教者の発生には堕天使陣形が必要
	// 11駒位相 全ての戦域において成立 
	// 5 + 7駒位相(駒重複不可) 13段階目まで
	// 13駒位相 全ての戦域において成立 ただし特殊駒の発生は小戦域に限定される 
	// 6 + 7駒位相(駒重複不可) 15段階目まで
	//   管理者の発生には傀儡陣形が必要

	// 15駒位相 7×7以上の小戦域においてのみ成立 
	// 4 + 5 + 6駒位相(駒重複不可)
	// 17駒位相 7×7以上の小戦域においてのみ成立
	//  5 + 4 + 4 + 4駒位相(駒重複不可)
	// 審問官の発生には堕天使，背教者陣形が必要
	// 21駒位相 7×7以上の小戦域においてのみ成立 
	// 7 + 7 + 7駒位相(駒重複不可)
	// 越境者の発生には暴君，混沌陣形が必要

	// 26駒位相 13×13の小戦域においてのみ成立
	// 7 + 7 + 7 + 5駒位相(駒重複不可)
	// 狂戦士の発生には堕天使，背教徒，戦塔，混沌陣形が必要
	// 30駒位相 13×13の小戦域においてのみ成立 陣形全部→これ実現できるか後で考える
	static List<CompositeOathPrepare> _9PiecesCheck(List<Oath> _4s, List<Oath> _5s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		foreach (var _4 in _4s)
			foreach (var _5 in _5s)
				if (_4.board == _5.board)
				{
					List<IPiece> l = _4.pieces;
					l.AddRange(_5.pieces);
					if (!OathChecker.DuplicatePieceCheck(l))
					{ }
				}
		return prepares;
	}
	static List<CompositeOathPrepare> _11PiecesCheck(List<Oath> _5s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _13PiecesCheck(List<Oath> _6s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _15PiecesCheck(List<Oath> _4s, List<Oath> _5s, List<Oath> _6s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _17PiecesCheck(List<Oath> _4s, List<Oath> _5s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _21PiecesCheck(List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _26PiecesCheck(List<Oath> _5s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	static List<CompositeOathPrepare> _30PiecesCheck(List<Oath> _4s, List<Oath> _5s, List<Oath> _6s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		return prepares;
	}
	public static bool DuplicatePieceCheck(List<IPiece> l)
	{
		foreach (var dup in l.Where((o, i) => l.Skip(i + 1).Contains(o)))
			return true;
		return false;
	}
}

