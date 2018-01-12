using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noclew
{

	using Ncte = NCARTrackableEventHandler;

	public class NCARMainController : MonoBehaviour
	{
		public NCARRuleSet ruleset;
		public Ncte t1;
		public Ncte t2;

		void Awake ()
		{
			//add NCARmodel to ar models
			GameObject[] modelList = NcHelpers.FindAllSceneModelsByTag ();
			foreach (GameObject model in modelList)
				model.AddComponent<NCARModelData> ();
			Debug.Log ("-----> NCARModelData Component Attached");
		}

		// Use this for initialization
		void Start ()
		{
			//set the default Targets
			foreach (NCARDefaultState dst in ruleset.defaultStates) {
				NCARappDB.GetModelByName (dst.ModelName).GetComponent<NCARModelData> ().defaultTargetAttached = NCARappDB.GetTargetByName (dst.TargetName);
			}
		}

		// Update is called once per frame
		void Update ()
		{

			//Set all the trigger flags of the models to false, and reset the targetAttached to default;
			foreach (GameObject go in NCARappDB.sceneModels) {
				NCARModelData md = go.GetComponent<NCARModelData> ();
				//set the trigger of this model false
				md.isTriggered = false;
				//set the target attached to the model to the default one
				md.targetAttached = md.defaultTargetAttached;
			}

			//Set all the configure flag of the target to false. 
			foreach (GameObject go in NCARappDB.targets) {
				go.GetComponent<NCARTrackableEventHandler> ().isTargetConfigured = false;
			}


			//iterate through the rules in ruleset
			foreach (NCARRule rule in ruleset.rules) {

				bool isConditionsMet = true;

				//iterate through conditions in a rule
				foreach (ARTargetPairwiseCondition condition in rule.pairWiseConditions) {
					
					Ncte t1 = NCARappDB.GetTargetByName (condition.target1Name).GetComponent<Ncte> ();
					Ncte t2 = NCARappDB.GetTargetByName (condition.target2Name).GetComponent<Ncte> ();
					//if any target in the condition is already configured, break
					if (t1.isTargetConfigured || t2.isTargetConfigured) {
						isConditionsMet = false;
						break;
					}

					//if the current condition is not met, break;
					if (!NCARappDB.conditionDeligates [condition.deligateIndex].Invoke (t1, t2, ruleset.angleThreshold, ruleset.distanceThreshold)) {
						isConditionsMet = false;
						break;
					}
				}

				//if all conditions are met
				if (isConditionsMet) {
					//mark all the target as configured
					//for each condition
					foreach (ARTargetPairwiseCondition condition in rule.pairWiseConditions) {
						NCARappDB.GetTargetByName (condition.target1Name).GetComponent<Ncte> ().isTargetConfigured = true;
						NCARappDB.GetTargetByName (condition.target2Name).GetComponent<Ncte> ().isTargetConfigured = true;
					}
					//for each show event
					foreach (NCARShowEvent showEvent in rule.showEvents) {
						
						//for each model in show event
						foreach (string modelName in showEvent.modelNames) {
							// if model is not triggered in this loop, trigger it and attach target to models
							NCARModelData md = NCARappDB.GetModelByName (modelName).GetComponent<NCARModelData> ();
							if (!md.isTriggered) {
								md.targetAttached = NCARappDB.GetTargetByName (showEvent.targetName);
								md.isTriggered = true;
							}
						}
					}

					//for each model in hide event, 
					foreach (NCARHideEvent hideEvent in rule.hideEvents) {
						//set target to null if exist

						if (hideEvent != null && hideEvent.modelName != "") {
							NCARModelData md = NCARappDB.GetModelByName (hideEvent.modelName).GetComponent<NCARModelData> ();
							md.targetAttached = null;
						}

					}
				}
			}

			//now update model state
			//for each model
			foreach (GameObject go in NCARappDB.sceneModels) {
				NCARModelData md = go.GetComponent<NCARModelData> ();

				//if this model is not attached to any target or the attached target is not being tracked, hide this model
				if (md.targetAttached == null || !md.targetAttached.GetComponent<Ncte> ().IsBeingTracked)
					DeactivateGameObject (md);

				//if this model is attached any target that is being tracked,
				if ((md.targetAttached != null)
				    && md.targetAttached.GetComponent<Ncte> ().IsBeingTracked) {
					//move this model on that target
					md.targetAttached.GetComponent<Ncte> ().MoveModelOnThis (md);
					//and show it, and show it 
					ActivateGameObject (md);
				}

			}
		}


		void DeactivateGameObject (GameObject go)
		{
			go.SetActive (false);
		}

		void ActivateGameObject (GameObject go)
		{
			go.SetActive (true);
		}

		void DeactivateGameObject<T> (T model) where T: Component
		{
			GameObject go = model.gameObject;
			go.SetActive (false);
//			model.gameObject.SetActive (false);
//			var rendererComponents = GetComponentsInChildren<Renderer> (true);
//			var colliderComponents = GetComponentsInChildren<Collider> (true);
//			var canvasComponents = GetComponentsInChildren<Canvas> (true);
//		
//			// Disable rendering:
//			foreach (var component in rendererComponents)
//				component.enabled = false;
//		
//			// Disable colliders:
//			foreach (var component in colliderComponents)
//				component.enabled = false;
//		
//			// Disable canvas':
//			foreach (var component in canvasComponents)
//				component.enabled = false;
		}

		void ActivateGameObject<T> (T model) where T: Component
		{
			GameObject go = model.gameObject;
			go.SetActive (true);
//			var rendererComponents = GetComponentsInChildren<Renderer> (true);
//			var colliderComponents = GetComponentsInChildren<Collider> (true);
//			var canvasComponents = GetComponentsInChildren<Canvas> (true);
//		
//			// Enable rendering:
//			foreach (var component in rendererComponents)
//				component.enabled = true;
//		
//			// Enable colliders:
//			foreach (var component in colliderComponents)
//				component.enabled = true;
//		
//			// Enable canvas':
//			foreach (var component in canvasComponents)
//				component.enabled = true;
		}
	}

}