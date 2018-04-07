// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace YVTuber {
	public class XRDeviceTracker : MonoBehaviour {
		public enum XRNodeIndex {
			Id0 = 0,
			Id1,
			Id2,
			Id3,
			Id4,
			Id5,
			Id6,
			Id7,
			Id8,
			Id9,
			Id10,
			Id11,
			Id12,
			Id13,
			Id14,
			Id15,
			Id16,
			Id17,
			Id18,
			Id19
		}
		[SerializeField]XRNodeIndex nodeIndex = XRNodeIndex.Id8;
		public Transform targetTransform = null;
		public Vector3 offsetAngles;

		List<XRNodeState> nodeStates = new List<XRNodeState>();

		void Start () {
		}

		void Update () {
			InputTracking.GetNodeStates (nodeStates);
			if (nodeStates.Count > (int)nodeIndex) {
				Vector3 pos;
				nodeStates [(int)nodeIndex].TryGetPosition (out pos);
				if (targetTransform == null) {				
					transform.position = pos;
				} else {
					targetTransform.position = pos;
				}
				Quaternion rot;
				nodeStates [(int)nodeIndex].TryGetRotation (out rot);
				if (targetTransform == null) {
					transform.rotation = rot;
				} else {
					targetTransform.rotation = rot * Quaternion.Euler (offsetAngles);
				}
			}
		}
	}
}
