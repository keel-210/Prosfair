using UnityEngine;

public class Player : MonoBehaviour
{
	public bool IsWhitePlayer = false;
	public PlayerPhase phase { get; set; }
	void Start()
	{
		if (!IsWhitePlayer)
			phase = PlayerPhase.OpponentTurn;
		enabled = IsWhitePlayer;
	}
	public void NextPhase()
	{
		if (phase == PlayerPhase.OpponentTurn)
			phase = PlayerPhase.FirstOath;
		else
			phase += 1;
	}
	public void CancelPieceMove()
	{
		phase = PlayerPhase.PieceSelect;
	}
	public void OathSkip()
	{
		if (this.enabled && phase == PlayerPhase.FirstOath || phase == PlayerPhase.SecondOath)
			NextPhase();
	}
}
