using UnityEngine;
using TMPro;
public class PieceCharacterInitializer : MonoBehaviour
{
	[SerializeField] Material White, Black;
	void Start()
	{
		string s = PieceUtils.PieceSimpleNotation(transform.parent.GetComponent<IPiece>().pieceType);
		GetComponent<TextMeshPro>().text = s;
		if (transform.parent.GetComponent<IPiece>().IsWhitePlayer)
		{
			GetComponent<TextMeshPro>().color = Color.white;
			GetComponent<MeshRenderer>().material = White;
		}
		else
		{
			GetComponent<TextMeshPro>().color = Color.black;
			GetComponent<MeshRenderer>().material = Black;
		}
	}
}