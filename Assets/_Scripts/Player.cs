using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	void Update()
	{
		BoardAndPos rayPos = RayCastScreen();
		if (rayPos.pos != Vector3.zero)
			DisplayMovablePosition(rayPos);
	}
	void DisplayMovablePosition(BoardAndPos bp)
	{
		PieceBase p = bp.board.GetPieceOnTargetPosition(bp.board.ObjectSpaceToBoardSpace(bp.pos));
		var movablePosition = PieceUtils.CheckMovement(bp);
	}
	BoardAndPos RayCastScreen()
	{
		BoardAndPos bp = new BoardAndPos();
		bp.pos = Vector3.zero;
		if (!Input.GetMouseButtonDown(0))
			return bp;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			bp.board = hit.collider.GetComponent<Board>();
			bp.pos = hit.point;
		}

		return bp;
	}
	public class BoardAndPos
	{
		public Board board;
		public Vector3 pos;
	}
}
