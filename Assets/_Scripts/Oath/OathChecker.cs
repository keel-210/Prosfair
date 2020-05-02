using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class OathChecker : MonoBehaviour
{
	//managerと共依存なので改善する
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
	public List<Oath> CheckOaths(bool IsWhite)
	{
		List<Oath> o = new List<Oath>();
		o.AddRange(CheckBoard(boards.mainBoard, IsWhite));
		foreach (Board b in boards.subBoards)
			o.AddRange(CheckBoard(b, IsWhite));
		return o;
	}
	List<Oath> CheckBoard(Board board, bool IsWhite)
	{
		List<Oath> o = new List<Oath>();
		foreach (IPiece p in board.pieces)
		{
			if (p == null || p.IsWhitePlayer != IsWhite)
				continue;
			foreach (List<Vector2Int> r in RelativeCoordinates)
			{
				List<IPiece> pieces = OathUtils.PiecesPlacementCheck(r, p, board);
				if (pieces.Count == r.Count && !OathUtils.IsInitialPlacementException(pieces))
					if (pieces[0].IsWhitePlayer == IsWhite && !DeplicationOathException(pieces))
						o.Add(OathTypeInstantiate(board, pieces, IsWhite));
			}
		}
		return o;
	}
	bool DeplicationOathException(List<IPiece> l)
	{
		List<Oath> target;
		if (l[0].IsWhitePlayer)
			target = manager.PrevWhiteOaths.Where(x => x.pieces.Count == l.Count).ToList();
		else
			target = manager.PrevBlackOaths.Where(x => x.pieces.Count == l.Count).ToList();

		bool DeplicateCheck = true;
		foreach (Oath t in target)
		{
			DeplicateCheck = true;
			foreach (IPiece p in l)
				DeplicateCheck = DeplicateCheck & t.pieces.Contains(p);
			if (DeplicateCheck)
				return true;
		}
		return false;
	}
	Oath OathTypeInstantiate(Board board, List<IPiece> pieces, bool IsWhite)
	{
		Oath o;
		if (FieldOathCheck())
			o = new FieldOath(boards, board, pieces, IsWhite);
		else
			o = new EnhanceOath(boards, board, pieces, IsWhite);
		return o;
	}
	bool FieldOathCheck()
	{
		//位相の範囲内に相手駒が2つ以上ある->true
		return false;
	}
}