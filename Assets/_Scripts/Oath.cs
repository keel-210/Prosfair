using System.Collections.Generic;
public abstract class Oath : IOath
{
	int PhaseCompletePiecesNum { get; set; }
	Board board { get; set; }
	List<IPiece> pieces { get; set; }
	public void SetOathDetail(Board b, List<IPiece> l)
	{
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
	}
	public abstract void OathEffect();
}