using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MecanimIKPlus
{
	
	public class IK_Body_Linkage_CS : MonoBehaviour
	{

		public Transform eyeTransform;
		public Transform leftHandTarget;
		public Transform rightHandTarget;
		public Transform leftFootTarget;
		public Transform rightFootTarget;
		public Transform pivotTransform;
		public float offsetY = 0.5f;
		public float offsetZ = 0.0f;
		public float maxTorsionAngle = 45.0f;
		public float armLength = 0.4f;

		Transform thisTransform;

		void Awake ()
		{
			thisTransform = transform;
		}

		void Update ()
		{
			Control_Pivot ();
			Position ();
			Rotation ();
		}

		void Control_Pivot ()
		{
			// Set "IK_Pivot" position at the center of the feet.
			pivotTransform.position = Vector3.Lerp (rightFootTarget.position, leftFootTarget.position, 0.5f);
			// Set "IK_Pivot" rotation at the intermediate angle of the feet.
			Vector3 pivotAngles = pivotTransform.localEulerAngles;
			pivotAngles.y = Mathf.LerpAngle (rightFootTarget.eulerAngles.y, leftFootTarget.eulerAngles.y, 0.5f);
			pivotTransform.localEulerAngles = pivotAngles;
		}

		void Position ()
		{
			Vector3 bodyPos = thisTransform.position;
			// Linked with eye position.
			bodyPos.y = eyeTransform.position.y - offsetY;
			// Linked with "IK_Pivot" position.
			bodyPos.x = pivotTransform.position.x;
			bodyPos.z = pivotTransform.position.z;
			// Set position.
			thisTransform.position = bodyPos;
		}

		void Rotation ()
		{
			// Linked with hand position.
			float leftHandLinkageAng = Mathf.LerpAngle (0.0f, maxTorsionAngle, pivotTransform.InverseTransformPoint (leftHandTarget.position).x / armLength);
			float rightHandLinkageAng = Mathf.LerpAngle (0.0f, -maxTorsionAngle, -pivotTransform.InverseTransformPoint (rightHandTarget.position).x / armLength);
			float handLinkageAng = leftHandLinkageAng + rightHandLinkageAng;
			// Linked with head position.
			Vector3 thisLocalPos = pivotTransform.InverseTransformPoint (thisTransform.position);
			Vector3 eyeLocalPos = pivotTransform.InverseTransformPoint (eyeTransform.position);
			float angX = Mathf.Atan2 (eyeLocalPos.y - thisLocalPos.y, eyeLocalPos.z - thisLocalPos.z - offsetZ) * Mathf.Rad2Deg - 90.0f;
			float angZ = Mathf.Atan2 (eyeLocalPos.y - thisLocalPos.y, eyeLocalPos.x - thisLocalPos.x) * Mathf.Rad2Deg - 90.0f;
			// Linked with head rotation.
			float headLinkageAng = Mathf.DeltaAngle (0.0f, eyeTransform.localEulerAngles.y) * 0.25f;
			// Set rotation.
			Vector3 bodyAng = thisTransform.localEulerAngles;
			bodyAng.x = -angX;
			bodyAng.y = pivotTransform.localEulerAngles.y + handLinkageAng + headLinkageAng;
			bodyAng.z = angZ;
			thisTransform.localEulerAngles = bodyAng;
		}

	}

}
