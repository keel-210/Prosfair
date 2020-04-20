using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class OathChecker : MonoBehaviour
{
	public List<IOath> Oaths = new List<IOath>();
	public List<IOath> PrevOaths = new List<IOath>();
	public bool IsWhitePlayer;
	public BoardManager boards;
	public List<List<Vector2Int>> RelativeCoordinates;
	void Start()
	{
		RelativeCoordinates = new List<List<Vector2Int>>();
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates3);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates4);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates5);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates6);
		RelativeCoordinates.Add(OathUtils.RelativeCoordinates7);

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
				List<IPiece> pieces = OathUtils.PiecesPlacementCheck(r, p, board);
				if (pieces.Count == r.Count && !OathUtils.IsInitialPlacementException(pieces))
					Oaths.Add(OathTypeInstantiate(board, pieces));
			}
		}
	}
	IOath OathTypeInstantiate(Board board, List<IPiece> pieces)
	{
		return new EnhanceOath(board, pieces, IsWhitePlayer);
	}
}