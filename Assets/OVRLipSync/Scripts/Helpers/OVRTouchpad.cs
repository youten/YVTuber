/************************************************************************************
Filename    :   OVRTouchpad.cs
Content     :   Interface to touchpad
Created     :   November 13, 2013
Authors     :   Peter Giokaris

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Use of this software is subject to the terms of the Oculus LLC license
agreement provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

************************************************************************************/
using UnityEngine;
using System;

//-------------------------------------------------------------------------------------
// ***** OVRTouchpad
//
// OVRTouchpad is an interface class to a touchpad.
//
static internal class OVRTouchpad
{		
	//-------------------------
	// Input enums
	public enum TouchEvent { SingleTap, DoubleTap, Left, Right, Up, Down };

	// mouse
	static Vector3 moveAmountMouse;
	static float   minMovMagnitudeMouse = 25.0f;

	//Disable the unused variable warning
#pragma warning disable 0414

	//Ensures that the TouchpadHelper will be created automatically upon start of the game.
	static private OVRTouchpadHelper touchpadHelper = 
	( new GameObject("OVRTouchpadHelper") ).AddComponent< OVRTouchpadHelper >();

#pragma warning restore 0414

	// We will call this to create the TouchpadHelper class. This will
	// add the Touchpad game object into the world and we can call into 
	// TouchEvent static functions to hook delegates into for touch capture
	static public void Create()
	{
		// Does nothing but call constructor to add game object into scene	
	}

	// Update
	static public void Update()
	{
		// MOUSE INPUT

		if(Input.GetMouseButtonDown(0))
		{
			moveAmountMouse = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			moveAmountMouse -= Input.mousePosition;
			HandleInputMouse(ref moveAmountMouse);
		}
	}
	
	// OnDisable
	static public void OnDisable()
	{
	}
	
	// HandleInputMouse
	static void HandleInputMouse(ref Vector3 move)
	{
		if( move.magnitude < minMovMagnitudeMouse)
			OVRMessenger.Broadcast<TouchEvent>("Touchpad", TouchEvent.SingleTap);
		else 
		{
			move.Normalize();

			// Left/Right
			if(Mathf.Abs(move.x) > Mathf.Abs (move.y))
				if(move.x > 0.0f)
					OVRMessenger.Broadcast<TouchEvent>("Touchpad", TouchEvent.Left);
				else
					OVRMessenger.Broadcast<TouchEvent>("Touchpad", TouchEvent.Right);
			// Up/Down
			else
				if(move.y > 0.0f)
					OVRMessenger.Broadcast<TouchEvent>("Touchpad", TouchEvent.Down);
				else
					OVRMessenger.Broadcast<TouchEvent>("Touchpad", TouchEvent.Up);
		}
	}
}

//-------------------------------------------------------------------------------------
// ***** OVRTouchpadHelper
//
// This singleton class gets created and stays resident in the application. It is used to 
// trap the touchpad values, which get broadcast to any listener on the "Touchpad" channel.
//
// This class also demontrates how to make calls from any class that needs these events by
// setting up a listener to "Touchpad" channel.
public sealed class OVRTouchpadHelper : MonoBehaviour 
{
	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	void Start ()
	{
		// Add a listener to the OVRMessenger for testing
		OVRMessenger.AddListener<OVRTouchpad.TouchEvent>("Touchpad", LocalTouchEventCallback);
	}


	void Update ()
	{
		OVRTouchpad.Update();
	}

 
	public void OnDisable() 
	{
		OVRTouchpad.OnDisable();
	}
	
	// LocalTouchEventCallback
	void LocalTouchEventCallback(OVRTouchpad.TouchEvent touchEvent)
	{
		switch(touchEvent)
		{
			case(OVRTouchpad.TouchEvent.SingleTap):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-SINGLE TAP");
			break;

			case(OVRTouchpad.TouchEvent.DoubleTap):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-DOUBLE TAP");
			break;

			case(OVRTouchpad.TouchEvent.Left):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-SWIPE LEFT");
			break;

			case(OVRTouchpad.TouchEvent.Right):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-SWIPE RIGHT");
			break;
		
			case(OVRTouchpad.TouchEvent.Up):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-SWIPE UP");
			break;

			case(OVRTouchpad.TouchEvent.Down):
//			OVRLipSyncDebugConsole.Clear();
//			OVRLipSyncDebugConsole.ClearTimeout(1.5f);
//			OVRLipSyncDebugConsole.Log("TP-SWIPE DOWN");
			break;
		}
	}

}

