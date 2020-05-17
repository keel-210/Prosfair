using UnityEngine;
using System.Collections.Generic;

public class PieceStock : MonoBehaviour
{
	public bool IsWhitePlayer;
	public Stock targetStock;
	[SerializeField] PieceStockUI stockUI;
	[SerializeField] BasicReference basic;
	[SerializeField] GamePhaseManager phaseManager;
	List<Stock> stocks = new List<Stock>();
	BoardManager manager;
	void Start()
	{
	}
	void Update()
	{

	}
	public void AddStock(List<IPiece> list)
	{
		foreach (IPiece p in list)
			stocks.Add(new Stock(p.pieceType, p.Stage, p.Experience));
	}
	public void SetPieceFromStock(Vector2Int pos)
	{

	}
	void SetStock(PieceStock.Stock _stock, Board board, Vector2Int boardPos)
	{
		GameObject obj = Instantiate(basic.PieceReferrence[(int)_stock.type]);
		IPiece p = obj.GetComponent<IPiece>();
		obj.GetComponent<PieceBase>().IsWhitePlayer = IsWhitePlayer;
		board.AddPiece(p, boardPos);
		phaseManager.NextPhase();
	}
	public class Stock
	{
		public PieceType type { get; private set; }
		public int Stage { get; private set; }
		public int Exp { get; private set; }
		public Stock(PieceType t, int s, int e)
		{
			type = t;
			Stage = s;
			Exp = e;
		}
	}
}