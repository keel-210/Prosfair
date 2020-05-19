using System.Collections.Generic;
using UnityEngine;
public static class PieceUtils
{
	public static int PieceInitialStage(PieceType type)
	{
		switch (type)
		{
			case PieceType.Gore: return 1;
			case PieceType.Laage: return 1;
			case PieceType.Shamain: return 1;
			case PieceType.Gisharl: return 1;
			case PieceType.Woofein: return 2;
			case PieceType.Cain: return 2;
			case PieceType.Ozoa: return 2;
			case PieceType.Golcleo: return 3;
			case PieceType.Remagoguu: return 3;
			case PieceType.Bolussa: return 7;
			case PieceType.Mechet: return 7;
			case PieceType.Backtorce: return 9;
			case PieceType.Maanagis: return 13;
			case PieceType.Inquisice: return 17;
			case PieceType.Chelstaminus: return 21;
			case PieceType.Penalcantos: return 26;
			case PieceType.Recstarionis: return 30;
			default: break;
		}
		return 0;
	}
	public static string PieceSimpleNotation(PieceType type)
	{
		switch (type)
		{
			case PieceType.Gore: return "G";
			case PieceType.Laage: return "L";
			case PieceType.Shamain: return "S";
			case PieceType.Gisharl: return "I";
			case PieceType.Woofein: return "W";
			case PieceType.Cain: return "C";
			case PieceType.Ozoa: return "O";
			case PieceType.Golcleo: return "Q";
			case PieceType.Remagoguu: return "R";
			case PieceType.Bolussa: return "B";
			case PieceType.Mechet: return "M";
			case PieceType.Backtorce: return "SB";
			case PieceType.Maanagis: return "SM";
			case PieceType.Inquisice: return "SI";
			case PieceType.Chelstaminus: return "SC";
			case PieceType.Penalcantos: return "SP";
			case PieceType.Recstarionis: return "SR";
			default: break;
		}
		return "0";
	}
	public static PieceType SpecialTypeFromOathCount(int OathCount)
	{
		switch (OathCount)
		{
			case 9: return PieceType.Backtorce;
			case 13: return PieceType.Maanagis;
			case 17: return PieceType.Inquisice;
			case 21: return PieceType.Chelstaminus;
			case 26: return PieceType.Penalcantos;
			case 30: return PieceType.Recstarionis;
			default: return PieceType.Gore;
		}
	}
	public static bool IsDefeatByTargetPiece()
	{
		return true;
	}
	public static int CheckPieceStrength(PieceType type)
	{
		return 0;
	}
	public static List<Vector2Int> InitialPositionFromPieceType(PieceType type, bool IsWhite)
	{
		List<Vector2Int> l = new List<Vector2Int>();
		int FirstY = IsWhite ? 2 : 10;
		int SecondY = IsWhite ? 1 : 11;
		int ThirdY = IsWhite ? 0 : 12;
		switch (type)
		{
			case PieceType.Gore:
				for (int i = 0; i < 13; i++)
					l.Add(new Vector2Int(i, FirstY));
				break;
			case PieceType.Laage:
				l.Add(new Vector2Int(0, ThirdY));
				l.Add(new Vector2Int(12, ThirdY));
				break;
			case PieceType.Shamain:
				l.Add(new Vector2Int(5, SecondY));
				l.Add(new Vector2Int(7, SecondY));
				break;
			case PieceType.Gisharl:
				l.Add(new Vector2Int(4, ThirdY));
				l.Add(new Vector2Int(8, ThirdY));
				break;
			case PieceType.Woofein:
				l.Add(new Vector2Int(1, ThirdY));
				l.Add(new Vector2Int(11, ThirdY));
				break;
			case PieceType.Cain:
				l.Add(new Vector2Int(3, ThirdY));
				l.Add(new Vector2Int(9, ThirdY));
				break;
			case PieceType.Ozoa:
				l.Add(new Vector2Int(2, ThirdY));
				l.Add(new Vector2Int(10, ThirdY));
				break;
			case PieceType.Golcleo:
				l.Add(new Vector2Int(3, SecondY));
				l.Add(new Vector2Int(9, SecondY));
				break;
			case PieceType.Remagoguu:
				l.Add(new Vector2Int(7, ThirdY));
				break;
			case PieceType.Bolussa:
				l.Add(new Vector2Int(6, ThirdY));
				break;
			case PieceType.Mechet:
				l.Add(new Vector2Int(5, ThirdY));
				break;
			default: break;
		}
		return l;
	}
}