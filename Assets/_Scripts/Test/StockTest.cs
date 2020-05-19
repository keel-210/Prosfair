using UnityEngine;
using System.Collections.Generic;
public class StockTest : MonoBehaviour
{
	[SerializeField] PieceStock stock;
	void Start()
	{
		List<IPiece> p = new List<IPiece>();
		p.Add(new Gore());
		stock.AddStock(p);
	}
}