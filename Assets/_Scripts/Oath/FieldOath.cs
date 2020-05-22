using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldOath : Oath
{
	public FieldOath(OathType _type, BoardManager _manager, Board b, List<IPiece> l, bool IsWhite, FieldCheck _check) : base(_type, _manager, b, l, IsWhite)
	{
		type = _type;
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		IsWhitePlayer = IsWhite;
		manager = _manager;
		var Xs = l.Select(X => X.PositionOnBoard.x);
		var Ys = l.Select(X => X.PositionOnBoard.y);
		check = _check;
	}
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }
	FieldCheck check { get; set; }
	public override void OathEffect(OathUIData UIData)
	{
		OnEffectActivated.Invoke(this);
		manager.AddSubBoard(this, UIData, check);
		OathEffect2Piece();
	}
	void OathEffect2Piece()
	{
		var targetTypes = OathUtils.InversionTargetPieceTypebyBoardTime(boardTime);
		var InvTargetTypes = OathUtils.InversionTargetPieceTypebyBoardTime(boardTime);
		var targetAttribute = OathUtils.AttributebyBoardAttribute(boardAttribute);
		manager.AllFieldEnhance(targetTypes, 1);
		if (boardAttribute == BoardAttribute.Vettoria)
			manager.AllFieldEnhance(InvTargetTypes, -1);
		manager.AllFieldExperience(targetTypes);
		manager.AllPieceAttribute(targetTypes, targetAttribute);
	}
}