using UnityEngine;
using System.Collections.Generic;

public class PieceStock : MonoBehaviour
{
	//Field多すぎ問題
	public PieceStockUI stockUI;
	public List<Stock> stocks = new List<Stock>();
	[SerializeField] BasicReference basic;
	[SerializeField] GamePhaseManager phaseManager;
	[SerializeField] BoardManager manager;
	[SerializeField] bool IsWhitePlayer;
	List<Vector2Int> MovablePos = new List<Vector2Int>();
	List<GameObject> MovableFieldCache = new List<GameObject>();
	Stock targetStockCashe;
	bool IsLoaded;
	void Update()
	{
		if (phaseManager.Phase() == PlayerPhase.PieceSelect ||
		phaseManager.Phase() == PlayerPhase.PieceSelected)
			SetStockPhase();
		else if (phaseManager.Phase() == PlayerPhase.OpponentTurn)
			Reset();
	}
	void OnDisable()
	{
		Reset();
	}
	void PieceSelectPhase()
	{
		if (!IsLoaded)
		{
			stockUI.LoadStock(this);
			IsLoaded = true;
		}
	}
	void SetStockPhase()
	{
		PieceSelectPhase();
		if (targetStockCashe != stockUI.target)
		{
			MovablePos = PieceUtils.InitialPositionFromPieceType(stockUI.target.type, IsWhitePlayer);
			MovablePos = PosSanityCheck(manager.mainBoard, MovablePos);
			DisplayEnableSetPos();
		}
		if (!stockUI.IsSelectingOtherThanDefault)
		{
			MovableFieldCache.ForEach(x => Destroy(x));
			MovableFieldCache.Clear();
			phaseManager.CancelPiece();
		}
		if (MovablePos.Count == 0 || !Input.GetMouseButtonDown(0))
			return;
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
		{
			Vector2Int boardPos = rayPos.board.ObjectSpaceToBoardSpace(rayPos.pos);
			if (MovablePos.Contains(boardPos))
				SetStock(stockUI.target, rayPos.board, boardPos);
		}
	}
	void Reset()
	{
		if (!IsLoaded)
			return;
		IsLoaded = false;
		MovableFieldCache.ForEach(x => Destroy(x));
		MovableFieldCache.Clear();
		targetStockCashe = null;
	}
	void DisplayEnableSetPos()
	{
		MovableFieldCache.ForEach(x => Destroy(x));
		MovableFieldCache.Clear();
		foreach (Vector2Int v in MovablePos)
			MovableFieldCache.Add(Instantiate(basic.FieldReference[2], manager.mainBoard.BoardSpaceToObjectSpace(v), Quaternion.identity));
		targetStockCashe = stockUI.target;
	}
	List<Vector2Int> PosSanityCheck(Board board, List<Vector2Int> list)
	{
		List<Vector2Int> l = new List<Vector2Int>();
		foreach (Vector2Int v in list)
			if (board.pieces[v.x, v.y] == null)
				l.Add(v);
		return l;
	}
	public void AddStock(List<IPiece> list)
	{
		foreach (IPiece p in list)
			stocks.Add(new Stock(p.pieceType, p.Stage, p.Experience));
	}
	void SetStock(PieceStock.Stock _stock, Board board, Vector2Int boardPos)
	{
		GameObject obj = Instantiate(basic.PieceReferrence[(int)_stock.type]);
		IPiece p = obj.GetComponent<IPiece>();
		obj.GetComponent<PieceBase>().IsWhitePlayer = IsWhitePlayer;
		board.AddPiece(p, boardPos);
		phaseManager.GoToSeceondOath();
		stocks.Remove(_stock);
	}
	BoardAndPos RayCastScreen()
	{
		BoardAndPos bp = new BoardAndPos();
		bp.pos = Vector3.zero;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		LayerMask mask = 1 << 10;
		if (Physics.Raycast(ray, out hit, 100f, mask))
		{
			bp.board = hit.collider.GetComponent<Board_MonoProxy>().board;
			bp.PosOnBoard = bp.board.ObjectSpaceToBoardSpace(hit.point);
			bp.pos = hit.point;
		}
		return bp;
	}
	public class Stock
	{
		public PieceType type { get; private set; }
		public int Stage { get; private set; }
		public int Exp { get; private set; }
		public Stock(PieceType t, int s, int e)
		{
			type = t;
			Stage = s;
			Exp = e;
		}
	}
	public class BoardAndEnableSetPos
	{
		public Board board { get; private set; }
		public List<Vector2Int> Positions { get; private set; }
		public BoardAndEnableSetPos(Board b, List<Vector2Int> l)
		{
			board = b;
			Positions = l;
		}
	}
}