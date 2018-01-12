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

			//delete default trackableEventHandler and add NCAR
			foreach (GameObject target in NCARappDB.targets) {
				if (target.GetComponent<DefaultTrackableEventHandler>() != null) {
					Destroy (target.GetComponent<DefaultTrackableEventHandler> () );
				}
				target.AddComponent<NCARTrackableEventHandler> ();
			}
		}

		// Use this for initialization
		void Start ()
		{
			//set the default Targets
			foreach (ARDefaultStateTemplate dst in ruleset.defaultStates) {
				NCARappDB.GetModelByName (dst.ModelName).GetComponent<NCARModelData> ().defaultTargetAttached = NCARappDB.GetTargetByName (dst.TargetName);
			}
		}

		// Update is called once per frame
		void Update ()
		{

			//Set all the trigger flags of the models to false, and reset the targetAttached to default;
			foreach (GameObject go in NCARappDB.sceneModels) {
				NCARModelData md = go.GetComponent<NCARModelData> ();
				md.isTriggered = false;
				md.targetAttached = md.defaultTargetAttached;
			}


			//iterate through the rules in ruleset
			foreach (ARRuleTemplate rule in ruleset.rules) {

				bool isConditionsMet = true;

				//iterate through conditions in a rule
				foreach (ARTargetConditionTemplate condition in rule.pairWiseConditions) {
					
					Ncte t1 = NCARappDB.GetTargetByName (condition.target1Name).GetComponent<Ncte>();
					Ncte t2 = NCARappDB.GetTargetByName (condition.target2Name).GetComponent<Ncte>();

					if (!NCARappDB.conditionDeligates [condition.deligateIndex].Invoke (t1, t2, ruleset.angleThreshold, ruleset.distanceThreshold)) {
						isConditionsMet = false;
						break;
					}
				}

				//if all conditions are met
				if (isConditionsMet) {
					//for each show event
					foreach (NCARShowEventTemplate showEvent in rule.showEvents) {
						//for each models in show event, attach target to models
						foreach (string modelName in showEvent.modelNames) {
							NCARModelData md = NCARappDB.GetModelByName (modelName).GetComponent<NCARModelData>();
							md.targetAttached = NCARappDB.GetTargetByName (showEvent.targetName);
						}
					}
				}
			}

			//for each model
			foreach (GameObject go in NCARappDB.sceneModels) {
				NCARModelData md = go.GetComponent<NCARModelData> ();

				//if this model is not attached to any target or the attached target is not being tracked, hide this model
				if (md.targetAttached == null || !md.targetAttached.GetComponent<Ncte>().IsBeingTracked) 
					DeactivateGameObject(md);

				//if this model is attached any target that is being tracked, move this model on that target and show it, and show it 
				if ((md.targetAttached != null) 
					&& md.targetAttached.GetComponent<Ncte> ().IsBeingTracked) {
					md.targetAttached.GetComponent<Ncte> ().MoveModelOnThis (md);
					ActivateGameObject (md);
				}

			}
		}


		void DeactivateGameObject(GameObject go){
			go.SetActive (false);
		}

		void ActivateGameObject(GameObject go){
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