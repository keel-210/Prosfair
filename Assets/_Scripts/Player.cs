using UnityEngine;

public class Player : MonoBehaviour
{
	public bool IsWhitePlayer = false;
	public PlayerPhase phase { get; set; }
	public PieceMover mover;
	public PieceStock stock;
	void Start()
	{
		if (!IsWhitePlayer)
			phase = PlayerPhase.OpponentTurn;
		enabled = IsWhitePlayer;
	}
	void Update()
	{
		if (phase == PlayerPhase.PieceSelect || phase == PlayerPhase.PieceSelected)
		{
			if (stock.stockUI.IsSelectingOtherThanDefault)
				PieceUtilExclusiveProcess(true);
			else
				PieceUtilExclusiveProcess(false);
		}
	}
	void PieceUtilExclusiveProcess(bool EnableStock)
	{
		stock.enabled = EnableStock;
		mover.enabled = !EnableStock;
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
