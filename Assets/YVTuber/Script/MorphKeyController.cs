// Copyright 2018 @youten_redo
// MIT 3-Clause https://opensource.org/licenses/BSD-3-Clause
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for youten-yum2 morph
namespace YVTuber {
	public class MorphKeyController : MonoBehaviour {
		public static bool keyEnable = true; // true時のみキー入力操作が有効

		SkinnedMeshRenderer refFace;
		static int FACE_INDEX_EYECLOSEL = 0;
		static int FACE_INDEX_EYECLOSER = 1;
		static int FACE_INDEX_EYESMILE = 27;
		static int FACE_INDEX_EYEX = 32;
		static int FACE_INDEX_EYELINE = 33;
		static int FACE_INDEX_EYESMALL = 37;
		// モーフ変化と衝突するので自動まばたきのon/off制御を行う
		AutoBlink refAutoBlink;

		void Awake ()
		{
			refFace = GameObject.Find("Body").GetComponent<SkinnedMeshRenderer>();
			refAutoBlink = GetComponent<AutoBlink>();
		}

		void Update () {
			if (keyEnable) {
				if (Input.GetKeyDown (KeyCode.Space)) { // reset
					resetMorph ();
				} else if (Input.GetKeyDown (KeyCode.H)) { // half close
					setEyeHalfClose ();
				} else if (Input.GetKeyDown (KeyCode.L)) { // eye line --
					setEyeLine ();
				} else if (Input.GetKeyDown (KeyCode.S)) { // small eye
					setEyeSmall ();
				} else if (Input.GetKeyDown (KeyCode.X)) { // >< eye
					setEyeX ();
				} else if (Input.GetKeyDown (KeyCode.N)) { // smile
					setEyeSmile ();
				}
			}
		}

		public void resetMorph() {
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSEL, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSER, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYESMILE, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYEX, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYELINE, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYESMALL, 0);
			refAutoBlink.isActive = true;
		}

		public void setEyeHalfClose() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSEL, 60);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSER, 60);
			refAutoBlink.isActive = false;
		}
			
		public void setEyeLine() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYELINE, 100);
			refAutoBlink.isActive = false;
		}

		public void setEyeSmall() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYESMALL, 50);
			refAutoBlink.isActive = false;
		}

		public void setEyeX() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYEX, 100);
			refAutoBlink.isActive = false;
		}

		public void setEyeSmile() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYESMILE, 100);
			refAutoBlink.isActive = false;
		}
	}
}