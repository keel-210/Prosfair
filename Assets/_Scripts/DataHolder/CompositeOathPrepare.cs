using System.Collections.Generic;
public class CompositeOathPrepare
{
	public CompositeOathPrepare(Board b, List<IPiece> p)
	{
		board = b;
		pieces = p;
	}
	public Board board { get; set; }
	public List<IPiece> pieces { get; set; }
}