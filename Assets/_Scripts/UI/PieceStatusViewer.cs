using UnityEngine;
using UnityEngine.UI;

public class PieceStatusViewer : MonoBehaviour
{
	[SerializeField] Text stage, experience;
	IPiece piece;
	void Start()
	{
		piece = transform.parent.GetComponent<IPiece>();
		transform.parent = GameObject.FindGameObjectWithTag("UICanvas").transform;

		foreach (Transform t in transform)
			if (piece.IsWhitePlayer)
				t.position = new Vector3(t.position.x, t.position.y, t.position.z);
			else
				t.position = new Vector3(t.position.x, -t.position.y, t.position.z);
	}
	void Update()
	{
		stage.text = piece.Stage.ToString();
		experience.text = piece.Experience.ToString();
		transform.position = Camera.main.WorldToScreenPoint(piece.PositionInWorld);
	}
}