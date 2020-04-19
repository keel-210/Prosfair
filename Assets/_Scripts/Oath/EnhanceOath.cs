using System.Linq;
using System.Collections.Generic;
public class EnhanceOath : Oath
{
	public EnhanceOath(Board b, List<IPiece> l, bool IsWhite) : base(b, l, IsWhite) { }
	public override void OathEffect()
	{
		IPiece p = pieces.OrderBy(x => x.PositionOnBoard.y).First();
		if (pieces.Count >= p.Stage)
			p.Stage++;
	}
}