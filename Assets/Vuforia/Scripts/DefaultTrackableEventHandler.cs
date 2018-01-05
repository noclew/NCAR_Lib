/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
	public GameObject testobj;
	Vector3 initLocal;
	Quaternion initRot;
	bool isTracking = false;
	/// 
	#region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

	#endregion // PRIVATE_MEMBER_VARIABLES

	#region UNTIY_MONOBEHAVIOUR_METHODS

	protected virtual void Start ()
	{
		if (testobj) {
			testobj = Instantiate (testobj);
			initLocal = this.transform.InverseTransformPoint (testobj.transform.position);
			initRot = testobj.transform.rotation;
		}



		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		
	}

	void Update ()
	{
		if (testobj && isTracking) {
//			testobj.transform.rotation = initRot * this.transform.rotation;
//			testobj.transform.position = transform.TransformPoint (initLocal);
			testobj.transform.SetPositionAndRotation (transform.TransformPoint (initLocal), initRot * this.transform.rotation);
			testobj.transform.SetParent (this.transform);

		}
	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS

	/// <summary>
	///     Implementation of the ITrackableEventHandler function called when the
	///     tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged (
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		          newStatus == TrackableBehaviour.Status.TRACKED ||
		          newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
			isTracking = true;
			OnTrackingFound ();
		} else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
		               newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			isTracking = false;
			OnTrackingLost ();
		} else {
			// For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
			// Vuforia is starting, but tracking has not been lost or found yet
			// Call OnTrackingLost() to hide the augmentations
			OnTrackingLost ();
		}
	}

	#endregion // PUBLIC_METHODS

	#region PRIVATE_METHODS

	protected virtual void OnTrackingFound ()
	{
		var rendererComponents = GetComponentsInChildren<Renderer> (true);
		var colliderComponents = GetComponentsInChildren<Collider> (true);
		var canvasComponents = GetComponentsInChildren<Canvas> (true);



		// Enable rendering:
		foreach (var component in rendererComponents)
			component.enabled = true;

		// Enable colliders:
		foreach (var component in colliderComponents)
			component.enabled = true;

		// Enable canvas':
		foreach (var component in canvasComponents)
			component.enabled = true;
		
		if(testobj) testobj.GetComponent<Renderer> ().enabled = true;
	}


	protected virtual void OnTrackingLost ()
	{
		var rendererComponents = GetComponentsInChildren<Renderer> (true);
		var colliderComponents = GetComponentsInChildren<Collider> (true);
		var canvasComponents = GetComponentsInChildren<Canvas> (true);

		// Disable rendering:
		foreach (var component in rendererComponents)
			component.enabled = false;

		// Disable colliders:
		foreach (var component in colliderComponents)
			component.enabled = false;

		// Disable canvas':
		foreach (var component in canvasComponents)
			component.enabled = false;

		if(testobj) testobj.GetComponent<Renderer> ().enabled = false;
	}

	#endregion // PRIVATE_METHODS
}
