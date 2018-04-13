// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for youten-yume2 morph
namespace YVTuber {
	public class Morph {
		SkinnedMeshRenderer refFace;
		static int FACE_INDEX_EYECLOSEL = 0;
		static int FACE_INDEX_EYECLOSER = 1;
		static int FACE_INDEX_EYESMILE = 28;
		static int FACE_INDEX_EYEX = 35;
		static int FACE_INDEX_EYELINE = 36;
		static int FACE_INDEX_EYEJITOME = 38;
		static int FACE_INDEX_EYESMALL = 41;
		// モーフ変化と衝突するので自動まばたきのon/off制御を行う
		AutoBlink refAutoBlink;

		public Morph(SkinnedMeshRenderer targetSkinnedMeshRenderer, AutoBlink autoBlink) {
			refFace = targetSkinnedMeshRenderer;
			refAutoBlink = autoBlink;
		}

		public void resetMorph() {
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSEL, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYECLOSER, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYESMILE, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYEX, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYELINE, 0);
			refFace.SetBlendShapeWeight (FACE_INDEX_EYEJITOME, 0);
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

		public void setEyeJitome() {
			resetMorph ();
			refFace.SetBlendShapeWeight (FACE_INDEX_EYEJITOME, 100);
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