using System.Linq;
using System.Collections.Generic;
public class FieldOath : Oath
{
	public FieldOath(BoardManager _manager, Board b, List<IPiece> l, bool IsWhite) : base(_manager, b, l, IsWhite) { }
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }
	public List<IPiece> AllPieces { get; set; }
	public override void OathEffect(OathPrepare prepare)
	{
		OnEffectActivated.Invoke(this);

		Board b = new Board();
		manager.AddSubBoard(b, prepare.boardAttribute, prepare.boardTime);
		EnhancePieces(prepare);
		ChangePieceAttribute(prepare);
	}
	void EnhancePieces(OathPrepare prepare)
	{
		var targetPieces = OathUtils.TargetPieceTypebyBoardTime(prepare.boardTime);
		manager.AllFieldEnhance(targetPieces, 1);
		if (prepare.boardAttribute == BoardAttribute.Vettoria)
			manager.AllFieldEnhance(OathUtils.InversionTargetPieceTypebyBoardTime(prepare.boardTime), -1);
		if (prepare.boardAttribute == BoardAttribute.Ignoria)
			manager.AllFieldEnhance(OathUtils.InversionTargetPieceTypebyBoardTime(prepare.boardTime), 1);
	}
	void ChangePieceAttribute(OathPrepare prepare)
	{
		var targetPieces = OathUtils.TargetPieceTypebyBoardTime(prepare.boardTime);
		manager.AllPieceAttribute(targetPieces, OathUtils.PieceTypebyBoardAttribute(prepare.boardAttribute));
	}
}