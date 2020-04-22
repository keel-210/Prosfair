using System.Collections.Generic;
public interface IOath
{
	bool IsWhitePlayer { get; set; }
	List<IPiece> pieces { get; set; }
	void OathEffect();
}