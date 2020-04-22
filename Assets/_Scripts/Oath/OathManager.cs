using UnityEngine;
using System.Collections.Generic;
public class OathManager : MonoBehaviour
{
	[SerializeField] OathChecker checker;
	[SerializeField] GamePhaseManager phaseManager;
	[SerializeField] OathButtons buttons;
	public List<IOath> WhiteOaths = new List<IOath>(), BlackOaths = new List<IOath>();
	public List<IOath> PrevWhiteOaths = new List<IOath>(), PrevBlackOaths = new List<IOath>();
	public void CheckBoard()
	{
		buttons.Clear();
		checker.CheckAllBoard();
		buttons.LoadOaths(phaseManager.IsWhitePlaying);
	}
	public void OathEffect(IOath oath)
	{
		oath.OathEffect();
		if (oath.IsWhitePlayer)
		{
			WhiteOaths.Remove(oath);
			PrevWhiteOaths.Add(oath);
		}
		else
		{
			BlackOaths.Remove(oath);
			PrevBlackOaths.Add(oath);
		}
	}
}