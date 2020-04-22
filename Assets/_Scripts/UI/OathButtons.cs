using UnityEngine;
using System.Collections.Generic;
public class OathButtons : MonoBehaviour
{
	public OathManager manager;
	public GameObject button, subButton;
	public void LoadOaths(bool IsWhitePlaying)
	{
		List<IOath> oaths = IsWhitePlaying ? manager.WhiteOaths : manager.BlackOaths;
		foreach (IOath o in oaths)
		{
			GameObject obj = Instantiate(button, transform.position, Quaternion.identity, transform);
			obj.GetComponent<OathButton>().oath = o;
		}
	}
	public void Clear()
	{
		foreach (Transform t in transform)
			Destroy(t.gameObject);
	}
}