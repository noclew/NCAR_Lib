using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noclew
{

	public class NARmodelController : MonoBehaviour
	{
		Vector3 posInit;
		Quaternion rotInit;
		// Use this for initialization
		void Start ()
		{
			//save the initial pos and rot
			Vector3 posInit = this.transform.InverseTransformPoint (transform.position);
			Quaternion rotInit = transform.rotation;
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		public bool attachToTarget ()
		{
			return true;
		}
	}

}