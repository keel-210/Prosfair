using UnityEngine;
using System.Collections.Generic;
public class OathManager : MonoBehaviour
{
	[SerializeField] OathChecker checker;
	[SerializeField] GamePhaseManager phaseManager;
	[SerializeField] OathButtons buttons;
	public List<Oath> WhiteOaths = new List<Oath>(), BlackOaths = new List<Oath>();
	public List<Oath> PrevWhiteOaths = new List<Oath>(), PrevBlackOaths = new List<Oath>();
	public bool IsChecked;
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

		checker.CheckAllBoard();
		foreach (Oath o in WhiteOaths)
			o.OnEffectActivated.AddListener(OathRemove);
		foreach (Oath o in BlackOaths)
			o.OnEffectActivated.AddListener(OathRemove);

		buttons.LoadOaths(phaseManager.IsWhitePlaying);
	}
	public void ResetCheckStatus()
	{
		IsChecked = false;
	}
	public void OathEffect(Oath oath, OathPrepare prepare)
	{
		oath.OathEffect(prepare);
	}
	void OathRemove(Oath oath)
	{
		(oath.IsWhitePlayer ? WhiteOaths : BlackOaths).Remove(oath);
		(oath.IsWhitePlayer ? PrevWhiteOaths : PrevBlackOaths).Add(oath);
	}
}