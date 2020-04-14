using System.Collections.Generic;
using UnityEngine;
public static class PieceUtils
{
	public static List<Vector2Int> GetRawMovement(PieceType type)
	{
		List<Vector2Int> l = new List<Vector2Int>();
		switch (type)
		{
			case PieceType.Gore: l = GoreMovement; break;
			case PieceType.Laage: l = LaageMovement; break;
			case PieceType.Shamain: l = ShamainMovement; break;
			case PieceType.Gisharl: l = GisharlMovement; break;
			case PieceType.Woofein: l = WoofeinMovement; break;
			case PieceType.Cain: l = CainMovement; break;
			case PieceType.Ozoa: l = OzoaMovement; break;
			case PieceType.Golcleo: l = GolcleoMovement; break;
			case PieceType.Remagoguu: l = RemagoguuMovement; break;
			case PieceType.Bolussa: l = BolussaMovement; break;
			case PieceType.Mechet: l = MechetMovement; break;
			case PieceType.Backtorce: l = BacktorceMovement; break;
			case PieceType.Maanagis: l = MaanagisMovement; break;
			case PieceType.Inquisice: l = InquisiceMovement; break;
			case PieceType.Chelstaminus: l = ChelstaminusMovement; break;
			case PieceType.Penalcantos: l = PenalcantosMovement; break;
			case PieceType.Recstarionis: l = RecstarionisMovement; break;
		}
		return l;
	}
	public static List<Vector2Int> CheckMovement(Player.BoardAndPos bp)
	{
		PieceBase p = bp.board.GetPieceOnTargetPosition(bp.board.ObjectSpaceToBoardSpace(bp.pos));
		List<Vector2Int> l = GetRawMovement(p.pieceType);
		List<Vector2Int> t = new List<Vector2Int>();
		for (int i = -bp.board.size; i < bp.board.size; i++)
		{
			for (int j = -bp.board.size; j < bp.board.size; j++)
			{

			}
		}
		return t;
	}
	public static bool IsDefeatByTargetPiece()
	{
		return true;
	}
	public static int CheckPieceStrength(PieceType type)
	{
		return 0;
	}
	static List<Vector2Int> GoreMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> LaageMovement = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(0, 3), new Vector2Int(0, 4), new Vector2Int(0, 5), new Vector2Int(0, 6), new Vector2Int(0, 7), new Vector2Int(0, 8), new Vector2Int(0, 9), new Vector2Int(0, 10), new Vector2Int(0, 11), new Vector2Int(0, 12) };
	static List<Vector2Int> ShamainMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> GisharlMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> WoofeinMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> CainMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> OzoaMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> GolcleoMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> RemagoguuMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> BolussaMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> MechetMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> BacktorceMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> MaanagisMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> InquisiceMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> ChelstaminusMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> PenalcantosMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
	static List<Vector2Int> RecstarionisMovement = new List<Vector2Int> { new Vector2Int(0, 1) };
}