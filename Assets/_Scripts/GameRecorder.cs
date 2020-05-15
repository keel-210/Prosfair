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
		WriteRecord("[" + isWhite + "]" + board.name + " " + piece.pieceType + " " + pos);
		WriteSimpleRecord(piece.IsWhitePlayer ? "W" : "B" + board.name + " " + PieceUtils.PieceSimpleNotation(piece.pieceType) + pos);
		WriteFullRecord("[" + isWhite + "]" + board.name + " " + piece.pieceType + " " + pos);
	}
	static public void FieldOathRecord(Board b)
	{
		WriteFullRecord("Day Carlio " + b.name + " " + b.attribute + " " + b.boardTime);
		WriteRecord("Day Carlio " + b.name + " " + b.attribute + " " + b.boardTime);
		WriteSimpleRecord("DC " + b.name + " " + SimpleBoardAttributeAndTime(b.attribute, b.boardTime));
	}
	static public void EnhanceOathRecord(Oath oath, Board b)
	{
		WriteFullRecord(oath.IsWhitePlayer.ToString() + "Oath " + b.name + b.attribute + b.boardTime);
	}
	static public void FieldAbandonmentOathRecord(PieceType type, Vector2Int pos)
	{
		WriteFullRecord("El Arlio " + type + " " + pos);
		WriteRecord("El Arlio " + type + " " + pos);
		WriteSimpleRecord("EA " + PieceUtils.PieceSimpleNotation(type) + " " + pos);
	}
	static void WriteRecord(string s)
	{
		BasicRecord += s + System.Environment.NewLine;
	}
	static void WriteSimpleRecord(string s)
	{
		SimpleRecord += s + System.Environment.NewLine;
	}
	static void WriteFullRecord(string s)
	{
		FullRecord += s + System.Environment.NewLine;
	}
	static string SimpleBoardAttributeAndTime(BoardAttribute attr, BoardTime t)
	{
		string s = "";
		switch (attr)
		{
			case BoardAttribute.Vettoria: s += "V"; break;
			case BoardAttribute.Ignoria: s += "I"; break;
			case BoardAttribute.Donamia: s += "D"; break;
		}
		s += ":";
		switch (t)
		{
			case BoardTime.Pasusu: s += "P"; break;
			case BoardTime.Claint: s += "C"; break;
			case BoardTime.Ftuule: s += "F"; break;
		}
		return s;
	}
}