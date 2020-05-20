using UnityEngine;
using System.Collections.Generic;
public class StockTest : MonoBehaviour
{
	[SerializeField] PieceStock stock;
	void Start()
	{
		List<IPiece> p = new List<IPiece>();
		Gore g = new Gore();
		g.Stage = 3;
		g.Experience = 10;
		p.Add(g);
		stock.AddStock(p);
	}
}