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
		stage.text = piece.Stage.ToString();
		experience.text = piece.Experience.ToString();
		if (!piece.IsWhitePlayer)
		{

			stage.transform.localPosition = new Vector3(stage.transform.localPosition.x, stage.transform.localPosition.y, -stage.transform.localPosition.z);
			experience.transform.localPosition = new Vector3(experience.transform.localPosition.x, experience.transform.localPosition.y, -experience.transform.localPosition.z);
		}
	}
}