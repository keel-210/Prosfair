using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
	Board board { get; set; }
	PieceType pieceType { get; }
	Vector2Int PositionOnBoard { get; set; }
	bool IsWhitePlayer { get; set; }

	int Stage { get; set; }
	int Experiment { get; set; }
	void Move(Vector3 pos);
	void KillSelf();
	List<Vector2Int> CheckMovement();
	void BoardUpdate();
}
