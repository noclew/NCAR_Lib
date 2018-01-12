using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace noclew
{
	public class NCAREditorAddons
	{

		[MenuItem ("NCAR/Rebuild AR App DB")]
		public static void RebuildAppDB(){
			NCARappDB.Rebuild ();	
		}

		[MenuItem ("NCAR/Find All AR model Prefab")]
		public static void FindAllNCARLabedPrefabs ()
		{		
			string[] prefabList = AssetDatabase.FindAssets ("l:NCAR_model");
			foreach (string guid in prefabList)
				Debug.Log (AssetDatabase.GUIDToAssetPath (guid)); 
	
		}
			
		[MenuItem ("NCAR/Find All AR postcard object in the scene")]
		public static void FindAllARCards ()
		{
			string[] cardList = NcHelpers.FindAllARTargetNames ();
			foreach (string name in cardList)
				Debug.Log (name);
		}

		[MenuItem ("NCAR/List all target in DB")]
		public static void ListAllTargetInDB()
		{
			foreach (GameObject go in NCARappDB.targets)
				Debug.Log (go.name);
		}

		[MenuItem ("NCAR/List all target names in DB")]
		public static void ListAllTargetNamesInDB()
		{
			foreach (string n in NCARappDB.targetNames)
				Debug.Log (n);
		}

		[MenuItem ("NCAR/List all model in DB")]
		public static void ListAllModelsInDB()
		{
			foreach (GameObject go in NCARappDB.sceneModels)
				Debug.Log (go.name);
		}
		[MenuItem ("NCAR/List all model names in DB")]
		public static void ListAllModelNamesInDB()
		{
			foreach (string n in NCARappDB.sceneModelNames)
				Debug.Log (n);
		}


	}


}
