using System.Collections.Generic;
using UnityEngine;
public class OathChecker : MonoBehaviour
{
	public List<IOath> Oaths = new List<IOath>();
	public bool IsWhitePlayer;
	public BoardManager boards;
	public List<List<Vector2Int>> RelativeCoordinates;
	void Start()
	{
		RelativeCoordinates = new List<List<Vector2Int>>();
		RelativeCoordinates.Add(RelativeCoordinates3);
		RelativeCoordinates.Add(RelativeCoordinates4);
		RelativeCoordinates.Add(RelativeCoordinates5);
		RelativeCoordinates.Add(RelativeCoordinates6);
		RelativeCoordinates.Add(RelativeCoordinates7);
		// RelativeCoordinates.Add(RelativeCoordinates9);
		// RelativeCoordinates.Add(RelativeCoordinates11);
		// RelativeCoordinates.Add(RelativeCoordinates13);
		// RelativeCoordinates.Add(RelativeCoordinates15);
		// RelativeCoordinates.Add(RelativeCoordinates17);
		// RelativeCoordinates.Add(RelativeCoordinates21);
		// RelativeCoordinates.Add(RelativeCoordinates26);
		// RelativeCoordinates.Add(RelativeCoordinates30);
	}
	public void CheckAllBoard()
	{
		CheckBoard(boards.mainBoard);
		foreach (Board b in boards.subBoards)
			CheckBoard(b);
	}
	public void CheckBoard(Board board)
	{
		foreach (IPiece p in board.pieces)
		{
			if (p == null)
				continue;
			foreach (List<Vector2Int> r in RelativeCoordinates)
			{
				List<IPiece> pieces = PiecesPlacementCheck(r, p, board);
				if (pieces.Count == r.Count)
					Oaths.Add(OathTypeInstantiate(board, pieces));
			}
		}
	}
	IOath OathTypeInstantiate(Board board, List<IPiece> pieces)
	{
		return new EnhanceOath(board, pieces);
	}
	List<IPiece> PiecesPlacementCheck(List<Vector2Int> relativePos, IPiece target, Board b)
	{
		List<IPiece> p = new List<IPiece>();
		Vector2Int targetPos = target.PositionOnBoard;
		for (int i = 0; i < relativePos.Count; i++)
		{
			Vector2Int t = targetPos + relativePos[i];
			if (t.x < 0 || b.size <= t.x || t.y < 0 || b.size <= t.y || b.pieces[t.x, t.y] == null)
				return p;
			else
				p.Add(b.pieces[t.x, t.y]);
		}
		return p;
	}
	List<Vector2Int> RelativeCoordinates3 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	List<Vector2Int> RelativeCoordinates4 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, -1) };
	List<Vector2Int> RelativeCoordinates5 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };
	List<Vector2Int> RelativeCoordinates6 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, -1) };
	List<Vector2Int> RelativeCoordinates7 = new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };
	List<Vector2Int> RelativeCoordinates9;
	List<Vector2Int> RelativeCoordinates11;
	List<Vector2Int> RelativeCoordinates13;
	List<Vector2Int> RelativeCoordinates15;
	List<Vector2Int> RelativeCoordinates17;
	List<Vector2Int> RelativeCoordinates21;
	List<Vector2Int> RelativeCoordinates26;
	List<Vector2Int> RelativeCoordinates30;
}