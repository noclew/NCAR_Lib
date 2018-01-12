using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace noclew
{

	/// <summary>
	/// The conditions.
	/// </summary>

	[System.Serializable]
	public class ARTargetPairwiseCondition
	{
		public int deligateIndex;
		public string target1Name;
		public string target2Name;
	}

	[System.Serializable]
	public class NCARShowEvent
	{
		[ModelNameAttribute]
		public string[] modelNames;
		public string targetName;
	}

	[System.Serializable]
	public class NCARHideEvent
	{
		[ModelNameAttribute]
		public string modelName;
	}

	[System.Serializable]
	public class NCARRule
	{
		public ARTargetPairwiseCondition[] pairWiseConditions;
		public NCARShowEvent[] showEvents;
		public NCARHideEvent[] hideEvents;
	}

	[System.Serializable]
	public class NCARDefaultState
	{
		[ModelNameAttribute]
		public string ModelName;
		[TargetNameAttribute]
		public string TargetName;	
	}


	//attribute to draw models
	public class ModelNameAttribute : PropertyAttribute
	{
		public ModelNameAttribute ()
		{

		}
	}
	//attribute to draw models
	public class TargetNameAttribute : PropertyAttribute
	{
		public TargetNameAttribute ()
		{

		}
	}
}



