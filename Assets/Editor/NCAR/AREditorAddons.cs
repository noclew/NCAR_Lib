using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace noclew
{
	public class AREditorAddons
	{

		[MenuItem ("Noclew/Find All AR model Prefab")]
		public static void FindAllNCARLabedPrefabs ()
		{		
			string[] prefabList = AssetDatabase.FindAssets ("l:NCAR_model");
			foreach (string guid in prefabList)
				Debug.Log (AssetDatabase.GUIDToAssetPath (guid)); 
	
		}

		[MenuItem ("Noclew/Find All AR postcard object")]
		public static void FindAllARCards ()
		{
			string[] cardList = NcHelpers.FindAllARTargetNames ();
			foreach (string name in cardList)
				Debug.Log (name);
		}

	}


}
