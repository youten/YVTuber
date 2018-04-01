using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace MecanimIKPlus
{
	
	public class XR_Hand_Tracking_CS : MonoBehaviour
	{

		public bool isLeft;
		public Vector3 offsetAngles;

		Transform thisTransform;
		XRNode node;
		Vector3 targetPos;

		void Start ()
		{
			thisTransform = transform;
			if (isLeft) {
				node = XRNode.LeftHand;
			} else {
				node = XRNode.RightHand;
			}
		}

		void Update ()
		{
			thisTransform.localPosition = InputTracking.GetLocalPosition (node);
			thisTransform.localRotation = InputTracking.GetLocalRotation (node) * Quaternion.Euler (offsetAngles);
		}
	}

}
