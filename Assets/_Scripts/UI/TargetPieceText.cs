using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TargetPieceText : MonoBehaviour
{
	[SerializeField] PieceMover player;
	Text text;
	void Start()
	{
		text = GetComponent<Text>();
	}
	void Update()
	{
		if (player.TargetPiece != null)
			text.text = player.TargetPiece.pieceType.ToString();
	}
}