using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class OathChecker : MonoBehaviour
{
	[SerializeField] OathManager manager;
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
					if (pieces[0].IsWhitePlayer)
						manager.WhiteOaths.Add(OathTypeInstantiate(board, pieces, true));
					else
						manager.BlackOaths.Add(OathTypeInstantiate(board, pieces, false));
			}
		}
	}
	bool DeplicationOathException(List<IPiece> l)
	{
		List<IOath> target;
		if (l[0].IsWhitePlayer)
			target = manager.PrevWhiteOaths.Where(x => x.pieces.Count == l.Count).ToList();
		else
			target = manager.PrevBlackOaths.Where(x => x.pieces.Count == l.Count).ToList();

		bool DeplicateCheck = true;
		for (int i = 0; i < target.Count; i++)
		{
			DeplicateCheck = true;
			foreach (IPiece p in l)
				DeplicateCheck = DeplicateCheck & target[i].pieces.Contains(p);
			if (DeplicateCheck)
				return true;
		}
		return false;
	}
	IOath OathTypeInstantiate(Board board, List<IPiece> pieces, bool IsWhite)
	{
		return new EnhanceOath(boards, board, pieces, IsWhite);
	}
}