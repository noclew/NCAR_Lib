using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace noclew
{
	/// <summary>
	/// AR condition drawer, derived from PropertyDrawer
	/// </summary>
	[CustomPropertyDrawer (typeof(ARTargetConditionTemplate))]
	[CanEditMultipleObjects]
	public class ARTargetConditionDrawer: PropertyDrawer
	{
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label)
		{
			//refresh AR database
			ARappDB.rebuild ();

			//ArCondition의 property
			SerializedProperty target1 = prop.FindPropertyRelative ("target1Index");
			SerializedProperty deligateIndex = prop.FindPropertyRelative ("deligateIndex");
			SerializedProperty target2 = prop.FindPropertyRelative ("target2Index"); 

			//start property. this enables the manipulation of a label.
			label = EditorGUI.BeginProperty (pos, label, prop);

			//draw label and return the adjusted rect
			Rect posNew = EditorGUI.PrefixLabel (pos, new GUIContent ("Check If"));

			//save original indent 
			int indentOriginal = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			//Set the popup bar width
			float elemWidth = posNew.width * (1f / 3f);

			//define positions of popups
			Rect t1rect = new Rect (posNew.x, posNew.y, elemWidth, posNew.height);
			Rect relrect = new Rect (posNew.x + posNew.width - (elemWidth * 2f), posNew.y, elemWidth, posNew.height);
			Rect t2rect = new Rect (posNew.x + posNew.width - elemWidth, posNew.y, elemWidth, posNew.height);
			//
			//			//create popups
			target1.intValue = EditorGUI.Popup (t1rect, target1.intValue, ARappDB.targetNames);
			deligateIndex.intValue = EditorGUI.Popup (relrect, deligateIndex.intValue, ARappDB.conditionNames);
			target2.intValue = EditorGUI.Popup (t2rect, target2.intValue, ARappDB.targetNames);

			//end property
			EditorGUI.EndProperty ();

			//revert indentlevel
			EditorGUI.indentLevel = indentOriginal;
		}

	}

	/// <summary>
	/// AR event drawer.
	/// </summary>
	[CustomPropertyDrawer (typeof(ARShowEventTemplate))]
	[CanEditMultipleObjects]
	public class ARShowEventDrawer: PropertyDrawer
	{
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			SerializedProperty models = prop.FindPropertyRelative ("modelIndices");
			SerializedProperty modelArraySize = models.FindPropertyRelative ("Array.size"); //models.GetArrayElementAtIndex(i)
			//if the "models" prop is expanded
			if (!models.isExpanded)
				return 18 * 2;
			//else
			else
				return  (18f + ((modelArraySize.intValue + 2) * 18)); 
		}

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label)
		{
			//refresh AR database
			//			ARappDB.rebuild ();

			//ShowEvent의 property
			SerializedProperty models = prop.FindPropertyRelative ("modelIndices");
			SerializedProperty target = prop.FindPropertyRelative ("targetIndex");
			int modelArraySize = models.FindPropertyRelative ("Array.size").intValue; //models.GetArrayElementAtIndex(i)


			//EditorGUI.indentLevel = 0;
			//
			Rect posTargetLabel = pos;
			posTargetLabel.height = 18f; 
			Rect posTarget = EditorGUI.PrefixLabel (posTargetLabel, new GUIContent ("Show Models On the target"));
			posTarget.height = 18f;

			EditorGUI.indentLevel = 0;
			target.intValue = EditorGUI.Popup (posTarget, target.intValue, ARappDB.targetNames);

			Rect posModels = new Rect (posTarget.x, posTarget.y + 18f , posTarget.width, posTarget.height);
			EditorGUI.PropertyField (posModels, models, new GUIContent ("models"), true);
			//
			//


			//target.intValue = EditorGUI.Popup (posNew, target.intValue, ARappDB.targetNames);

		}

	}




	[CustomPropertyDrawer (typeof(modelIndicesAttribute))]
	public class myModelListDrawer : PropertyDrawer
	{
		// 현재 스콥에서 다루는 attribute 반환
		modelIndicesAttribute attr { get { return (modelIndicesAttribute)attribute; } }

		//GUI for property drawer. Calld by Unity
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label)
		{
			prop.intValue = EditorGUI.Popup (pos, prop.intValue, ARappDB.modelNames);
		}

	}
}
