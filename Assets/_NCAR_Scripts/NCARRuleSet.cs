using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace noclew
{
	[CreateAssetMenu (fileName = "NCAR Rule Set", menuName = "NCAR/Create AR Rule Set", order = 1)]
	public class NCARRuleSet : ScriptableObject
	{
		public float angleThreshold = 20;
		public float distanceThreshold = 10;

		public NCARDefaultState[] defaultStates;
		public NCARRule[] rules;
	}
}