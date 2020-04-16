using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public GameObject MovableFieldReference;
	public PlayerPhase phase;
	IPiece TargetPiece;
	List<GameObject> MovableFieldCache = new List<GameObject>();
	List<Vector2Int> MovablePos = new List<Vector2Int>();
	BoardAndPos BoardAndPosCache;
	void Update()
	{
		switch (phase)
		{
			case PlayerPhase.FirstOath: Oath(); break;
			case PlayerPhase.PieceSelect: PieceSelect(); break;
			case PlayerPhase.PieceSelected: PieceMove(); break;
			case PlayerPhase.SecondOath: Oath(); break;
		}
	}
	void Oath()
	{
		phase = PlayerPhase.PieceSelect;
	}
	void PieceSelect()
	{
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
			DisplayMovablePosition(rayPos);
	}
	void PieceMove()
	{
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
		{
			if (BoardAndPosCache.board != rayPos.board)
			{
				CancelPiece();
				return;
			}
			Vector2Int boardPos = rayPos.board.ObjectSpaceToBoardSpace(rayPos.pos);
			if (!MovablePos.Contains(boardPos))
			{
				CancelPiece();
				return;
			}
			rayPos.board.MovePiece(TargetPiece, BoardAndPosCache.PosOnBoard, boardPos);
			MovablePos.Clear();
			BoardAndPosCache = null;
			MovableFieldCache.ForEach(x => Destroy(x));
			MovableFieldCache.Clear();
			phase = PlayerPhase.SecondOath;
		}

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
		if (p == null)
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
		phase = PlayerPhase.PieceSelected;
	}
	BoardAndPos RayCastScreen()
	{
		BoardAndPos bp = new BoardAndPos();
		bp.pos = Vector3.zero;
		if (!Input.GetMouseButtonDown(0))
			return bp;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		LayerMask mask = 1 << 10;
		if (Physics.Raycast(ray, out hit, 100f, mask))
		{
			bp.board = hit.collider.GetComponent<Board>();
			Debug.Log(bp.board);
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
