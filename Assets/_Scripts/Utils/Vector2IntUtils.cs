using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public static class Vector2IntUtils
{
	public static Vector2Int RegionMin(List<IPiece> pieces)
	{
		var Xs = pieces.Select(X => X.PositionOnBoard.x);
		var Ys = pieces.Select(X => X.PositionOnBoard.y);
		return new Vector2Int(Xs.Min(), Ys.Min());
	}
	public static Vector2Int RegionMax(List<IPiece> pieces)
	{
		var Xs = pieces.Select(X => X.PositionOnBoard.x);
		var Ys = pieces.Select(X => X.PositionOnBoard.y);
		return new Vector2Int(Xs.Max(), Ys.Max());
	}
	public static List<List<Vector2Int>> PossibleRegionPos(int fieldSize, int BoardSize, Vector2Int _min, Vector2Int _max)
	{
		List<List<Vector2Int>> l = new List<List<Vector2Int>>();
		Vector2Int oathSize = _max - _min;
		Vector2Int Space = new Vector2Int(fieldSize, fieldSize) - oathSize;
		for (int i = -Space.x; i <= Space.x; i++)
			for (int j = -Space.y; j <= Space.y; j++)
				if (0 <= _min.x + i && 0 <= _min.y + j && _min.x + i + 4 < BoardSize && _min.y + j + 4 < BoardSize)
					l.Add(new List<Vector2Int> { new Vector2Int(_min.x + i, _min.y + j), new Vector2Int(_min.x + i + 4, _min.y + j + 4) });
		return l;
	}
}