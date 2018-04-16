// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Hand Motion Contorller for HandPoses 0.2.0
namespace YVTuber {
	public class HandController : MonoBehaviour {
		private string LEFT_HAND_POSE_NAME = "LeftHandPose";
		private string RIGHT_HAND_POSE_NAME = "RightHandPose";
		enum HandPose {
			_base = 0,
			fist = 1,
			v = 2,
			open = 3,
			like = 4,
			ok = 5,
			point = 6,
			rocknroll = 7,
			otome_grip = 8,
		};

		private Animator animator;

		void Start() {
			animator = GetComponent<Animator> ();	
			animator.SetLayerWeight (1, 1.0f); // LeftHand Layer
			animator.SetLayerWeight (2, 1.0f); // rightHand Layer
		}

		void Update() {
			// Left Controller Trackpad Press:8 Touch:16
			// Right Controller Trackpad Press:9 Touch:17
			if (XRVive.isLeftTrackPadTouchGetKeyUp()) {
				animator.SetInteger (LEFT_HAND_POSE_NAME, (int)HandPose._base);
			}
			if (XRVive.isLeftTrackPadTouchGetKey()) {
				if (XRVive.isLeftTrackPadLeftUpTouched()) {
					animator.SetInteger (LEFT_HAND_POSE_NAME, (int)HandPose.rocknroll);
				} else if (XRVive.isLeftTrackPadRightUpTouched()) {
					animator.SetInteger (LEFT_HAND_POSE_NAME, (int)HandPose.point);
				} else if (XRVive.isLeftTrackPadLeftDownTouched()) {
					animator.SetInteger (LEFT_HAND_POSE_NAME, (int)HandPose.v);
				} else if (XRVive.isLeftTrackPadRightDownTouched()) {
					animator.SetInteger (LEFT_HAND_POSE_NAME, (int)HandPose.fist);
				}
			}
			if (XRVive.isRightTrackPadTouchGetKeyUp()) {
				animator.SetInteger (RIGHT_HAND_POSE_NAME, (int)HandPose._base);
			}
			if (XRVive.isRightTrackPadTouchGetKey()) {
				if (XRVive.isRightTrackPadLeftUpTouched()) {
					animator.SetInteger (RIGHT_HAND_POSE_NAME, (int)HandPose.point);
				} else if (XRVive.isRightTrackPadRightUpTouched()) {
					animator.SetInteger (RIGHT_HAND_POSE_NAME, (int)HandPose.rocknroll);
				} else if (XRVive.isRightTrackPadLeftDownTouched()) {
					animator.SetInteger (RIGHT_HAND_POSE_NAME, (int)HandPose.fist);
				} else if (XRVive.isRightTrackPadRightDownTouched()) {
					animator.SetInteger (RIGHT_HAND_POSE_NAME, (int)HandPose.v);
				}
			}
		}
	}
}
