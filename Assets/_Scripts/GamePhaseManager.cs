using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
	public Player WhitePlayer, BlackPlayer;
	public bool IsWhitePlaying;
	void Start()
	{
		IsWhitePlaying = true;
		WhitePlayer.enabled = true;
		BlackPlayer.enabled = false;
	}
	void Update()
	{
		if (IsWhitePlaying && WhitePlayer.phase == PlayerPhase.OpponentTurn)
			ChangeTurn();
		else if (!IsWhitePlaying && BlackPlayer.phase == PlayerPhase.OpponentTurn)
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
}