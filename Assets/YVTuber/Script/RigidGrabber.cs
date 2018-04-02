// Copyright 2018 @youten_redo
// MIT https://opensource.org/licenses/MIT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ref. http://vr-lab.voyagegroup.com/entry/2016/10/13/003204
// RigidBody Grabber for XR Mecanim IK Plus
// Usage:
// - Attach this script to IK_Target_Hand_L(Is Left:on) and IK_Target_Hand_R(Is Left:off)
// - Need to grab target, Rigidbody is attached.
public class RigidGrabber : MonoBehaviour {
	public bool isLeft = true;
	GameObject grabbed;
	FixedJoint joint;

	void Start () {
		joint = gameObject.GetComponent<FixedJoint> ();
		if (joint == null) {
			joint = gameObject.AddComponent<FixedJoint> ();
		}
		joint.breakForce = 20000;
		joint.breakTorque = 20000;
		BoxCollider boxCollider = gameObject.GetComponent<BoxCollider> ();
		if (boxCollider == null) {
			boxCollider = gameObject.AddComponent<BoxCollider> ();
		}
		boxCollider.center = new Vector3 (0, 0, 0.1f);
		boxCollider.size = new Vector3 (0.1f, 0.1f, 0.1f);
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		if (rigidBody == null) {
			rigidBody = gameObject.AddComponent<Rigidbody> ();
		}
		rigidBody.useGravity = false;
		rigidBody.isKinematic = true;
	}

	void Update () {
		if (isLeft) {
			if (XRVive.isLeftTriggerTouchGetKeyDown()) {
				grab ();
			}
			if (XRVive.isLeftTriggerTouchGetKeyUp()) {
				release ();
			}
		} else {
			if (XRVive.isRightTriggerTouchGetKeyDown()) {
				grab ();
			}
			if (XRVive.isRightTriggerTouchGetKeyUp()) {
				release ();
			}
		}
	} 

	void grab() {
		if (grabbed != null && joint.connectedBody == null) {
			joint.connectedBody = grabbed.GetComponent<Rigidbody> ();
			joint.connectedBody.isKinematic = false;
			joint.connectedBody.useGravity = true;
		}
	}

	void release() {
		if (joint.connectedBody != null) {
			joint.connectedBody.useGravity = false;
			joint.connectedBody = null;
		}
	}

	void OnTriggerEnter(Collider other) {
		grabbed = other.gameObject;
	}

	void OnTriggerExit(Collider other) {
		grabbed = null;
	}
}
