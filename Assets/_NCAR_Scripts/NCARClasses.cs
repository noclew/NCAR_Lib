using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace noclew
{
	

	[CreateAssetMenu (menuName = "refresh AR database")]
	public class NCARClasses : ScriptableObject
	{
		public enum states
		{
			qe,
			rw,
			rr
		}

		[ARObjectList ()]
		public string card01;
		public states relation;
		[ARObjectList ()]
		public string card02;
	}

	//////////////////////////////////////////////////////////////////

	/// <summary>
	/// The conditions.
	/// </summary>

	[System.Serializable]
	public class ARTargetConditionTemplate
	{
		public int deligateIndex;
		public string target1Name;
		public string target2Name;
	}

	[System.Serializable]
	public class NCARShowEventTemplate
	{
		[ModelNameAttribute]
		public string[] modelNames;
		public string targetName;
	}

	[System.Serializable]
	public class NCARHideEventTemplate
	{
		//[ModelNameAttribute]
		public string targetName;
	}

	[System.Serializable]
	public class ARRuleTemplate
	{
		public ARTargetConditionTemplate[] pairWiseConditions;
		public NCARShowEventTemplate[] showEvents;
		public NCARHideEventTemplate[] hideEvents;
	}

	[System.Serializable]
	public class ARDefaultStateTemplate
	{
		public string ModelName;
		public string TargetName;	
	}
	//test attr
	public class ModelNameAttribute : PropertyAttribute
	{
		public ModelNameAttribute ()
		{

		}
	}

	//did not use for speed
	public delegate bool delCondition (GameObject target1, GameObject target2);


	/// <summary>
	/// this class includes all the targets, models, conditions and events.
	/// </summary>

		



	///////////////
	/// 
	/// 
	/// no need
	/// 
	/// 
	/// 


	public class ARObjectList : PropertyAttribute
	{
		/// <summary>
		/// Attribute to display custom list
		/// </summary>
		public readonly string[] items;
		public int selected = 0;

		public ARObjectList (string[] sList)
		{
			this.items = sList;
		}

		public ARObjectList ()
		{
			Debug.Log ("Dd");
			this.items = NcHelpers.FindAllARAssetModels ();
		}
	}

	[CustomPropertyDrawer (typeof(ARObjectList))]
	public class ARObjectListDrawer : PropertyDrawer
	{

		ARObjectList attr{ get { return (ARObjectList)attribute; } }

		public override void OnGUI (Rect position, SerializedProperty prop, GUIContent label)
		{

			attr.selected = EditorGUI.Popup (EditorGUI.PrefixLabel (position, label), attr.selected, attr.items);
			prop.stringValue = attr.items [attr.selected];
		}
	}
}



