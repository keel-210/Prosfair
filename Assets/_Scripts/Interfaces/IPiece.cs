using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
	Board board { get; set; }
	PieceType pieceType { get; }
	PieceAttribute pieceAttribute { get; set; }
	Vector2Int PositionOnBoard { get; set; }
	Vector3 PositionInWorld { get; set; }
	bool IsWhitePlayer { get; set; }

	int Stage { get; set; }
	int Experience { get; set; }
	void Move(Vector3 pos);
	void KillSelf();
	List<Vector2Int> CheckMovement();
	void BoardUpdate();
}
