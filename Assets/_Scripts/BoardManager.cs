using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager : MonoBehaviour
{
	public Board mainBoard;
	public List<Board> subBoards;
	public List<GameObject> subBoardGameObjects = new List<GameObject>();
	[SerializeField] BasicReference basic;
	[SerializeField] Material WhitePiece, BlackPiece;
	protected virtual void Start()
	{
		Debug.Log("Init Board");
		mainBoard.size = 13;
		SetInitPiece(true);
		SetInitPiece(false);
	}
	protected void SetInitPiece(bool IsWhite)
	{
		mainBoard.InitializeBoard("M", BoardAttribute.Ignoria, BoardTime.Claint);
		int PosY = 0, Dir = 0;
		if (IsWhite)
		{ PosY = 2; Dir = -1; }
		else
		{ PosY = 10; Dir = 1; }
		for (int i = 0; i < 13; i++)
			SetPiece(mainBoard, PieceType.Gore, new Vector2Int(i, PosY), IsWhite);
		PosY += Dir;
		SetPiece(mainBoard, PieceType.Golcleo, new Vector2Int(3, PosY), IsWhite);
		SetPiece(mainBoard, PieceType.Shamain, new Vector2Int(5, PosY), IsWhite);
		SetPiece(mainBoard, PieceType.Shamain, new Vector2Int(7, PosY), IsWhite);
		SetPiece(mainBoard, PieceType.Golcleo, new Vector2Int(9, PosY), IsWhite);
		PosY += Dir;
		PieceType[] InitPiece = {PieceType.Laage,PieceType.Woofein,PieceType.Ozoa,PieceType.Cain,PieceType.Gisharl,PieceType.Mechet,
		PieceType.Bolussa,PieceType.Remagoguu,PieceType.Gisharl,PieceType.Cain,PieceType.Ozoa,PieceType.Woofein,PieceType.Laage};
		if (IsWhite)
			for (int i = 0; i < 13; i++)
				SetPiece(mainBoard, InitPiece[i], new Vector2Int(i, PosY), IsWhite);
		else
			for (int i = 0; i < 13; i++)
				SetPiece(mainBoard, InitPiece[InitPiece.Length - 1 - i], new Vector2Int(i, PosY), IsWhite);
	}
	protected void SetPiece(Board board, PieceType type, Vector2Int pos, bool IsWhite)
	{
		GameObject obj = Instantiate(basic.PieceReferrence[(int)type]);
		var renderers = obj.GetComponentsInChildren<Renderer>();
		if (IsWhite)
			foreach (Renderer r in renderers)
				r.material = WhitePiece;
		else
			foreach (Renderer r in renderers)
				r.material = BlackPiece;

		IPiece p = obj.GetComponent<IPiece>();
		obj.GetComponent<PieceBase>().IsWhitePlayer = IsWhite;
		board.AddPiece(p, pos);
	}
	public void AddSubBoard(OathUIData prepare, FieldCheck check)
	{
		Board b = subBoardGameObjects[0].AddComponent<Board>();
		subBoardGameObjects.RemoveAt(0);
		b.size = check.FieldSize;
		b.InitializeBoard("F" + subBoards.Count.ToString(), prepare.boardAttribute, prepare.boardTime);
		subBoards.Add(b);
		foreach (IPiece p in check.AllPieces)
			SetPiece(b, p.pieceType, p.PositionOnBoard - check.FieldPos, p.IsWhitePlayer);
		b.OccupationCheck();
	}
	public void AllFieldEnhance(List<PieceType> types, int enhanceStage)
	{
		foreach (PieceType t in types)
		{
			mainBoard.EnhancePieceType(t, enhanceStage);
			foreach (Board b in subBoards)
				b.EnhancePieceType(t, enhanceStage);
		}
	}
	public void AllPieceAttribute(List<PieceType> types, PieceAttribute attr)
	{
		foreach (PieceType t in types)
		{
			mainBoard.ChangePieceAttribute(t, attr);
			foreach (Board b in subBoards)
				b.ChangePieceAttribute(t, attr);
		}
	}
}
