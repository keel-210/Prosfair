using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
	//ここのPlayer隠蔽した方がいいと思う
	//OathSkipとかNextPhaseもここ経由の方がいいのではというか全部ここのPlayer経由してるしな
	//でもここ起点にするとPlayerのNextPhaseをpublicにしなきゃいけないじゃんそれだとここから以外でもアクセスできるじゃんそれ意味なくない？
	//PlayerFacadeみたいな扱い...?
	//PieceMoverになったことでphase管理の意味合いが薄くなりすぎ
	//改めてPlayerクラスが必要ではないか
	public PlayerPhase WhitePlayerPhase { get { return WhitePlayer.phase; } }
	public PlayerPhase BlackPlayerPhase { get { return BlackPlayer.phase; } }
	//こうかな...違うな...Playerの隠蔽という初期の目的死んでんじゃん
	//戦域放棄でStock要求されるのが痛い
	public PieceStock WhiteStock, BlackStock;
	public bool IsWhitePlaying;
	[SerializeField] PieceMover WhiteMover, BlackMover;
	[SerializeField] Player WhitePlayer, BlackPlayer;

	void Start()
	{
		IsWhitePlaying = true;
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
	public void NextPhase()
	{
		if (IsWhitePlaying)
			BlackPlayer.NextPhase();
		else
			WhitePlayer.NextPhase();
	}
	public void OathSkip()
	{
		if (IsWhitePlaying)
			BlackPlayer.OathSkip();
		else
			WhitePlayer.OathSkip();
	}
}