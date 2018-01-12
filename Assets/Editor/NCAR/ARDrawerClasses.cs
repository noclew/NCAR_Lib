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
		//		public override void OnGUI_original (Rect pos, SerializedProperty prop, GUIContent label)
		//		{
		//			//refresh AR database
		//			NCARappDB.Rebuild ();
		//
		//			//ArCondition의 property
		//			SerializedProperty target1 = prop.FindPropertyRelative ("target1Index");
		//			SerializedProperty deligateIndex = prop.FindPropertyRelative ("deligateIndex");
		//			SerializedProperty target2 = prop.FindPropertyRelative ("target2Index");
		//
		//			//start property. this enables the manipulation of a label.
		//			label = EditorGUI.BeginProperty (pos, label, prop);
		//
		//			//draw label and return the adjusted rect
		//			Rect posNew = EditorGUI.PrefixLabel (pos, new GUIContent ("Check If"));
		//
		//			//save original indent
		//			int indentOriginal = EditorGUI.indentLevel;
		//			EditorGUI.indentLevel = 0;
		//
		//			//Set the popup bar width
		//			float elemWidth = posNew.width * (1f / 3f);
		//
		//			//define positions of popups
		//			Rect t1rect = new Rect (posNew.x, posNew.y, elemWidth, posNew.height);
		//			Rect relrect = new Rect (posNew.x + posNew.width - (elemWidth * 2f), posNew.y, elemWidth, posNew.height);
		//			Rect t2rect = new Rect (posNew.x + posNew.width - elemWidth, posNew.y, elemWidth, posNew.height);
		//			//
		//			//			//create popups
		//			target1.intValue = EditorGUI.Popup (t1rect, target1.intValue, NCARappDB.targetNames);
		//			deligateIndex.intValue = EditorGUI.Popup (relrect, deligateIndex.intValue, NCARappDB.conditionNames);
		//			target2.intValue = EditorGUI.Popup (t2rect, target2.intValue, NCARappDB.targetNames);
		//
		//			//end property
		//			EditorGUI.EndProperty ();
		//
		//			//revert indentlevel
		//			EditorGUI.indentLevel = indentOriginal;
		//		}

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label)
		{
			//refresh AR database
			NCARappDB.Rebuild ();

			//ArCondition의 property
			SerializedProperty target1 = prop.FindPropertyRelative ("target1Name");
			SerializedProperty deligateIndex = prop.FindPropertyRelative ("deligateIndex");
			SerializedProperty target2 = prop.FindPropertyRelative ("target2Name"); 

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

			//create popups
			//target1
			int target1_index_old = System.Array.IndexOf (NCARappDB.targetNames, target1.stringValue.ToString());
			if (target1_index_old == -1) {
				Debug.Log (" Image Targets are not properly set. Target1 is set to the first target");
				target1_index_old = 0;
			}
			int target1_index_new = EditorGUI.Popup (t1rect, target1_index_old, NCARappDB.targetNames); 
			target1.stringValue =  NCARappDB.targetNames [target1_index_new] ;

			//deligate
			deligateIndex.intValue = EditorGUI.Popup (relrect, deligateIndex.intValue, NCARappDB.conditionNames);

			//target2 as GUID
			int target2_index_old = System.Array.IndexOf (NCARappDB.targetNames, target2.stringValue.ToString());
			if (target2_index_old == -1)
				Debug.Log (" Image Targets are not properly set. Target1 is set to the first target");
				target2_index_old = 0;
			int target2_index_new = EditorGUI.Popup (t2rect, target2_index_old, NCARappDB.targetNames); 
			target2.stringValue = NCARappDB.targetNames [target2_index_new];


			//end property
			EditorGUI.EndProperty ();

			//revert indentlevel
			EditorGUI.indentLevel = indentOriginal;
		}

	}

	/// <summary>
	/// AR event drawer.
	/// </summary>
	[CustomPropertyDrawer (typeof(NCARShowEventTemplate))]
	[CanEditMultipleObjects]
	public class ARShowEventDrawer: PropertyDrawer
	{
		public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
		{
			SerializedProperty models = prop.FindPropertyRelative ("modelNames");
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
			//			NCARappDB.rebuild ();

			//ShowEvent의 property
			SerializedProperty models = prop.FindPropertyRelative ("modelNames");
			SerializedProperty target = prop.FindPropertyRelative ("targetName");
			int modelArraySize = models.FindPropertyRelative ("Array.size").intValue; //models.GetArrayElementAtIndex(i)


			//EditorGUI.indentLevel = 0;
			//
			Rect posTargetLabel = pos;
			posTargetLabel.height = 18f; 
			Rect posTarget = EditorGUI.PrefixLabel (posTargetLabel, new GUIContent ("Show Models On"));
			posTarget.height = 18f;

			EditorGUI.indentLevel = 0;
			int idx_target_old = System.Array.IndexOf (NCARappDB.targetNames, target.stringValue.ToString ());
			if (idx_target_old == -1)
				idx_target_old = 0;
			int idx_target_new = EditorGUI.Popup (posTarget, idx_target_old, NCARappDB.targetNames);
			target.stringValue = NCARappDB.targetNames [idx_target_new];
			  
			Rect posModels = new Rect (posTarget.x, posTarget.y + 18f, posTarget.width, posTarget.height);
			EditorGUI.PropertyField (posModels, models, new GUIContent ("models"), true);
			//
			//


			//target.intValue = EditorGUI.Popup (posNew, target.intValue, NCARappDB.targetNames);

		}

	}




	[CustomPropertyDrawer (typeof(ModelNameAttribute))]
	public class myModelListDrawer : PropertyDrawer
	{
		// 현재 스콥에서 다루는 attribute 반환
		ModelNameAttribute attr { get { return (ModelNameAttribute)attribute; } }

		//GUI for property drawer. Calld by Unity
		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label)
		{
			int idx_old = System.Array.IndexOf (NCARappDB.sceneModelNames, prop.stringValue.ToString ());
			if (idx_old == -1)
				idx_old = 0;
			int idx_new = EditorGUI.Popup (pos, idx_old, NCARappDB.sceneModelNames);
			prop.stringValue = NCARappDB.sceneModelNames [idx_new];
		}

	}
}
