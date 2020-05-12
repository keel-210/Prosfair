using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PieceStatusViewer : MonoBehaviour
{
	[SerializeField] TextMeshPro stage, experience;
	IPiece piece;
	void Start()
	{
		piece = transform.parent.GetComponent<IPiece>();
	}
	void Update()
	{
		stage.text = piece.Stage.ToString();
		experience.text = piece.Experience.ToString();
	}
}