using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace noclew
{	
	public static class NcHelpers
	{

		static GameObject[] FindSceneObjectsByTag(){
			GameObject[] res = GameObject.FindGameObjectsWithTag ("NCAR_model");
			return res;
		}


		/// <summary>
		/// Finds all AR models labelled as "NCAR_model"
		/// </summary>
		/// <returns>The all AR models.</returns>
		/// <param name="isGuid">If set to <c>true</c> is GUID.</param>

		public static string[] FindAllARAssetModels(bool isGuid=false){		
			string[] GuidList = AssetDatabase.FindAssets ("l:NCAR_model");

			if (isGuid) {
				return GuidList;
			}
			else {
				string[] res = new string[ GuidList.Length ];
				int i = 0;
				foreach (string guid in GuidList) {
					res[i] = System.IO.Path.GetFileNameWithoutExtension( AssetDatabase.GUIDToAssetPath(guid) );
					i++;
				}
				return res;
			}			
		}
		/// <summary>
		/// Finds all AR targets, return as gameobjects.
		/// </summary>
		/// <returns>The array of a gameObject containing AR handler.</returns>
		public static GameObject[] FindAllARTargets(){
			DefaultTrackableEventHandler[] temp = UnityEngine.Object.FindObjectsOfType<DefaultTrackableEventHandler>();
			var res = temp.Select(p => p.gameObject).ToArray ();
			return res;
		}

		/// <summary>
		/// Finds all AR target names, return as string[].
		/// </summary>
		/// <returns>The all AR target names.</returns>
		public static string[] FindAllARTargetNames(){
			GameObject[] temp = FindAllARTargets ();
			var res = temp.Select(p => p.gameObject.name).ToArray ();
			return res;
		}


	}

}
