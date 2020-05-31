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
		Vector2Int OathIncludingSize = maxPieces - minPieces;
		if (OathIncludingSize.x > fieldSize || OathIncludingSize.y > fieldSize)
			return null;
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
		_4sCache = _4s;
		_5sCache = _5s;
		_6sCache = _6s;
		_7sCache = _7s;
		return _nPiecesCheck(_4s, _5s, _6s, _7s);
	}
	static List<Oath> _4sCache, _5sCache, _6sCache, _7sCache;
	static List<List<int>> CompositeCombinationCounts = new List<List<int>>
	{
		 new List<int> { 4, 5 },
		 new List<int> { 5,7 },
		 new List<int> { 6, 7 },
		 new List<int> { 4, 5,6 },
		 new List<int> { 4, 4,4,5 },
		 new List<int> { 7,7,7 },
		 new List<int> { 7,7,7,5 },
		 new List<int> { 4, 5,7,7,7 }
		 };
	static List<CompositeOathPrepare> _nPiecesCheck(List<Oath> _4s, List<Oath> _5s, List<Oath> _6s, List<Oath> _7s)
	{
		List<CompositeOathPrepare> prepares = new List<CompositeOathPrepare>();
		//組み合わせでボードが同じものを探す再帰
		List<List<Oath>> Combinations = new List<List<Oath>>();
		foreach (var c in CompositeCombinationCounts)
			foreach (var l in CombinationChecker(c))
				Combinations.Add(l);
		Combinations.ForEach(x =>
		{
			List<IPiece> l = new List<IPiece>();
			foreach (Oath o in x)
				l.AddRange(o.pieces);
			if (!OathChecker.DuplicatePieceCheck(l))
				prepares.Add(new CompositeOathPrepare(x[0].board, l));
		});
		return prepares;
	}
	static List<List<Oath>> CombinationChecker(List<int> c)
	{
		List<List<Oath>> l = new List<List<Oath>>();
		for (int i = 1; i < c.Count; i++)
		{
			if (i == 1)
				l = CompositeCombination(_nPieceOaths(c[0]).Select(x => new List<Oath> { x }).ToList(), _nPieceOaths(c[1]));
			else
				l = CompositeCombination(l, _nPieceOaths(c[i]));
		}
		return l;
	}
	static List<List<Oath>> CompositeCombination(List<List<Oath>> a, List<Oath> b)
	{
		List<List<Oath>> l = new List<List<Oath>>();
		foreach (var _a in a)
			foreach (var _b in b)
			{
				if (_a[0].board == _b.board)
				{
					List<Oath> _l = new List<Oath>(_a);
					_l.Add(_b);
					l.Add(_l);
				}
			}
		return l;
	}
	static List<Oath> _nPieceOaths(int n)
	{
		switch (n)
		{
			case 4: return _4sCache;
			case 5: return _5sCache;
			case 6: return _6sCache;
			case 7: return _7sCache;
			default: return null;
		}
	}
	public static bool DuplicatePieceCheck(List<IPiece> l)
	{
		foreach (var dup in l.Where((o, i) => l.Skip(i + 1).Contains(o)))
			return true;
		return false;
	}
}

