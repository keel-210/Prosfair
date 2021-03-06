using UnityEngine;
using UnityEngine.UI;
public class BoardStatusChecker : MonoBehaviour
{
	public bool IsInitialized;
	[SerializeField] Text OccupiedPlayer, BoardAttribute, BoardTime;
	[SerializeField] GameObject obj;
	Board board;
	void Start()
	{
		if (obj.GetComponent<Board_MonoProxy>())
			board = obj.GetComponent<Board_MonoProxy>().board;
		else
			return;
		if (board == null)
			return;
		Vector3 pos = board.BoardSpaceToObjectSpace(new Vector2Int(board.size - 1, board.size - 1));
		pos = Camera.main.WorldToScreenPoint(pos);
		transform.position = pos + new Vector3(GetComponent<RectTransform>().rect.width, 0, 0) / 2;
		Vector3 Xoffset = new Vector3(0, 0, 0);
		Vector3 Yoffset = new Vector3(0, -OccupiedPlayer.GetComponent<RectTransform>().rect.height, 0);
		OccupiedPlayer.enabled = true;
		BoardAttribute.enabled = true;
		BoardTime.enabled = true;
		OccupiedPlayer.transform.localPosition = Xoffset;
		BoardAttribute.transform.localPosition = Xoffset + Yoffset;
		BoardTime.transform.localPosition = Xoffset + Yoffset * 2;
		IsInitialized = !IsInitialized;
	}
	void Update()
	{
		if (!IsInitialized)
			Start();
		else
		{
			OccupiedPlayer.text = board.OccupiedPlayer.ToString();
			BoardAttribute.text = board.attribute.ToString();
			BoardTime.text = board.boardTime.ToString();
		}
	}
}