using UnityEngine;
using UnityEngine.UI;
public class OathButton : MonoBehaviour
{
	public IOath oath { get; set; }
	OathButtons buttons;
	Button button;
	void Start()
	{
		buttons = transform.parent.GetComponent<OathButtons>();
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}
	public void OnClick()
	{
		buttons.manager.OathEffect(oath);
		button.onClick.RemoveListener(OnClick);
	}
}
