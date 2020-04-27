using System.Linq;
using System.Collections.Generic;
public class EnhanceOath : Oath
{
	public EnhanceOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public override void OathEffect()
	{
		OnEffectActivated.Invoke(this);
		IPiece p = pieces.OrderBy(x => x.PositionOnBoard.y).First();
		if (pieces.Count >= p.Stage)
			p.Stage++;
	}
	int EnhanceLevel(int pieceCount, IPiece p)
	{
		return 0;
	}
}