using System.Collections.Generic;
public abstract class Oath : IOath
{
	public bool IsWhitePlayer { get; set; }
	public List<IPiece> pieces { get; set; }

	protected int PhaseCompletePiecesNum { get; set; }
	protected Board board { get; set; }
	protected BoardAttribute attribute;
	protected BoardTime boardTime;
	protected BoardManager manager { get; set; }
	public Oath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite)
	{
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		IsWhitePlayer = IsWhite;
		manager = _manager;
	}
	public abstract void OathEffect();
}