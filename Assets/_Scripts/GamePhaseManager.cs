using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
	//PlayerFacadeみたいな扱い...?
	public PlayerPhase WhitePlayerPhase { get { return WhitePlayer.phase; } }
	public PlayerPhase BlackPlayerPhase { get { return BlackPlayer.phase; } }
	public bool IsWhitePlaying;
	public Player WhitePlayer, BlackPlayer;
	void Start()
	{
		IsWhitePlaying = true;
	}
	void Update()
	{
		if (Phase() == PlayerPhase.OpponentTurn)
			ChangeTurn();
	}
	void ChangeTurn()
	{
		WhitePlayer.enabled = !WhitePlayer.enabled;
		BlackPlayer.enabled = !BlackPlayer.enabled;
		if (IsWhitePlaying)
			BlackPlayer.NextPhase();
		else
			WhitePlayer.NextPhase();
		IsWhitePlaying = !IsWhitePlaying;
	}
	public void NextPhase()
	{
		if (IsWhitePlaying)
			WhitePlayer.NextPhase();
		else
			BlackPlayer.NextPhase();
	}
	public void GoToSeceondOath()
	{
		NextPhase();
		if (Phase() == PlayerPhase.PieceSelected)
			NextPhase();
	}
	public void CancelPiece()
	{
		if (IsWhitePlaying)
			WhitePlayer.CancelPieceMove();
		else
			BlackPlayer.CancelPieceMove();
	}
	public PlayerPhase Phase()
	{
		return (IsWhitePlaying ? WhitePlayerPhase : BlackPlayerPhase);
	}
	public void OathSkip()
	{
		BlackPlayer.OathSkip();
		WhitePlayer.OathSkip();
	}
}