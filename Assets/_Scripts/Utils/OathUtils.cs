using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public static class OathUtils
{
	public static List<Vector2Int> RelativeCoordinates3 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	public static List<Vector2Int> RelativeCoordinates4 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, -1) };
	public static List<Vector2Int> RelativeCoordinates5 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };
	public static List<Vector2Int> RelativeCoordinates6 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, -1) };
	public static List<Vector2Int> RelativeCoordinates7 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };


	public static bool IsInitialPlacementException(List<IPiece> pieces)
	{
		var types = pieces.Select(x => x.pieceType);
		switch (pieces.Count)
		{
			case 3:
				if ((types.Contains(PieceType.Gore) && types.Contains(PieceType.Golcleo) && types.Contains(PieceType.Shamain))
				|| (types.Contains(PieceType.Gore) && types.Where(x => x == PieceType.Shamain).Count() == 2)
				|| (types.Contains(PieceType.Golcleo) && types.Contains(PieceType.Ozoa) && types.Contains(PieceType.Gisharl))
				|| (types.Contains(PieceType.Shamain) && types.Contains(PieceType.Gisharl) && types.Contains(PieceType.Bolussa))
				)
					return true;
				else
					break;
			case 5:
				if ((types.Where(x => x == PieceType.Gore).Count() == 2 && types.Contains(PieceType.Golcleo) && types.Contains(PieceType.Ozoa) && types.Contains(PieceType.Gisharl))
				|| (types.Where(x => x == PieceType.Gore).Count() == 2 && types.Contains(PieceType.Shamain) && types.Contains(PieceType.Gisharl) && types.Contains(PieceType.Bolussa))
				)
					return true;
				else
					break;
			case 7:
				if ((types.Where(x => x == PieceType.Gore).Count() == 3 && types.Contains(PieceType.Golcleo) && types.Contains(PieceType.Ozoa) && types.Contains(PieceType.Gisharl) && types.Contains(PieceType.Cain))
					|| (types.Where(x => x == PieceType.Gore).Count() == 3 && types.Contains(PieceType.Shamain) && types.Contains(PieceType.Gisharl) && types.Contains(PieceType.Bolussa) && types.Contains(PieceType.Mechet))
					|| (types.Where(x => x == PieceType.Gore).Count() == 3 && types.Contains(PieceType.Shamain) && types.Contains(PieceType.Gisharl) && types.Contains(PieceType.Bolussa) && types.Contains(PieceType.Remagoguu))
					)
					return true;
				else
					break;
			default: break;
		}
		return false;
	}
	public static List<IPiece> PiecesPlacementCheck(List<Vector2Int> relativePos, IPiece target, Board b)
	{
		List<IPiece> p = new List<IPiece>();
		foreach (Vector2Int r in relativePos)
		{
			Vector2Int t = target.PositionOnBoard + r * (target.IsWhitePlayer ? 1 : -1);
			if (t.x < 0 || b.size <= t.x || t.y < 0 || b.size <= t.y || b.pieces[t.x, t.y] == null)
				return p;
			else if (b.pieces[t.x, t.y].IsWhitePlayer == target.IsWhitePlayer)
				p.Add(b.pieces[t.x, t.y]);
		}
		return p;
	}
	public static List<PieceType> TargetPieceTypebyBoardTime(BoardTime time)
	{
		List<PieceType> types = new List<PieceType>();
		switch (time)
		{
			case BoardTime.Pasusu:
				types.Add(PieceType.Woofein);
				types.Add(PieceType.Cain);
				types.Add(PieceType.Golcleo);
				types.Add(PieceType.Ozoa);
				break;
			case BoardTime.Claint:
				types.Add(PieceType.Gore);
				types.Add(PieceType.Gisharl);
				types.Add(PieceType.Laage);
				types.Add(PieceType.Shamain);
				break;
			case BoardTime.Ftuule:
				types.Add(PieceType.Bolussa);
				types.Add(PieceType.Mechet);
				types.Add(PieceType.Remagoguu);
				break;
		}
		return types;
	}
	public static List<PieceType> InversionTargetPieceTypebyBoardTime(BoardTime time)
	{
		List<PieceType> types = System.Enum.GetValues(typeof(PieceType)).Cast<PieceType>().ToList();
		switch (time)
		{
			case BoardTime.Pasusu:
				types = types.Where(x => x != PieceType.Woofein && x != PieceType.Cain && x != PieceType.Golcleo && x != PieceType.Ozoa).ToList();
				break;
			case BoardTime.Claint:
				types = types.Where(x => x != PieceType.Gore && x != PieceType.Gisharl && x != PieceType.Laage && x != PieceType.Shamain).ToList();
				break;
			case BoardTime.Ftuule:
				types = types.Where(x => x != PieceType.Bolussa && x != PieceType.Mechet && x != PieceType.Remagoguu).ToList();
				break;
		}
		return types;
	}
	public static PieceAttribute AttributebyBoardAttribute(BoardAttribute attr)
	{
		switch (attr)
		{
			case BoardAttribute.Vettoria:
				return PieceAttribute.Barlum;
			case BoardAttribute.Ignoria:
				return PieceAttribute.Decsusu;
			case BoardAttribute.Donamia:
				return PieceAttribute.Linnes;
		}
		return PieceAttribute.Barlum;
	}
	public static int FieldSize(int OathPieceCount)
	{
		return OathPieceCount >= 17 ? 13 : (OathPieceCount >= 7 ? 7 : 5);
	}
}