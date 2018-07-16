// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for youten-yume2 morph
namespace YVTuber {
	public class MorphController : MonoBehaviour {
		Morph morph;

		void Awake () {
			// for youten-yume2 SkinnedMeshRenderer named "Body",
			// for Alicia VRM SKinnedMeshRenderer named "face"
			morph = new Morph(GameObject.Find("face").GetComponent<SkinnedMeshRenderer>(),
				GetComponent<AutoBlink>());
		}

		void Update () {
			if (XRVive.isLeftTriggerTouchGetKey() && XRVive.isLeftTrackPadPressGetKey()) {
				if (XRVive.isLeftTrackPadLeftUpTouched()) {
					morph.setEyeJitome ();
				} else if (XRVive.isLeftTrackPadRightUpTouched()) {
					morph.setEyeX ();
				} else if (XRVive.isLeftTrackPadLeftDownTouched()) {
					morph.resetMorph ();
				} else if (XRVive.isLeftTrackPadRightDownTouched()) {
					morph.resetMorph ();
				}
			}
			if (XRVive.isRightTriggerTouchGetKey() && XRVive.isRightTrackPadPressGetKey()) {
				if (XRVive.isRightTrackPadLeftUpTouched()) {
					morph.setEyeSmall ();
				} else if (XRVive.isRightTrackPadRightUpTouched()) {
					morph.setEyeLine ();
				} else if (XRVive.isRightTrackPadLeftDownTouched()) {
					morph.setEyeSmile ();
				} else if (XRVive.isRightTrackPadRightDownTouched()) {
					morph.setEyeSmile ();
				}
			}
		}
	}
}