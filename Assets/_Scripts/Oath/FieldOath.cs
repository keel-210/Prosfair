using System.Linq;
using System.Collections.Generic;
public class FieldOath : Oath
{
	public FieldOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public override void OathEffect()
	{
		Board b = new Board();
		manager.AddSubBoard(b);
	}
}