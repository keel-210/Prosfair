using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class PieceStockUI : MonoBehaviour
{
	//必要な情報はTargetのStockだけ
	public PieceStock.Stock target = default;
	public bool IsSelectingOtherThanDefault;
	[SerializeField] Dropdown dropdown;
	[SerializeField] PieceStock stock;
	List<PieceStock.Stock> list = new List<PieceStock.Stock>();
	void Start()
	{
		dropdown.ClearOptions();
	}
	public void LoadStock(PieceStock _stock)
	{
		stock = _stock;
		dropdown.ClearOptions();
		list.Clear();
		list.Add(new PieceStock.Stock(PieceType.Gore, 0, 0));
		list.AddRange(stock.stocks);
		var l = list.Select(x => x.type + ":" + x.Stage + ":" + x.Exp).ToList();
		dropdown.AddOptions(l);
	}
	void Update()
	{
		IsSelectingOtherThanDefault = false;
		if (dropdown.options.Count != 0)
		{
			target = list[dropdown.value];
			if (dropdown.options.Count > 0 && dropdown.value != 0)
				IsSelectingOtherThanDefault = true;
		}
	}
}