using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldOath : Oath
{
	public FieldOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }
	FieldCheck check { get; set; }
	public void Initialize(FieldCheck _check)
	{
		check = _check;
	}
	public override void OathEffect(OathPrepare prepare)
	{
		OnEffectActivated.Invoke(this);

		Board b = new Board();
		b.size = check.FieldSize;
		manager.AddSubBoard(b, prepare.boardAttribute, prepare.boardTime);
		foreach (IPiece p in check.AllPieces)
			b.AddPiece(p, p.PositionOnBoard - check.FieldPos);
	}
}