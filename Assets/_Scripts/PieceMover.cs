using UnityEngine;
using System.Collections.Generic;

//そもそも機能をPlayerに乗っけすぎ問題？StockはStockで独立してもよいのでは
//PhaseManagerでPhase管理は窓口できてるんだし
//これPlayerじゃなくてPieceMoverだな
public class PieceMover : MonoBehaviour
{
	public GameObject MovableFieldReference;
	//いろんなところからアクセスしすぎだこれでよし
	//PhaseManagerに任せた方がいいのではPieceMoverになった以上phaseは分離するべきでは
	public IPiece TargetPiece;
	public bool IsWhitePlayer = false;
	[SerializeField] GamePhaseManager phaseManager;
	List<GameObject> MovableFieldCache = new List<GameObject>();
	List<Vector2Int> MovablePos = new List<Vector2Int>();
	BoardAndPos BoardAndPosCache;
	void Update()
	{
		if (phaseManager.Phase(IsWhitePlayer) == PlayerPhase.PieceSelect)
			PieceSelectPhase();
		else if (phaseManager.Phase(IsWhitePlayer) == PlayerPhase.PieceSelected)
			PieceMovePhase();
	}
	void PieceSelectPhase()
	{
		if (!Input.GetMouseButtonDown(0))
			return;
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
			DisplayMovablePosition(rayPos);
	}
	void PieceMovePhase()
	{
		if (!Input.GetMouseButtonDown(0))
			return;
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
		{
			Vector2Int boardPos = rayPos.board.ObjectSpaceToBoardSpace(rayPos.pos);
			if (BoardAndPosCache.board != rayPos.board || !MovablePos.Contains(boardPos))
				CancelPiece();
			else
				MovePiece(rayPos, boardPos);
		}
		else
			CancelPiece();
	}
	void MovePiece(BoardAndPos rayPos, Vector2Int boardPos)
	{
		rayPos.board.MovePiece(TargetPiece, BoardAndPosCache.PosOnBoard, boardPos);
		MovablePos.Clear();
		BoardAndPosCache = null;
		MovableFieldCache.ForEach(x => Destroy(x));
		MovableFieldCache.Clear();
		phaseManager.NextPhase();
	}
	void CancelPiece()
	{
		MovablePos.Clear();
		BoardAndPosCache = null;
		MovableFieldCache.ForEach(x => Destroy(x));
		MovableFieldCache.Clear();
		phaseManager.CancelPiece();
	}
	void DisplayMovablePosition(BoardAndPos bp)
	{
		IPiece p = bp.board.GetPieceOnRayPosition(bp.pos);
		if (p == null || p.IsWhitePlayer != IsWhitePlayer)
			return;
		TargetPiece = p;
		var movablePosition = p.CheckMovement();
		if (movablePosition.Count == 0)
		{
			CancelPiece();
			return;
		}
		foreach (Vector2Int v in movablePosition)
			MovableFieldCache.Add(Instantiate(MovableFieldReference, bp.board.BoardSpaceToObjectSpace(v), Quaternion.identity));
		BoardAndPosCache = bp;
		MovablePos = movablePosition;
		phaseManager.NextPhase();
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
	public class BoardAndPos
	{
		public Board board;
		public Vector2Int PosOnBoard;
		public Vector3 pos;
	}
}
