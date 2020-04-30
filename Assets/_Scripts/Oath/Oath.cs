using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class Oath : IOath
{
	public bool IsWhitePlayer { get; set; }
	public List<IPiece> pieces { get; set; }
	public Board board { get; set; }
	public Vector2Int minRegion { get; set; }
	public Vector2Int maxRegion { get; set; }
	public OnEffectCallback OnEffectActivated { get; set; } = new OnEffectCallback();
	public OnPrevEffectPrepare OnEffectPrepare { get; set; } = new OnPrevEffectPrepare();

	protected int PhaseCompletePiecesNum { get; set; }
	protected BoardManager manager { get; set; }
	public Oath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite)
	{
		board = b;
		PhaseCompletePiecesNum = l.Count;
		pieces = l;
		IsWhitePlayer = IsWhite;
		manager = _manager;
		var Xs = l.Select(X => X.PositionOnBoard.x);
		var Ys = l.Select(X => X.PositionOnBoard.y);
		minRegion = new Vector2Int(Xs.Min(), Ys.Min());
		maxRegion = new Vector2Int(Xs.Max(), Ys.Max());
	}
	public virtual void OathEffect(OathPrepare prepare)
	{
		OnEffectPrepare.Invoke(prepare);
	}
}
public class OnEffectCallback : UnityEngine.Events.UnityEvent<Oath> { }
public class OnPrevEffectPrepare : UnityEngine.Events.UnityEvent<OathPrepare> { }
