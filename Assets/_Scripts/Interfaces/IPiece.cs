using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
	PieceType pieceType { get; }
	Vector2Int PositionOnBoard { get; set; }
	int Stage { get; set; }
	int Experiment { get; set; }
	void Move();
	void KillSelf();
}
