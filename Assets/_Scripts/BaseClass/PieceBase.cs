using UnityEngine;
using System.Collections.Generic;
public abstract class PieceBase : MonoBehaviour, IPiece
{
	public Board board { get; set; }
	public PieceType pieceType { get; private set; }
	public Vector2Int PositionOnBoard { get; set; }
	public bool IsWhitePlayer { get; set; }
	public int Stage { get; set; }
	public int Experiment { get; set; }
	public abstract List<Vector2Int> CheckMovement();
	public void Move(Vector3 pos)
	{
		transform.position = pos;
	}
	public void KillSelf()
	{

	}
	public void BoardUpdate()
	{
		//Oathによる駒変形等の記述
	}
	protected void Check(List<Vector2Int> temp, Vector2Int target)
	{
		if (!IsWhitePlayer)
			target *= -1;
		int X = PositionOnBoard.x + target.x, Y = PositionOnBoard.y + target.y;
		if (X < 0 || board.size - 1 < X || Y < 0 || board.size - 1 < Y)
			return;
		IPiece p = board.pieces[X, Y];
		if (p == null)
			temp.Add(target + PositionOnBoard);
		else
		{
			if (p.Stage <= Stage && (IsWhitePlayer ^ p.IsWhitePlayer))
				temp.Add(target + PositionOnBoard);
		}
	}
	protected void RowCheck(List<Vector2Int> temp, Vector2Int dir)
	{
		if (!IsWhitePlayer)
			dir *= -1;
		for (int i = 0; i < board.size; i++)
		{
			int X = PositionOnBoard.x + dir.x * i, Y = PositionOnBoard.y + dir.y * i;
			if (X < 0 || board.size - 1 < X || Y < 0 || board.size - 1 < Y)
				break;
			IPiece p = board.pieces[X, Y];
			if (p == null)
				temp.Add(dir * i + PositionOnBoard);
			else
			{
				if (p.Stage <= Stage && (IsWhitePlayer ^ p.IsWhitePlayer))
					temp.Add(dir * i + PositionOnBoard);
				break;
			}
		}
	}
}