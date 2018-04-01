using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Viveコントローラのタッチパッドを左上・右上・左下・右下の4分割で使い分ける（上下左右はAPI上難しいので象限の方が妥当）
// 左上[（予約）][（予約）]右上
// 左下[ グー ][ チョキ ]右下
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
			if (Input.GetKeyUp (KeyCode.JoystickButton8)) {
				animator.Play ("hand_l_base");
			} else if (Input.GetKey (KeyCode.JoystickButton8)) {
				float x = Input.GetAxis ("Horizontal");
				float y = Input.GetAxis ("Vertical");
				if (isTouchPadLeftUpTouched (x, y)) {
					animator.Play ("hand_l_point");
				} else if (isTouchPadRightUpTouched (x, y)) {
					animator.Play ("hand_l_ok");
				} else if (isTouchPadLeftDownTouched (x, y)) {
					animator.Play ("hand_l_fist");
				} else if (isTouchPadRightDownTouched (x, y)) {
					animator.Play ("hand_l_v");
				}
			}
			if (Input.GetKeyUp(KeyCode.JoystickButton9)) {
				animator.Play ("hand_r_base");
			} else if (Input.GetKey(KeyCode.JoystickButton9)) {
				float x = Input.GetAxis ("RightHorizontal");
				float y = Input.GetAxis ("RightVertical");
				if (isTouchPadLeftUpTouched (x, y)) {
					animator.Play ("hand_r_point");
				} else if (isTouchPadRightUpTouched (x, y)) {
					animator.Play ("hand_r_ok");
				} else if (isTouchPadLeftDownTouched (x, y)) {
					animator.Play ("hand_r_fist");
				} else if (isTouchPadRightDownTouched (x, y)) {
					animator.Play ("hand_r_v");
				}
			}
		}

		// 左上
		bool isTouchPadLeftUpTouched(float x, float y) {
			if (x < 0 && y > 0) {
				return true;
			}
			return false;
		}

		// 右上
		bool isTouchPadRightUpTouched(float x, float y) {
			if (0 < x && y > 0) {
				return true;
			}
			return false;
		}

		// 左下
		bool isTouchPadLeftDownTouched(float x, float y) {
			if (x < 0 && y < 0) {
				return true;
			}
			return false;
		}

		// 右下
		bool isTouchPadRightDownTouched(float x, float y) {
			if (0 < x && y < 0) {
				return true;
			}
			return false;
		}

	}
}