using System.Collections.Generic;
public abstract class Oath : IOath
{
	int PhaseCompletePiecesNum { get; set; }
	Board board { get; set; }
	List<IPiece> pieces { get; set; }
	public Oath(Board b, List<IPiece> l)
	{
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		UnityEngine.Debug.Log(l.Count.ToString());
	}
	public abstract void OathEffect();
}