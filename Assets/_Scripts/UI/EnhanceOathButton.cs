using UnityEngine;
using UnityEngine.UI;
public class EnhanceOathButton : OathButtonBase
{
	EnhanceOath enhanceOath;
	IPiece piece;
	void SetTargetPiece()
	{
		enhanceOath = (EnhanceOath)oath;
		enhanceOath.EnhanceTargetPiece = piece;
	}
}
