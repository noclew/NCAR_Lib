using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;

public class NCARTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

	//custom variables
	ImageTargetBehaviour vImageTargetBehavior;

	public int targetIndex { get; set; }
	bool isBeingTracked = false;
	Vector3 ptTopLeft;
	Vector3 ptTopRight;
	Vector3 ptBottomLeft;
	Vector3 ptBottomRight;

	float targetHeight;
	float targetWidth;

	public Vector3 wTopLeft
	{
		get { return this.transform.TransformPoint(ptTopLeft); }
	}
	public Vector3 wTopRight
	{
		get { return this.transform.TransformPoint(ptTopRight); }
	}
	public Vector3 wBottomLeft
	{
		get { return this.transform.TransformPoint(ptBottomLeft); }
	}
	public Vector3 wBottomRight
	{
		get { return this.transform.TransformPoint(ptBottomRight); }
	}
	public float TargetWidth
	{
		get { return targetWidth; }
	}
	public float TargetHeight
	{
		get { return targetHeight; }
	}

	public bool IsBeingTracked
	{
		get { return isBeingTracked; }
	}

	#region PRIVATE_MEMBER_VARIABLES
	protected TrackableBehaviour mTrackableBehaviour;
	#endregion // PRIVATE_MEMBER_VARIABLES

	#region UNTIY_MONOBEHAVIOUR_METHODS

	// Use this for initialization

	void Awake() {
		vImageTargetBehavior = GetComponent<ImageTargetBehaviour> ();	
	}

	protected virtual void Start () {
		//save the default pos and rot


		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}

		///NCAR code
		targetHeight = vImageTargetBehavior.GetSize()[1];
		targetWidth = vImageTargetBehavior.GetSize()[0];

		ptTopRight = new Vector3(0.5f * (targetWidth / targetHeight), 0, 0.5f);
		ptTopLeft = new Vector3(-0.5f * (targetWidth / targetHeight), 0, 0.5f);
		ptBottomRight = new Vector3(0.5f * (targetWidth / targetHeight), 0, -0.5f);
		ptBottomLeft = new Vector3(-0.5f * (targetWidth / targetHeight), 0, -0.5f);

		GameObject c = GameObject.CreatePrimitive (PrimitiveType.Cube);
		c.transform.SetParent (transform);
		c.transform.localPosition = ptTopLeft;
		c.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

	}
	
	// Update is called once per frame
	void Update () {
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
			isBeingTracked = true;
			OnTrackingFound ();
		} else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
			newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			isBeingTracked = false;
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

		////////////VuforiaDefault disabled.
//		var rendererComponents = GetComponentsInChildren<Renderer> (true);
//		var colliderComponents = GetComponentsInChildren<Collider> (true);
//		var canvasComponents = GetComponentsInChildren<Canvas> (true);
//
//		// Enable rendering:
//		foreach (var component in rendererComponents)
//			component.enabled = true;
//
//		// Enable colliders:
//		foreach (var component in colliderComponents)
//			component.enabled = true;
//
//		// Enable canvas':
//		foreach (var component in canvasComponents)
//			component.enabled = true;


	}


	protected virtual void OnTrackingLost ()
	{
		////////////VuforiaDefault disabled.
//		var rendererComponents = GetComponentsInChildren<Renderer> (true);
//		var colliderComponents = GetComponentsInChildren<Collider> (true);
//		var canvasComponents = GetComponentsInChildren<Canvas> (true);
//
//		// Disable rendering:
//		foreach (var component in rendererComponents)
//			component.enabled = false;
//
//		// Disable colliders:
//		foreach (var component in colliderComponents)
//			component.enabled = false;
//
//		// Disable canvas':
//		foreach (var component in canvasComponents)
//			component.enabled = false;

	}

	#endregion // PRIVATE_METHODS
}
