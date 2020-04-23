using UnityEngine;
using UnityEngine.UI;
public class FieldOathButton : MonoBehaviour
{
	public IOath oath { get; set; }
	[SerializeField] GameObject OathField;
	OathButtons buttons;
	Button button;
	GameObject field;
	void Start()
	{
		buttons = transform.parent.GetComponent<OathButtons>();
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}
	void DisplayOathRegion()
	{
		field = Instantiate(OathField);
		Vector3 minPos = oath.board.BoardSpaceToObjectSpace(oath.minRegion);
		Vector3 maxPos = oath.board.BoardSpaceToObjectSpace(oath.minRegion);
		field.transform.position = (minPos + maxPos) / 2;
		float XScale = 0.05f * (maxPos.x - minPos.x), YScale = 0.05f * (maxPos.y - minPos.y);
		transform.localScale = new Vector3(XScale, transform.localScale.y, YScale);
	}
	public void OnClick()
	{
		buttons.manager.OathEffect(oath);
		button.onClick.RemoveListener(OnClick);
	}
}
