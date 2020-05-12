using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Board : MonoBehaviour
{
	public string name { get; set; }
	public bool IsOccupied { get; set; }
	public BoardOccupation OccupiedPlayer { get; set; }
	public int size, height;
	public IPiece[,] pieces;
	public BoardAttribute attribute;
	public BoardTime boardTime;
	public void InitializeBoard(string _name, BoardAttribute _attribute, BoardTime _boardTime)
	{
		if (pieces == null)
			pieces = new IPiece[size, size];
		name = _name;
		OccupiedPlayer = BoardOccupation.NonOccupied;
		attribute = _attribute;
		boardTime = _boardTime;
	}
	public void AddPiece(IPiece piece, Vector2Int Pos)
	{
		piece.board = this;
		piece.PositionOnBoard = Pos;
		pieces[Pos.x, Pos.y] = piece;
		piece.Move(BoardSpaceToObjectSpace(Pos));
	}
	public void MovePiece(IPiece piece, Vector2Int nowPos, Vector2Int targetPos)
	{
		pieces[nowPos.x, nowPos.y] = null;
		if (pieces[targetPos.x, targetPos.y] != null)
			pieces[targetPos.x, targetPos.y].KillSelf();
		piece.PositionOnBoard = targetPos;
		pieces[targetPos.x, targetPos.y] = piece;
		piece.Move(BoardSpaceToObjectSpace(targetPos));
		GameRecorder.PieceMoveRecord(piece, this, piece.PositionOnBoard);
		OccupationCheck();
	}
	public void EnhancePieceType(PieceType enhanceType, int enhanceStage)
	{
		if (attribute == BoardAttribute.Vettoria)
			return;
		foreach (IPiece p in pieces)
			if (p != null && p.pieceType == enhanceType)
				p.Stage += enhanceStage;
	}
	public void ExpPieceType(PieceType enhanceType, int enhanceStage)
	{
		foreach (IPiece p in pieces)
			if (p != null && p.pieceType == enhanceType)
				p.Experience += enhanceStage;
	}
	public void ChangePieceAttribute(PieceType enhanceType, PieceAttribute attr)
	{
		foreach (IPiece p in pieces)
			if (p != null && p.pieceType == enhanceType)
				p.pieceAttribute = attr;
	}
	public Vector2Int ObjectSpaceToBoardSpace(Vector3 o_pos)
	{
		Vector3 v = (o_pos - transform.position + new Vector3(0.05f, 0, 0.05f)) / 0.1f;
		v = new Vector3(Mathf.Floor(v.x), 0, Mathf.Floor(v.z));
		Vector2Int vi = new Vector2Int((int)v.x + size / 2, (int)v.z + size / 2);
		return vi;
	}
	public Vector3 BoardSpaceToObjectSpace(Vector2Int b_pos)
	{
		Vector2Int vi = b_pos - Vector2Int.one * (size / 2);
		Vector3 v = new Vector3(vi.x * 0.1f, 0, vi.y * 0.1f);
		return v + transform.position;
	}
	public IPiece GetPieceOnRayPosition(Vector3 pos)
	{
		Vector2Int p = ObjectSpaceToBoardSpace(pos);
		return pieces[p.x, p.y];
	}
	public void OccupationCheck()
	{
		bool WhiteOccupiedCheck = true, BlackOccupiedCheck = true;
		foreach (IPiece p in pieces)
		{
			if (p == null)
				continue;
			if (p.IsWhitePlayer && p.CheckMovement().Count() != 0)
				WhiteOccupiedCheck = false;
			if (!p.IsWhitePlayer && p.CheckMovement().Count() != 0)
				BlackOccupiedCheck = false;
		}
		if (WhiteOccupiedCheck && BlackOccupiedCheck)
			OccupiedPlayer = BoardOccupation.BothPlayer;
		else if (WhiteOccupiedCheck)
			OccupiedPlayer = BoardOccupation.WhitePlayer;
		else if (BlackOccupiedCheck)
			OccupiedPlayer = BoardOccupation.BlackPlayer;
		else
			OccupiedPlayer = BoardOccupation.NonOccupied;
	}
}