using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noclew
{

	public class NCARModelData : MonoBehaviour
	{
		public Vector3 posInit { get; set;}
		public Quaternion rotInit{ get; set;}
		public Vector2 scaleInit {get; set;}

		public bool isTriggered {get; set;}

		public GameObject targetAttached { get; set; }
		public GameObject defaultTargetAttached = null;

		// Use this for initialization
		void Start ()
		{
			//save the initial pos and rot
			posInit = transform.position;
			rotInit = transform.rotation;
			scaleInit = transform.lossyScale;
		}
	
		// Update is called once per frame
		void Update ()
		{

		}


	}

}