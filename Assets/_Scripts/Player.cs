using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public GameObject MovableFieldReference;
	public PlayerPhase phase;
	public IPiece TargetPiece;
	public bool IsWhitePlayer = false;
	public OathManager oathManager;
	List<GameObject> MovableFieldCache = new List<GameObject>();
	List<Vector2Int> MovablePos = new List<Vector2Int>();
	BoardAndPos BoardAndPosCache;
	void Start()
	{
		if (!IsWhitePlayer)
		{
			phase = PlayerPhase.OpponentTurn;
			enabled = false;
		}
	}
	void Update()
	{
		switch (phase)
		{
			case PlayerPhase.FirstOath: FirstOath(); break;
			case PlayerPhase.PieceSelect: PieceSelect(); break;
			case PlayerPhase.PieceSelected: PieceMove(); break;
			case PlayerPhase.SecondOath: SecondOath(); break;
			default: break;
		}
	}
	public void OathSkip()
	{
		if (this.enabled && phase == PlayerPhase.FirstOath || phase == PlayerPhase.SecondOath)
			NextPhase();
	}
	void FirstOath()
	{
	}
	void SecondOath()
	{
	}
	void PieceSelect()
	{
		if (!Input.GetMouseButtonDown(0))
			return;
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
			DisplayMovablePosition(rayPos);
	}
	void PieceMove()
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
			{
				rayPos.board.MovePiece(TargetPiece, BoardAndPosCache.PosOnBoard, boardPos);
				MovablePos.Clear();
				BoardAndPosCache = null;
				MovableFieldCache.ForEach(x => Destroy(x));
				MovableFieldCache.Clear();
				NextPhase();
			}
		}
		else
			CancelPiece();
	}
	void CancelPiece()
	{
		MovablePos.Clear();
		BoardAndPosCache = null;
		MovableFieldCache.ForEach(x => Destroy(x));
		MovableFieldCache.Clear();
		phase = PlayerPhase.PieceSelect;
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
		NextPhase();
	}
	public void NextPhase()
	{
		if (phase == PlayerPhase.OpponentTurn)
			phase = PlayerPhase.FirstOath;
		else
			phase += 1;
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
