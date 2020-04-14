using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Board : MonoBehaviour
{
	public int size;
	List<PieceBase> pieces;
	PieceBase[,] piecesOnBoard;
	public Board(int _size)
	{
		size = _size;
		piecesOnBoard = new PieceBase[size, size];
	}
	public void UpdateBoardStatus()
	{
		pieces.ForEach(x => x.UpdateBoard());
	}
	public Vector2Int ObjectSpaceToBoardSpace(Vector3 o_pos)
	{
		Vector3 v = (o_pos - transform.position + new Vector3(0.05f, 0, 0.05f)) / 0.1f;
		v = new Vector3(Mathf.Floor(v.x), 0, Mathf.Floor(v.z));
		Vector2Int vi = new Vector2Int((int)v.x + size / 2, (int)v.z + size / 2);
		return vi;
	}
	public Vector3 BoardSpaceToObjectSpace(Vector2Int b_pos)
	{
		Vector2Int vi = b_pos - Vector2Int.one * (size / 2);
		Vector3 v = new Vector3(vi.x * 0.1f, 0, vi.y * 0.1f);
		return v + transform.position;
	}
	public PieceBase GetPieceOnTargetPosition(Vector2Int pos)
	{
		return piecesOnBoard[pos.x, pos.y];
	}
}