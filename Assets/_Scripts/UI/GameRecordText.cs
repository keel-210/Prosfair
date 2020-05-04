using UnityEngine;
using UnityEngine.UI;

public class GameRecordText : MonoBehaviour
{
	[SerializeField] Text text;
	void Update()
	{
		text.text = GameRecorder.BasicRecord;
	}
}