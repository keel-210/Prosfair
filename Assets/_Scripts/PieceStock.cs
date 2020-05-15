using UnityEngine;
using System.Collections.Generic;

public class PieceStock : MonoBehaviour
{
	public bool IsWhitePlayer;
	List<Stock> stocks = new List<Stock>();
	public void AddStock(List<IPiece> list)
	{
		foreach (IPiece p in list)
			stocks.Add(new Stock(p.pieceType, p.Stage, p.Experience));
	}
	public void SetPieceFromStock()
	{

	}
	public class Stock
	{
		PieceType type { get; set; }
		int Stage { get; set; }
		int Exp { get; set; }
		public Stock(PieceType t, int s, int e)
		{
			type = t;
			Stage = s;
			Exp = e;
		}
	}
}