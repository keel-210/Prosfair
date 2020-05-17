using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class OathManager : MonoBehaviour
{
	[SerializeField] public GamePhaseManager phaseManager;
	[SerializeField] BoardManager boards;
	public List<Oath> WhiteOaths = new List<Oath>(), BlackOaths = new List<Oath>();
	public List<Oath> PrevWhiteOaths = new List<Oath>(), PrevBlackOaths = new List<Oath>();
	[SerializeField] OathButtons buttons;
	bool IsChecked;
	void Update()
	{
		if (!IsChecked)
		{
			if (phaseManager.IsWhitePlaying && (phaseManager.WhitePlayerPhase == PlayerPhase.FirstOath || phaseManager.WhitePlayerPhase == PlayerPhase.SecondOath))
				CheckBoard();
			if (!phaseManager.IsWhitePlaying && (phaseManager.BlackPlayerPhase == PlayerPhase.FirstOath || phaseManager.BlackPlayerPhase == PlayerPhase.SecondOath))
				CheckBoard();
		}
	}
	public void CheckBoard()
	{
		IsChecked = true;
		WhiteOaths.Clear();
		BlackOaths.Clear();
		buttons.Clear();

		WhiteOaths = CheckOaths(true);
		BlackOaths = CheckOaths(false);
		WhiteOaths.ForEach(x => x.OnEffectActivated.AddListener(OathRemove));
		BlackOaths.ForEach(x => x.OnEffectActivated.AddListener(OathRemove));

		buttons.LoadOaths(phaseManager.IsWhitePlaying);
	}
	public void ResetCheckStatus()
	{
		IsChecked = false;
	}
	void OathRemove(Oath oath)
	{
		(oath.IsWhitePlayer ? WhiteOaths : BlackOaths).Remove(oath);
		(oath.IsWhitePlayer ? PrevWhiteOaths : PrevBlackOaths).Add(oath);
	}
	public List<Oath> CheckOaths(bool IsWhite)
	{
		List<Oath> o = new List<Oath>();
		o.AddRange(CheckBoard(boards.mainBoard, IsWhite));
		foreach (Board b in boards.subBoards)
			o.AddRange(CheckBoard(b, IsWhite));
		return o;
	}
	//ここ階層深すぎ
	List<Oath> CheckBoard(Board board, bool IsWhite)
	{
		List<Oath> o = new List<Oath>();
		foreach (IPiece p in board.pieces)
		{
			if (p == null || p.IsWhitePlayer != IsWhite)
				continue;
			foreach (List<Vector2Int> r in OathChecker.RelativeCoordinates)
			{
				List<IPiece> pieces = OathUtils.PiecesPlacementCheck(r, p, board);
				if (pieces.Count == r.Count && !OathUtils.IsInitialPlacementException(pieces))
					if (pieces[0].IsWhitePlayer == IsWhite && !DeplicationOathException(pieces))
						o.Add(OathTypeInstantiate(boards, board, pieces, IsWhite));
			}
		}
		return o;
	}
	bool DeplicationOathException(List<IPiece> l)
	{
		List<Oath> target;
		if (l[0].IsWhitePlayer)
			target = PrevWhiteOaths.Where(x => x.pieces.Count == l.Count).ToList();
		else
			target = PrevBlackOaths.Where(x => x.pieces.Count == l.Count).ToList();

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
	public Oath OathTypeInstantiate(BoardManager boards, Board board, List<IPiece> pieces, bool IsWhite)
	{
		var check = OathChecker.FieldOathCheck(board, pieces, IsWhite);
		if (check != null && check.FieldSize < board.size)
		{
			Debug.Log((check != null).ToString() + " " + check.WhitePieceCount + " " + check.BlackPieceCount);
			var f = new FieldOath(OathType.Field, boards, board, pieces, IsWhite);
			f.Initialize(check);
			return f;
		}
		if (pieces.Count >= 9)
			if (board.OccupiedPlayer != BoardOccupation.NonOccupied)
				return new FieldAbandonmentOath(OathType.TypeEnhance, boards, board, pieces, IsWhite, phaseManager.WhiteStock, phaseManager.BlackStock);
			else
				return new TypeEnhanceOath(OathType.TypeEnhance, boards, board, pieces, IsWhite);
		else
			return new EnhanceOath(OathType.Enhance, boards, board, pieces, IsWhite);
	}
}