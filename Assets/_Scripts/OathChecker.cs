using System.Collections.Generic;
using UnityEngine;
public class OathChecker
{
	public List<IOath> Oaths = new List<IOath>();
	public bool IsWhitePlayer;
	public BoardManager boards;
	public List<List<Vector2Int>> RelativeCoordinates;
	void Start()
	{
		RelativeCoordinates = new List<List<Vector2Int>>();
		RelativeCoordinates.Add(ThreeRelativeCoordinates);
		RelativeCoordinates.Add(FourRelativeCoordinates);
		RelativeCoordinates.Add(FourAltRelativeCoordinates);
		RelativeCoordinates.Add(FiveRelativeCoordinates);
		RelativeCoordinates.Add(SixRelativeCoordinates);

	}
	public void CheckAllBoard()
	{
		CheckBoard(boards.mainBoard);
		foreach (Board b in boards.subBoards)
			CheckBoard(b);
	}
	public void CheckBoard(Board board)
	{

	}
	List<IPiece> PiecesPlacementCheck(int PiecesNum, IPiece target, Board b)
	{
		List<IPiece> p = new List<IPiece>();
		Vector2Int targetPos = target.PositionOnBoard;
		List<Vector2Int> relativePos = RelativeCoordinates[PiecesNum];
		for (int i = 0; i < PiecesNum; i++)
		{
			Vector2Int t = targetPos + relativePos[i];
			if (t.x < 0 || b.size <= t.x || t.y < 0 || b.size <= t.y || b.pieces[t.x, t.y] == null)
				return p;
			else
				p.Add(b.pieces[t.x, t.y]);
		}
		return p;
	}
	List<Vector2Int> ThreeRelativeCoordinates = new List<Vector2Int> { new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	List<Vector2Int> FourRelativeCoordinates = new List<Vector2Int> { new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	List<Vector2Int> FourAltRelativeCoordinates = new List<Vector2Int> { new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	List<Vector2Int> FiveRelativeCoordinates = new List<Vector2Int> { new Vector2Int(-1, -1), new Vector2Int(1, -1) };
	List<Vector2Int> SixRelativeCoordinates = new List<Vector2Int> { new Vector2Int(-1, -1), new Vector2Int(1, -1) };
}