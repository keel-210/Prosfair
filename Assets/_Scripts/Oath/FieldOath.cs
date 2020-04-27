using System.Linq;
using System.Collections.Generic;
public class FieldOath : Oath
{
	public FieldOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public BoardAttribute attribute;
	public BoardTime boardTime;

	public override void OathEffect()
	{
		OnEffectActivated.Invoke(this);
		Board b = new Board();
		manager.AddSubBoard(b, attribute, boardTime);
	}
}