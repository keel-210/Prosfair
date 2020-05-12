using System.Linq;
using System.Collections.Generic;
public class TypeEnhanceOath : Oath
{
	public IPiece EnhanceTargetPiece { get; set; }
	public TypeEnhanceOath(OathType _type, BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_type, _manager, b, l, IsWhite) { }
	public override void OathEffect(OathUIData prepare)
	{
		if (board.attribute == BoardAttribute.Donamia)
			return;
		OnEffectActivated.Invoke(this);
		PieceType target = prepare.MultipleTargetPieceType;
		manager.AllFieldEnhance(new List<PieceType> { target }, 1);
	}
}