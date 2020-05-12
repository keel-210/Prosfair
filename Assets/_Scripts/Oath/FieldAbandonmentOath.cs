using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class FieldAbandonmentOath : Oath
{
	public FieldAbandonmentOath(OathType _type, BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_type, _manager, b, l, IsWhite) { }
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
		manager.AbandonmentSubBoard(board, UIData);
	}
}