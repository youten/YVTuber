using System.Collections;
using UnityEngine;

namespace MecanimIKPlus
{
	
	public class IK_Shoulder_Linkage_CS : MonoBehaviour
	{

		public Transform leftShoulderTransform;
		public Transform rightShoulderTransform;
		public Transform leftArmTransform;
		public Transform rightArmTransform;
		[Range (0.0f, 1.0f)] public float shoulderLinkageRatio = 0.4f;

		Quaternion initialLeftSholderRot;
		Quaternion initialRightSholderRot;
		Quaternion initialLeftArmRot;
		Quaternion initialRightArmRot;

		void Awake ()
		{
			initialLeftSholderRot = leftShoulderTransform.localRotation;
			initialRightSholderRot = rightShoulderTransform.localRotation;
			initialLeftArmRot = leftArmTransform.localRotation;
			initialRightArmRot = rightArmTransform.localRotation;
		}

		void LateUpdate ()
		{
			Rotate ();
		}

		void Rotate ()
		{
			// Left
			Quaternion deltaRot = leftArmTransform.localRotation * Quaternion.Inverse(initialLeftArmRot);
			Quaternion targetRot = initialLeftSholderRot * deltaRot;
			leftShoulderTransform.localRotation = Quaternion.Lerp(initialLeftSholderRot, targetRot, shoulderLinkageRatio);
			leftArmTransform.localRotation = Quaternion.Lerp(initialLeftArmRot, leftArmTransform.localRotation, 1.0f - shoulderLinkageRatio);
			// Right
			deltaRot = rightArmTransform.localRotation * Quaternion.Inverse(initialRightArmRot);
			targetRot = initialRightSholderRot * deltaRot;
			rightShoulderTransform.localRotation = Quaternion.Lerp(initialRightSholderRot, targetRot, shoulderLinkageRatio);
			rightArmTransform.localRotation = Quaternion.Lerp(initialRightArmRot, rightArmTransform.localRotation, 1.0f - shoulderLinkageRatio);
		}

	}

}