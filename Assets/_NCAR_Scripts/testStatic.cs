// OnHierarchyChange
//
// Watch Hierarchy and Inspector access.  When GameObjects are changed, or new ones
// added or deleted this example will be called.

using UnityEngine;
using UnityEditor;

public class ExampleCode
{
	[MenuItem("Example/Hierarchy Window Changed")]
	static void Example()
	{
		EditorApplication.hierarchyWindowChanged += ExampleCallback;
	}

	[MenuItem("Example/REmove call back Hierarchy Window Changed")]
	static void Example2()
	{
		EditorApplication.hierarchyWindowChanged -= ExampleCallback;
	}

	static void ExampleCallback()
	{
		Object[] all = Resources.FindObjectsOfTypeAll(typeof(Object));
		Debug.Log("There are " + all.Length + " objects at the moment.");
		//EditorApplication.hierarchyWindowChanged.GetInvocationList[]
	}

	[MenuItem("Example/Find with tag")]
	static void findBitch(){
		GameObject[] res = GameObject.FindGameObjectsWithTag ("NCAR_model");
		foreach (GameObject go in res)
			Debug.Log (go.name);
		Debug.Log ("fin");
	}

}