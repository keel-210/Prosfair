using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class PieceStockUI : MonoBehaviour
{
	[SerializeField] Dropdown dropdown;
	//必要な情報はTargetのStockだけ
	public PieceStock.Stock target;
	PieceStock stock;
	List<PieceStock.Stock> list = new List<PieceStock.Stock>();
	void Start()
	{
		dropdown.ClearOptions();
	}
	public void LoadStock()
	{
		dropdown.ClearOptions();
		list.Clear();
		list.Add(new PieceStock.Stock(PieceType.Gore, 0, 0));
		list.AddRange(stock.stocks);
		var l = list.Select(x => x.type + ":" + x.Stage + ":" + x.Exp).ToList();
		dropdown.AddOptions(l);
	}
	void Update()
	{
		target = list[dropdown.value];
	}
}