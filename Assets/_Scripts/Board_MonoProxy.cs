using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Board_MonoProxy : MonoBehaviour
{
	public Board board { get; set; }
	public GameObject DisplayField;
	GameObject field;
	public Vector2Int ObjectSpaceToBoardSpace(Vector3 o_pos)
	{
		Vector3 v = (o_pos - transform.position + new Vector3(0.05f, 0, 0.05f)) / 0.1f;
		v = new Vector3(Mathf.Floor(v.x), 0, Mathf.Floor(v.z));
		Vector2Int vi = new Vector2Int((int)v.x + board.size / 2, (int)v.z + board.size / 2);
		return vi;
	}
	public Vector3 BoardSpaceToObjectSpace(Vector2Int b_pos)
	{
		Vector2Int vi = b_pos - Vector2Int.one * (board.size / 2);
		Vector3 v = new Vector3(vi.x * 0.1f, 0, vi.y * 0.1f);
		return v + transform.position;
	}
	public void DisplaySubBoardField(Board b, Vector2Int pos, int size)
	{
		field = Instantiate(DisplayField);
		Vector3 minPos = b.BoardSpaceToObjectSpace(pos);
		Vector3 maxPos = b.BoardSpaceToObjectSpace(pos + Vector2Int.one * size);
		field.transform.position = (minPos + maxPos) / 2;
		float XScale = Mathf.Abs(maxPos.x - minPos.x) + 0.1f, ZScale = Mathf.Abs(maxPos.z - minPos.z) + 0.1f;
		field.transform.localScale = new Vector3(XScale, field.transform.localScale.y, ZScale);
	}
	void OnDestroy()
	{
		Destroy(field);
	}
}