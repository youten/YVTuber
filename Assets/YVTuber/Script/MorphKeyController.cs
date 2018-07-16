// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for youten-yume2 morph
namespace YVTuber {
	public class MorphKeyController : MonoBehaviour {
		public static bool keyEnable = true; // true時のみキー入力操作が有効
		Morph morph;

		void Awake () {
			// for youten-yume2 SkinnedMeshRenderer named "Body",
			// for Alicia VRM SKinnedMeshRenderer named "face"
			morph = new Morph(GameObject.Find("face").GetComponent<SkinnedMeshRenderer>(),
				GetComponent<AutoBlink>());
		}

		void Update () {
			if (keyEnable) {
				if (Input.GetKeyDown (KeyCode.Space)) { // reset
					morph.resetMorph ();
				} else if (Input.GetKeyDown (KeyCode.H)) { // half close
					morph.setEyeHalfClose ();
				} else if (Input.GetKeyDown (KeyCode.J)) { // eye jitome
					morph.setEyeJitome ();
				} else if (Input.GetKeyDown (KeyCode.L)) { // eye line --
					morph.setEyeLine ();
				} else if (Input.GetKeyDown (KeyCode.S)) { // small eye
					morph.setEyeSmall ();
				} else if (Input.GetKeyDown (KeyCode.X)) { // >< eye
					morph.setEyeX ();
				} else if (Input.GetKeyDown (KeyCode.N)) { // smile
					morph.setEyeSmile ();
				}
			}
		}
	}
}