using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
	void OnGUI() {
		GUI.Button(new Rect(0, 0, 100, 20), new GUIContent("Click me", "This is the tooltip"));
		GUI.Label(new Rect(0, 40, 100, 40), GUI.tooltip);
	}
}