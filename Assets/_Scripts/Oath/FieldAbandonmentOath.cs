using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldAbandonmentOath : Oath
{
	Player White, Black;
	public FieldAbandonmentOath(OathType _type, BoardManager _manager, Board b, List<IPiece> l, bool IsWhite, Player _White, Player _Black) : base(_type, _manager, b, l, IsWhite)
	{
		type = _type;
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		IsWhitePlayer = IsWhite;
		manager = _manager;
		var Xs = l.Select(X => X.PositionOnBoard.x);
		var Ys = l.Select(X => X.PositionOnBoard.y);
		minRegion = new Vector2Int(Xs.Min(), Ys.Min());
		maxRegion = new Vector2Int(Xs.Max(), Ys.Max());
		White = _White;
		Black = _Black;
	}
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }
	FieldCheck check { get; set; }
	public void Initialize(FieldCheck _check)
	{
		check = _check;
	}
	public override void OathEffect(OathUIData UIData)
	{
		OnEffectActivated.Invoke(this);
		Vector2Int p = new Vector2Int((minRegion.x + maxRegion.x) / 2, (minRegion.y + maxRegion.y) / 2);
		manager.AbandonmentSubBoard(board, p, PieceUtils.SpecialTypeFromOathCount(pieces.Count), IsWhitePlayer);
		StockPieces(board.OriginalOathBoard, minRegion, maxRegion);
		GameRecorder.FieldAbandonmentOathRecord(PieceUtils.SpecialTypeFromOathCount(pieces.Count), p);
	}
	void StockPieces(Board b, Vector2Int minRegion, Vector2Int maxRegion)
	{
		var PiecesInRegion = BoardUtils.GetPiecesInRegion(b, minRegion, maxRegion);
		var WhitesInRegion = PiecesInRegion.Where(x => x.IsWhitePlayer == true).ToList();
		var BlacksInRegion = PiecesInRegion.Where(x => x.IsWhitePlayer == false).ToList();
		White.stock.AddStock(WhitesInRegion);
		Black.stock.AddStock(BlacksInRegion);
		foreach (IPiece p in PiecesInRegion)
			p.KillSelf();
	}
}