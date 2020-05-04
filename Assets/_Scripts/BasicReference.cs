using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "MyScriptable/BasicReference")]
public class BasicReferrence : ScriptableObject
{
	public List<GameObject> PieceReferrence = new List<GameObject>();
}