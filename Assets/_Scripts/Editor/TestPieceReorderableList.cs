using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(BoardManager_Test))]
public class TestPieceDrawer : Editor
{
	private ReorderableList RL;
	private SerializedProperty ListProp;
	private void OnEnable()
	{
		ListProp = serializedObject.FindProperty("testPieces");
		RL = new ReorderableList(serializedObject, ListProp);
		RL.elementHeight = 60;
		RL.drawHeaderCallback = (rect) =>
		{
			EditorGUI.LabelField(rect, "TestPieces");
		};
		RL.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			var element = ListProp.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField(rect, element);
		};
	}
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		serializedObject.Update();
		RL.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}
[CustomPropertyDrawer(typeof(TestPiecePos))]
public class TestPieceListAttribute : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		using (new EditorGUI.PropertyScope(position, label, property))
		{
			position.height = EditorGUIUtility.singleLineHeight;
			var ele1 = property.FindPropertyRelative("type");
			var ele2 = property.FindPropertyRelative("IsWhite");
			var ele3 = property.FindPropertyRelative("pos");
			var ele1Rect = new Rect(position)
			{
				height = position.height
			};
			var ele2Rect = new Rect(position)
			{
				height = position.height,
				y = ele1Rect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ele3Rect = new Rect(position)
			{
				height = position.height * 2,
				y = ele2Rect.y + EditorGUIUtility.singleLineHeight + 2
			};

			EditorGUI.PropertyField(ele1Rect, ele1);
			EditorGUI.PropertyField(ele2Rect, ele2);
			EditorGUI.PropertyField(ele3Rect, ele3);
		}
	}
}
#endif