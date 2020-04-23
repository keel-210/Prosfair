using UnityEngine;
using System.Collections.Generic;
public interface IOath
{
	bool IsWhitePlayer { get; set; }
	List<IPiece> pieces { get; set; }
	Board board { get; set; }

	Vector2Int minRegion { get; set; }
	Vector2Int maxRegion { get; set; }
	void OathEffect();
}