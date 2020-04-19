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
	public static bool IsDefeatByTargetPiece()
	{
		return true;
	}
	public static int CheckPieceStrength(PieceType type)
	{
		return 0;
	}

}