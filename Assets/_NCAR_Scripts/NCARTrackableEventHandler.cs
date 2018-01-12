using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noclew;
using Vuforia;

public class NCARTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

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

	Vector3 posTargetInit;
	Vector3 scaleTargetInit;
	Quaternion rotTargetInit;
	 

	public Vector3 wTopLeft {
		get { return this.transform.TransformPoint (ptTopLeft); }
	}

	public Vector3 wTopRight {
		get { return this.transform.TransformPoint (ptTopRight); }
	}

	public Vector3 wBottomLeft {
		get { return this.transform.TransformPoint (ptBottomLeft); }
	}

	public Vector3 wBottomRight {
		get { return this.transform.TransformPoint (ptBottomRight); }
	}

	public float TargetWidth {
		get { return targetWidth; }
	}

	public float TargetHeight {
		get { return targetHeight; }
	}

	public bool IsBeingTracked {
		get { return isBeingTracked; }
	}

	#region PRIVATE_MEMBER_VARIABLES

	protected TrackableBehaviour mTrackableBehaviour;

	#endregion // PRIVATE_MEMBER_VARIABLES

	#region UNTIY_MONOBEHAVIOUR_METHODS

	// Use this for initialization

	void Awake ()
	{
		vImageTargetBehavior = GetComponent<ImageTargetBehaviour> ();	
	}

	GameObject c;

	protected virtual void Start ()
	{

		//save the default pos and rot
		posTargetInit = transform.position;
		rotTargetInit = transform.rotation;
		scaleTargetInit = transform.lossyScale;
			
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}



		///get the size of the target
		targetHeight = vImageTargetBehavior.GetSize () [1];
		targetWidth = vImageTargetBehavior.GetSize () [0];

		//local coordinates of th target
		ptTopRight = new Vector3 (0.5f * (targetWidth / targetHeight), 0, 0.5f);
		ptTopLeft = new Vector3 (-0.5f * (targetWidth / targetHeight), 0, 0.5f);
		ptBottomRight = new Vector3 (0.5f * (targetWidth / targetHeight), 0, -0.5f);
		ptBottomLeft = new Vector3 (-0.5f * (targetWidth / targetHeight), 0, -0.5f);

	}
			
	
	// Update is called once per frame
	void Update ()
	{

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
			isBeingTracked = false;
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

	/// <summary>
	/// Calculates the initial local posistion of a model in relation to this target's initial position before trackings.
	/// </summary>
	/// <returns>The initial local poss.</returns>
	/// <param name="modelPos">Model position.</param>
	public Vector3 CalcInitialLocalPosOfModel (Vector3 posModelInit)
	{
		//this part is inverse of local to global (inverse function of TransformPoint).
		Vector3 t = Quaternion.Inverse (rotTargetInit) * (posModelInit - posTargetInit);
		t = Vector3.Scale (new Vector3 (1 / scaleTargetInit.x, 1 / scaleTargetInit.y, 1 / this.scaleTargetInit.z), t);
		return t;
	}

	public void MoveModelOnThis (NCARModelData model)
	{
		Transform trans = model.transform;
		Vector3 initLocal = CalcInitialLocalPosOfModel (model.posInit);
		trans.SetPositionAndRotation (transform.TransformPoint (initLocal), model.rotInit * this.transform.rotation);
		//trans.SetParent (this.transform);
	}

	#endregion // PRIVATE_METHODS
}

