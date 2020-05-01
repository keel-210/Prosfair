using System.Linq;
using System.Collections.Generic;
public class EnhanceOath : Oath
{
	public IPiece EnhanceTargetPiece { get; set; }
	public EnhanceOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public override void OathEffect(OathPrepare prepare)
	{
		OnEffectActivated.Invoke(this);
		IPiece p = prepare.TargetPiece;
		if (pieces.Count >= p.Stage)
			p.Stage++;
	}
}