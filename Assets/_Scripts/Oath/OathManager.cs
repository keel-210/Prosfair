using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class OathManager : MonoBehaviour
{
	[SerializeField] public GamePhaseManager phaseManager;
	public List<Oath> WhiteOaths = new List<Oath>(), BlackOaths = new List<Oath>();
	public List<Oath> PrevWhiteOaths = new List<Oath>(), PrevBlackOaths = new List<Oath>();
	[SerializeField] OathChecker checker;
	[SerializeField] OathButtons buttons;
	bool IsChecked;
	void Update()
	{
		if (!IsChecked)
		{
			if (phaseManager.IsWhitePlaying && (phaseManager.WhitePlayer.phase == PlayerPhase.FirstOath || phaseManager.WhitePlayer.phase == PlayerPhase.SecondOath))
				CheckBoard();
			if (!phaseManager.IsWhitePlaying && (phaseManager.BlackPlayer.phase == PlayerPhase.FirstOath || phaseManager.BlackPlayer.phase == PlayerPhase.SecondOath))
				CheckBoard();
		}
	}
	public void CheckBoard()
	{
		IsChecked = true;
		WhiteOaths.Clear();
		BlackOaths.Clear();
		buttons.Clear();

		WhiteOaths = checker.CheckOaths(true);
		BlackOaths = checker.CheckOaths(false);
		WhiteOaths.ForEach(x => x.OnEffectActivated.AddListener(OathRemove));
		BlackOaths.ForEach(x => x.OnEffectActivated.AddListener(OathRemove));

		buttons.LoadOaths(phaseManager.IsWhitePlaying);
	}
	public void ResetCheckStatus()
	{
		IsChecked = false;
	}
	void OathRemove(Oath oath)
	{
		Debug.Log(oath.IsWhitePlayer);
		(oath.IsWhitePlayer ? WhiteOaths : BlackOaths).Remove(oath);
		(oath.IsWhitePlayer ? PrevWhiteOaths : PrevBlackOaths).Add(oath);
	}
}