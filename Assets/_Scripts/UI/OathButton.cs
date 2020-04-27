using UnityEngine;
using UnityEngine.UI;
public class OathButton : MonoBehaviour
{
	public IOath oath { get; set; }
	[SerializeField] GameObject OathField;
	[SerializeField] Material LineMaterial;
	OathButtons buttons;
	Button button;
	public GameObject field;
	LineRenderer line;
	void Start()
	{
		buttons = transform.parent.GetComponent<OathButtons>();
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
		DisplayOathRegion();
		DrawLine();
	}
	void Update()
	{
		Vector3 maxPos = oath.board.BoardSpaceToObjectSpace(oath.maxRegion) + new Vector3(0.05f, 0, 0.05f);
		Vector3 ButtonRect = new Vector3(GetComponent<RectTransform>().rect.width / 2, 0, 0);
		Vector3 ButtonPos = Camera.main.ScreenToWorldPoint(transform.position - ButtonRect);
		ButtonPos = new Vector3(ButtonPos.x, 0, ButtonPos.z);

		line.SetPositions(new Vector3[] { maxPos, ButtonPos });
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
	void DrawLine()
	{
		line = gameObject.AddComponent<LineRenderer>();
		line.startWidth = line.endWidth = 0.01f;
		line.material = LineMaterial;
	}
	public void OnClick()
	{
		Debug.Log("Oath");
		oath.OathEffect();
		button.onClick.RemoveListener(OnClick);
	}
	void OnDestroy()
	{
		Destroy(field);
	}
}
