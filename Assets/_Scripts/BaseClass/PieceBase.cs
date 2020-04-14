using UnityEngine;
using System.Collections.Generic;
public abstract class PieceBase : MonoBehaviour, IPiece, IBoardObserver
{
	public PieceType pieceType { get; private set; }
	public Vector2Int PositionOnBoard { get; set; }
	public int Stage { get; set; }
	public int Experiment { get; set; }

	public void Move()
	{

	}
	public void KillSelf()
	{

	}
	public void UpdateBoard()
	{

	}
}