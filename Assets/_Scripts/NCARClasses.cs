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

		[ARObjectList()]
		public string card01;
		public states relation;
		[ARObjectList()]
		public string card02;
	}

	//////////////////////////////////////////////////////////////////

	/// <summary>
	/// The conditions.
	/// </summary>
	[System.Serializable]
	[CanEditMultipleObjects]
	public class ARRule : ScriptableObject
	{
		public ARTargetConditionTemplate[] conditions;
		public string events;
	}

	[System.Serializable]
	public class ARTargetConditionTemplate
	{
		public int deligateIndex;
		public int target1Index;
		public int target2Index;
	}

	[System.Serializable]
	public class ARShowEventTemplate{
		[modelIndicesAttribute]
		public int[] modelIndices;
		public int targetIndex;
	}

	[System.Serializable]
	public class ARRuleTemplate{
		public ARTargetConditionTemplate[] pairWiseConditions;
		public ARShowEventTemplate[] showEvents; 
	}
		
	//test attr
	public class modelIndicesAttribute : PropertyAttribute {
		public modelIndicesAttribute(){

		}
	}

	//did not use for speed
	public delegate bool delCondition(GameObject target1, GameObject target2);


	/// <summary>
	/// this class includes all the targets, models, conditions and events.
	/// </summary>
	public static class ARappDB {

		public static string[] modelGuis;
		public static string[] modelNames;
		public static GameObject[] target;					//targets as game objects
		public static string[] targetNames; 				//Name of the target objects
		public static MethodInfo[] conditions;				//Methodsinfo of condition list specified in ARTargetCondition
		public static string[] conditionNames;				//List of condition function names;
		public static Func<GameObject, GameObject, bool>[] conditionDeligates;  //TargetCondition Deletages

		static ARappDB(){
			rebuild ();
		}

		/// <summary>
		/// rebuild ar database
		/// </summary>
		public static void rebuild(){
			//get all the model guis
			modelGuis = NcHelpers.FindAllARModels(isGuid:true);

			//get all the model names
			modelNames = NcHelpers.FindAllARModels(isGuid:false);

			//get all the target object
			target = NcHelpers.FindAllARTargets();

			//get all the target names
			targetNames = ARappDB.target.Select(p => p.gameObject.name).ToArray ();

			//get all the conditions as methodinfo
			conditions = typeof(ARTargetCondition).GetMethods(BindingFlags.Public | BindingFlags.Static);

			//get all the condition function names
			conditionNames = ARappDB.conditions.Select (p => p.Name).ToArray (); 

			//get all the conditiondeligates
			conditionDeligates = ARappDB.conditions.Select (p => (Func<GameObject, GameObject, bool>) System.Delegate.CreateDelegate (typeof(Func<GameObject, GameObject, bool>), null, p)).ToArray();
		}
	}

	[System.Serializable]
	public static class ARTargetCondition {
		public static bool IsPerpTo(GameObject target1, GameObject target2){
			Debug.Log ("c1 ran");
			return true;
		}
		public static bool IsleftTo(GameObject target1, GameObject target2){
			Debug.Log ("c2 ran");
			return true;
		}
		public static bool IsOnTopOf(GameObject target1, GameObject target2){
			Debug.Log ("c2 ran");
			return true;
		}
	}



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
			this.items = NcHelpers.FindAllARModels ();
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



