using UnityEngine;
using UnityEngine.UI;
public class OathButton : MonoBehaviour
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
		DisplayOathRegion();
	}
	void DisplayOathRegion()
	{
		field = Instantiate(OathField);
		Vector3 minPos = oath.board.BoardSpaceToObjectSpace(oath.minRegion);
		Vector3 maxPos = oath.board.BoardSpaceToObjectSpace(oath.maxRegion);
		field.transform.position = (minPos + maxPos) / 2;
		float XScale = Mathf.Abs(maxPos.x - minPos.x) + 0.1f, ZScale = Mathf.Abs(maxPos.z - minPos.z) + 0.1f;
		field.transform.localScale = new Vector3(XScale, field.transform.localScale.y, ZScale);
	}
	public void OnClick()
	{
		buttons.manager.OathEffect(oath);
		button.onClick.RemoveListener(OnClick);
	}
}
