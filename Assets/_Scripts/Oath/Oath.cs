using System.Collections.Generic;
public abstract class Oath : IOath
{
	protected int PhaseCompletePiecesNum { get; set; }
	protected Board board { get; set; }
	protected List<IPiece> pieces { get; set; }
	protected bool IsWhitePlayer { get; set; }
	protected BoardAttribute attribute;
	protected BoardTime boardTime;
	public Oath(Board b, List<IPiece> l, bool IsWhite)
	{
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		IsWhitePlayer = IsWhite;
	}
	public abstract void OathEffect();
}