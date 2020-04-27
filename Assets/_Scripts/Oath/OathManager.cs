using UnityEngine;
using System.Collections.Generic;
public class OathManager : MonoBehaviour
{
	[SerializeField] OathChecker checker;
	[SerializeField] GamePhaseManager phaseManager;
	[SerializeField] OathButtons buttons;
	public List<IOath> WhiteOaths = new List<IOath>(), BlackOaths = new List<IOath>();
	public List<IOath> PrevWhiteOaths = new List<IOath>(), PrevBlackOaths = new List<IOath>();
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
		foreach (IOath o in WhiteOaths)
			o.OnEffectActivated.AddListener(OathRemove);
		foreach (IOath o in BlackOaths)
			o.OnEffectActivated.AddListener(OathRemove);

		buttons.LoadOaths(phaseManager.IsWhitePlaying);
	}
	public void ResetCheckStatus()
	{
		IsChecked = false;
	}
	public void OathEffect(IOath oath)
	{
		oath.OathEffect();
	}
	void OathRemove(IOath oath)
	{
		(oath.IsWhitePlayer ? WhiteOaths : BlackOaths).Remove(oath);
		(oath.IsWhitePlayer ? PrevWhiteOaths : PrevBlackOaths).Add(oath);
	}
}