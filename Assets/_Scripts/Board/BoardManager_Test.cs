using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager_Test : BoardManager
{
	public List<TestPiecePos> testPieces = new List<TestPiecePos>();
	protected override void Start()
	{
		mainBoard = new Board();
		mainBoard.InitializeBoard("M", 13, BoardAttribute.Ignoria, BoardTime.Claint, mainBoardGameObject.GetComponent<Board_MonoProxy>());
		foreach (TestPiecePos p in testPieces)
			SetPiece(mainBoard, p.type, p.pos, p.IsWhite);
	}
}
[System.Serializable]
public class TestPiecePos
{
	public PieceType type;
	public bool IsWhite;
	public Vector2Int pos;
}