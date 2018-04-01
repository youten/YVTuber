// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Hand Motion Contorller for HandPoses 0.2.0
namespace YVTuber {
	public class HandController : MonoBehaviour {
		public GameObject targetModel;
		private Animator animator;

		void Start() {
			animator = GetComponent<Animator> ();	
			animator.SetLayerWeight (1, 1.0f); // LeftHand Layer
			animator.SetLayerWeight (2, 1.0f); // rightHand Layer
		}

		void Update() {
			// Left Controller Trackpad Press:8 Touch:16
			// Right Controller Trackpad Press:9 Touch:17
			if (XRVive.isLeftTrackPadPressGetKeyUp()) {
				animator.Play ("hand_l_base");
			} else if (XRVive.isLeftTrackPadPressGetKey()) {
				if (XRVive.isLeftTrackPadLeftUpTouched()) {
					animator.Play ("hand_l_point");
				} else if (XRVive.isLeftTrackPadRightUpTouched()) {
					animator.Play ("hand_l_ok");
				} else if (XRVive.isLeftTrackPadLeftDownTouched()) {
					animator.Play ("hand_l_fist");
				} else if (XRVive.isLeftTrackPadRightDownTouched()) {
					animator.Play ("hand_l_v");
				}
			}
			if (Input.GetKeyUp(KeyCode.JoystickButton9)) {
				animator.Play ("hand_r_base");
			} else if (Input.GetKey(KeyCode.JoystickButton9)) {
				if (XRVive.isRightTrackPadLeftUpTouched()) {
					animator.Play ("hand_r_point");
				} else if (XRVive.isRightTrackPadRightUpTouched()) {
					animator.Play ("hand_r_ok");
				} else if (XRVive.isRightTrackPadLeftDownTouched()) {
					animator.Play ("hand_r_fist");
				} else if (XRVive.isRightTrackPadRightDownTouched()) {
					animator.Play ("hand_r_v");
				}
			}
		}
	}
}