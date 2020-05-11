using UnityEngine;
using UnityEngine.UI;
public class BoardStatusChecker : MonoBehaviour
{
	public Board board;
	[SerializeField] Text OccupiedPlayer, BoardAttribute, BoardTime;
	[SerializeField] GameObject obj;
	public bool IsInitialized;
	void Start()
	{
		if (board == null || IsInitialized)
			return;
		IsInitialized = !IsInitialized;
		Debug.Log("Init");
		Vector3 pos = board.BoardSpaceToObjectSpace(new Vector2Int(board.size - 1, board.size - 1));
		pos = Camera.main.WorldToScreenPoint(pos);
		Vector3 Xoffset = new Vector3(70, 0, 0);
		Vector3 Yoffset = new Vector3(0, -OccupiedPlayer.GetComponent<RectTransform>().rect.height, 0);
		OccupiedPlayer.enabled = true;
		BoardAttribute.enabled = true;
		BoardTime.enabled = true;
		OccupiedPlayer.transform.position = pos + Xoffset;
		BoardAttribute.transform.position = pos + Xoffset + Yoffset;
		BoardTime.transform.position = pos + Xoffset + Yoffset * 2;
	}
	void Update()
	{
		if (board == null && (board = obj.GetComponent<Board>()) == null)
			return;
		else
			Start();
		OccupiedPlayer.text = board.OccupiedPlayer.ToString();
		BoardAttribute.text = board.attribute.ToString();
		BoardTime.text = board.boardTime.ToString();
	}
}