using System.Linq;
using System.Collections.Generic;
public class EnhanceOath : Oath
{
	public IPiece EnhanceTargetPiece { get; set; }
	public EnhanceOath(OathType _type, BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_type, _manager, b, l, IsWhite) { }
	public override void OathEffect(OathUIData prepare)
	{
		if (board.attribute == BoardAttribute.Donamia)
			return;
		OnEffectActivated.Invoke(this);
		IPiece p = prepare.TargetPiece;
		if (pieces.Count > p.Stage)
			p.Stage++;
	}
}