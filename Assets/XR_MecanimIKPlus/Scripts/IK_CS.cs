using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace MecanimIKPlus
{
	public class IK_CS : MonoBehaviour
	{
		public Transform bodyTarget;
		public Transform leftHandTarget;
		public Transform rightHandTarget;
		public Transform leftFootTarget;
		public Transform rightFootTarget;

		[Range (0.0f, 1.0f)] public float leftHandPosWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float leftHandRotWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float rightHandPosWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float rightHandRotWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float leftFootPosWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float leftFootRotWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float rightFootPosWeight = 1.0f;
		[Range (0.0f, 1.0f)] public float rightFootRotWeight = 1.0f;

		Animator animator;

		void Start ()
		{
			animator = GetComponent <Animator> ();
		}

		void OnAnimatorIK ()
		{	
			animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, leftHandPosWeight);
			animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, leftHandRotWeight);
			animator.SetIKPositionWeight (AvatarIKGoal.RightHand, rightHandPosWeight);
			animator.SetIKRotationWeight (AvatarIKGoal.RightHand, rightHandRotWeight);
			animator.SetIKPositionWeight (AvatarIKGoal.LeftFoot, leftFootPosWeight);
			animator.SetIKRotationWeight (AvatarIKGoal.LeftFoot, leftFootRotWeight);
			animator.SetIKPositionWeight (AvatarIKGoal.RightFoot, rightFootPosWeight);
			animator.SetIKRotationWeight (AvatarIKGoal.RightFoot, rightFootRotWeight);
			if (bodyTarget != null) {
				animator.bodyPosition = bodyTarget.position;
				animator.bodyRotation = bodyTarget.rotation;
			}
			if (leftHandTarget != null) {
				animator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandTarget.position);
				animator.SetIKRotation (AvatarIKGoal.LeftHand, leftHandTarget.rotation);
			}				
			if (rightHandTarget != null) {
				animator.SetIKPosition (AvatarIKGoal.RightHand, rightHandTarget.position);
				animator.SetIKRotation (AvatarIKGoal.RightHand, rightHandTarget.rotation);
			}				
			if (leftFootTarget != null) {
				animator.SetIKPosition (AvatarIKGoal.LeftFoot, leftFootTarget.position);
				animator.SetIKRotation (AvatarIKGoal.LeftFoot, leftFootTarget.rotation);
			}				
			if (rightFootTarget != null) {
				animator.SetIKPosition (AvatarIKGoal.RightFoot, rightFootTarget.position);
				animator.SetIKRotation (AvatarIKGoal.RightFoot, rightFootTarget.rotation);
			}				
		}

	}

}
