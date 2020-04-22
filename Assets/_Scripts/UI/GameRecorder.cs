using UnityEngine;
using UnityEngine.UI;

public class GameRecorder : MonoBehaviour
{
	[SerializeField] Text text;
	string BasicRecord, SimpleRecord, FullRecord;
	void Start()
	{
		BasicRecord = "Prosfair Game Record" + System.Environment.NewLine;
		SimpleRecord = "Prosfair Game Record" + System.Environment.NewLine;
		FullRecord = "Prosfair Game Record" + System.Environment.NewLine;
	}
	void Update()
	{
		text.text = BasicRecord;
	}
	public void PieceMoveRecord(IPiece piece, Board board, Vector2Int pos)
	{
		string isWhite = piece.IsWhitePlayer ? "White" : "Black";
		WriteRecord("[" + isWhite + "]" + piece.pieceType + " " + pos + System.Environment.NewLine);
		WriteSimpleRecord(piece.IsWhitePlayer ? "W" : "B" + PieceUtils.PieceSimpleNotation(piece.pieceType));
	}
	public void FieldOathRecord(IOath oath)
	{

	}
	void WriteRecord(string s)
	{
		BasicRecord += s;
	}
	void WriteSimpleRecord(string s)
	{
		SimpleRecord += s;
	}
	void WriteFullRecord(string s)
	{
		FullRecord += s;
	}
}