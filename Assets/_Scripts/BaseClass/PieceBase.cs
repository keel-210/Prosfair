using UnityEngine;
using System.Collections.Generic;
public abstract class PieceBase : MonoBehaviour, IPiece
{
	public Board board { get; set; }
	public PieceType pieceType { get; protected set; }
	public Vector2Int PositionOnBoard { get; set; }
	public PieceAttribute pieceAttribute { get; set; }
	public bool IsWhitePlayer { get; set; }
	public int Stage
	{
		get { return _Stage; }
		set
		{
			if (value > 0)
				_Stage = value;
			else
				_Stage = 1;
		}
	}
	private int _Stage;
	public int Experience { get; set; }
	public Vector3 PositionInWorld { get; set; }
	public abstract List<Vector2Int> CheckMovement();
	void Start()
	{
		Initialize();
		Stage = PieceUtils.PieceInitialStage(pieceType);
	}
	protected abstract void Initialize();
	public void Move(Vector3 pos)
	{
		transform.position = pos;
		PositionInWorld = pos;
		PositionOnBoard = board.ObjectSpaceToBoardSpace(pos);
		// Debug.Log(PositionOnBoard);
	}
	public void KillSelf()
	{
		Destroy(gameObject);
	}
	public void BoardUpdate()
	{
	}
	protected void Check(List<Vector2Int> temp, Vector2Int target)
	{
		if (!IsWhitePlayer)
			target *= -1;
		int X = PositionOnBoard.x + target.x, Y = PositionOnBoard.y + target.y;
		if (X < 0 || board.size - 1 < X || Y < 0 || board.size - 1 < Y || !IsMovableByBoardTime(target))
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
		for (int i = 1; i < board.size; i++)
		{
			int X = PositionOnBoard.x + dir.x * i, Y = PositionOnBoard.y + dir.y * i;
			if (X < 0 || board.size - 1 < X || Y < 0 || board.size - 1 < Y || !IsMovableByBoardTime(dir))
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
	protected bool IsMovableByBoardTime(Vector2Int dir)
	{
		if (board.boardTime == BoardTime.Claint)
			return true;
		if (board.boardTime == BoardTime.Pasusu)
			if (IsWhitePlayer && dir.y > 0)
				return false;
			else if (!IsWhitePlayer && dir.y < 0)
				return false;
		if (board.boardTime == BoardTime.Ftuule)
			if (IsWhitePlayer && dir.y < 0)
				return false;
			else if (!IsWhitePlayer && dir.y > 0)
				return false;
		return true;
	}
}