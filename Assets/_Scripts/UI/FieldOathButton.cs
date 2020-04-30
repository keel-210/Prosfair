using UnityEngine;
using UnityEngine.UI;
public class FieldOathButton : OathButtonBase
{
	public FieldOath fieldOath { get; set; }
	public BoardAttribute boardAttribute { get; set; }
	public BoardTime boardTime { get; set; }
	void SetFieldOath()
	{
		fieldOath = (FieldOath)oath;
		fieldOath.boardAttribute = boardAttribute;
		fieldOath.boardTime = boardTime;
	}
}
