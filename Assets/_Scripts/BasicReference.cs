using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "MyScriptable/BasicReference")]
public class BasicReference : ScriptableObject
{
	public List<GameObject> PieceReferrence = new List<GameObject>();
	public List<GameObject> FieldReference = new List<GameObject>();
}