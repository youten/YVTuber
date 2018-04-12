// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Unity XR APIでViveを扱う
namespace YVTuber {
	public static class XRVive {
		// Unity XR APIはHTC Vive, Oculus Touch, Valve Knucklesで共通化するために
		// 少し遠回りなIFしか用意されてないので、適当なラッパを作ります。

		// https://docs.unity3d.com/Manual/OpenVRControllers.html
		// また、Axis入力はEdit -> Project Settings -> InputでInput Managerに対して設定の追加が必要です
		// ＜要追加＞
		// Name:"Grip L" Type:Joystick Axis Axis:11th axis
		// Name:"Grip R" Type:Joystick Axis Axis:12th axis
		// Name:"RightHorizontal" Type:Joystick Axis Axis:4th axis
		// Name:"RightVertical" Type:Joystick Axis Axis:5th axis Invert:on **
		// **Right TrackPadのVerticalだけ上下が反転してる（2017.3.1p4で確認、他ver.では直ってるかも）ので注意。
		// ＜既存設定で利用可能＞
		// Name:"Horizontal" Type:Joystick Axis Axis:X axis
		// Name:"Vertical" Type:Joystick Axis Axis:Y axis

		// left trackpad press
		public static bool isLeftTrackPadPressGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton8);
		}
		public static bool isLeftTrackPadPressGetKey() {
			return Input.GetKey (KeyCode.JoystickButton8);
		}
		public static bool isLeftTrackPadPressGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton8);
		}
		// left trackpad touch
		public static bool isLeftTrackPadTouchGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton16);
		}
		public static bool isLeftTrackPadTouchGetKey() {
			return Input.GetKey (KeyCode.JoystickButton16);
		}
		public static bool isLeftTrackPadTouchGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton16);
		}
		// left trigger touch
		public static bool isLeftTriggerTouchGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton14);
		}
		public static bool isLeftTriggerTouchGetKey() {
			return Input.GetKey (KeyCode.JoystickButton14);
		}
		public static bool isLeftTriggerTouchGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton14);
		}

		// right trackpad press
		public static bool isRightTrackPadPressGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton9);
		}
		public static bool isRightTrackPadPressGetKey() {
			return Input.GetKey (KeyCode.JoystickButton9);
		}
		public static bool isRightTrackPadPressGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton9);
		}
		// right trackpad touch
		public static bool isRightTrackPadTouchGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton17);
		}
		public static bool isRightTrackPadTouchGetKey() {
			return Input.GetKey (KeyCode.JoystickButton17);
		}
		public static bool isRightTrackPadTouchGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton17);
		}
		// right trigger touch
		public static bool isRightTriggerTouchGetKeyUp() {
			return Input.GetKeyUp (KeyCode.JoystickButton15);
		}
		public static bool isRightTriggerTouchGetKey() {
			return Input.GetKey (KeyCode.JoystickButton15);
		}
		public static bool isRightTriggerTouchGetKeyDown() {
			return Input.GetKeyDown (KeyCode.JoystickButton15);
		}

		public static Vector2 getLeftTrackPadPos() {
			float x = Input.GetAxis ("Horizontal");
			float y = Input.GetAxis ("Vertical");
			return new Vector2 (x, y);
		}

		public static Vector2 getRightTrackPadPos() {
			float x = Input.GetAxis ("RightHorizontal");
			float y = Input.GetAxis ("RightVertical");
			return new Vector2 (x, y);
		}

		// left controller
		public static bool isLeftTrackPadLeftUpTouched() {
			return isTrackPadLeftUpTouched (getLeftTrackPadPos ());
		}
		public static bool isLeftTrackPadRightUpTouched() {
			return isTrackPadRightUpTouched (getLeftTrackPadPos ());
		}
		public static bool isLeftTrackPadLeftDownTouched() {
			return isTrackPadLeftDownTouched (getLeftTrackPadPos ());
		}
		public static bool isLeftTrackPadRightDownTouched() {
			return isTrackPadRightDownTouched (getLeftTrackPadPos ());
		}

		// right controller
		public static bool isRightTrackPadLeftUpTouched() {
			return isTrackPadLeftUpTouched (getRightTrackPadPos ());
		}
		public static bool isRightTrackPadRightUpTouched() {
			return isTrackPadRightUpTouched (getRightTrackPadPos ());
		}
		public static bool isRightTrackPadLeftDownTouched() {
			return isTrackPadLeftDownTouched (getRightTrackPadPos ());
		}
		public static bool isRightTrackPadRightDownTouched() {
			return isTrackPadRightDownTouched (getRightTrackPadPos ());
		}

		// 左上
		private static bool isTrackPadLeftUpTouched(Vector2 pos) {
			if (pos.x < 0 && pos.y > 0) {
				return true;
			}
			return false;
		}

		// 右上
		private static bool isTrackPadRightUpTouched(Vector2 pos) {
			if (0 < pos.x && pos.y > 0) {
				return true;
			}
			return false;
		}

		// 左下
		private static bool isTrackPadLeftDownTouched(Vector2 pos) {
			if (pos.x < 0 && pos.y < 0) {
				return true;
			}
			return false;
		}

		// 右下
		private static bool isTrackPadRightDownTouched(Vector2 pos) {
			if (0 < pos.x && pos.y < 0) {
				return true;
			}
			return false;
		}
	}
}