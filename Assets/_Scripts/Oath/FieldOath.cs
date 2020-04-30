using System.Linq;
using System.Collections.Generic;
public class FieldOath : Oath
{
	public FieldOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }

	public override void OathEffect(OathPrepare prepare)
	{
		OnEffectActivated.Invoke(this);
		Board b = new Board();
		manager.AddSubBoard(b, boardAttribute, boardTime);
	}
}