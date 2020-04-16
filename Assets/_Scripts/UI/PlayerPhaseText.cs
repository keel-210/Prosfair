using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerPhaseText : MonoBehaviour
{
	[SerializeField] Player player;
	Text text;
	void Start()
	{
		text = GetComponent<Text>();
	}
	void Update()
	{
		text.text = player.phase.ToString();
	}
}