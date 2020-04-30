using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public interface IOath
{
	bool IsWhitePlayer { get; set; }
	List<IPiece> pieces { get; set; }
	Board board { get; set; }

	Vector2Int minRegion { get; set; }
	Vector2Int maxRegion { get; set; }
	OnEffectCallback OnEffectActivated { get; set; }
}