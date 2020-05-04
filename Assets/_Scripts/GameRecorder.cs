using UnityEngine;
public static class GameRecorder
{
	public static string BasicRecord, SimpleRecord, FullRecord;
	static GameRecorder()
	{
		BasicRecord = "Prosfair Game Record" + System.Environment.NewLine;
		SimpleRecord = "Prosfair Game Record" + System.Environment.NewLine;
		FullRecord = "Prosfair Game Record" + System.Environment.NewLine;
	}
	static public void PieceMoveRecord(IPiece piece, Board board, Vector2Int pos)
	{
		string isWhite = piece.IsWhitePlayer ? "White" : "Black";
		WriteRecord("[" + isWhite + "]" + board.name + " " + piece.pieceType + " " + pos + System.Environment.NewLine);
		WriteSimpleRecord(piece.IsWhitePlayer ? "W" : "B" + board.name + " " + PieceUtils.PieceSimpleNotation(piece.pieceType) + pos + System.Environment.NewLine);
		WriteFullRecord("[" + isWhite + "]" + board.name + " " + piece.pieceType + " " + pos + System.Environment.NewLine);
	}
	static public void FieldOathRecord(Oath oath, Board b)
	{
		WriteFullRecord(oath.IsWhitePlayer.ToString() + "Oath " + b.name + b.attribute + b.boardTime + System.Environment.NewLine);
	}
	static public void EnhanceOathRecord(Oath oath, Board b)
	{
		WriteFullRecord(oath.IsWhitePlayer.ToString() + "Oath " + b.name + b.attribute + b.boardTime + System.Environment.NewLine);
	}
	static void WriteRecord(string s)
	{
		BasicRecord += s;
	}
	static void WriteSimpleRecord(string s)
	{
		SimpleRecord += s;
	}
	static void WriteFullRecord(string s)
	{
		FullRecord += s;
	}
}