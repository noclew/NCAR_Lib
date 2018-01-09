using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noclew;

public class TestComponent : MonoBehaviour {
	public int[] defaultSetting;
	public ARRuleTemplate[] rules;

	// Use this for initialization
	void Start () {
		foreach (ARRuleTemplate rule in rules) {
			foreach (ARShowEventTemplate nEvent in rule.showEvents) {
				foreach (int n in nEvent.modelIndices){
					print (n);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
