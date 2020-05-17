using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OathAndStockUIManager : MonoBehaviour
{
	[SerializeField] DrawerUI Oath, Stock;
	[SerializeField] List<PlayerPhase> OathPhase, PiecePhase;
	GamePhaseManager phaseManager;
	bool IsAppearance;
	void Start()
	{
		phaseManager = FindObjectOfType<GamePhaseManager>();
	}
	void Update()
	{
		if ((PhaseComparator(phaseManager.WhitePlayerPhase, OathPhase) || PhaseComparator(phaseManager.BlackPlayerPhase, OathPhase)))
			Oath.AppearanceRect();
		else
			Oath.DisappearanceRect();
		if ((PhaseComparator(phaseManager.WhitePlayerPhase, PiecePhase) || PhaseComparator(phaseManager.BlackPlayerPhase, PiecePhase)))
			Stock.AppearanceRect();
		else
			Stock.DisappearanceRect();
	}
	bool PhaseComparator(PlayerPhase target, List<PlayerPhase> phase)
	{
		foreach (PlayerPhase p in phase)
			if (p == target)
				return true;
		return false;
	}
}