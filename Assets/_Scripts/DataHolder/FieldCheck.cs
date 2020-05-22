using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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