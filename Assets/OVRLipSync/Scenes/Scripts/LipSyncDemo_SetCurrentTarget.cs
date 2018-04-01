using UnityEngine;
using System.Collections;

public class LipSyncDemo_SetCurrentTarget : MonoBehaviour 
{
	public EnableSwitch[] SwitchTargets;

	private int targetSet = 0;
	
	// Use this for initialization
	void Start () 
	{
		// Add a listener to the OVRMessenger for touch events
		OVRMessenger.AddListener<OVRTouchpad.TouchEvent>("Touchpad", LocalTouchEventCallback);

		// Initialize the proper target set
		targetSet = 0;
		SwitchTargets[0].SetActive(0);
		SwitchTargets[1].SetActive(0);
	}
	
	// Update is called once per frame
	// Logic for LipSync_Demo
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			targetSet = 0;
			SetCurrentTarget();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			targetSet = 1;
			SetCurrentTarget();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			targetSet = 2;
			SetCurrentTarget();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			targetSet = 3;
			SetCurrentTarget();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			targetSet = 4;
			SetCurrentTarget();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			targetSet = 5;
			SetCurrentTarget();
		}

		// Close app
		if(Input.GetKeyDown (KeyCode.Escape))
		   Application.Quit();
	}

	/// <summary>
	/// Sets the current target.
	/// </summary>
	void SetCurrentTarget()
	{
		switch(targetSet)
		{
		case(0):
			SwitchTargets[0].SetActive(0);
			SwitchTargets[1].SetActive(0);
			break;
		case(1):
			SwitchTargets[0].SetActive(0);
			SwitchTargets[1].SetActive(1);
			break;
		case(2):
			SwitchTargets[0].SetActive(1);
			SwitchTargets[1].SetActive(2);
			break;
		case(3):
			SwitchTargets[0].SetActive(1);
			SwitchTargets[1].SetActive(3);
			break;
		case(4):
			SwitchTargets[0].SetActive(2);
			SwitchTargets[1].SetActive(4);
			break;
		case(5):
			SwitchTargets[0].SetActive(2);
			SwitchTargets[1].SetActive(5);
			break;
			
		}
	}
	
	/// <summary>
	/// Local touch event callback.
	/// </summary>
	/// <param name="touchEvent">Touch event.</param>
	void LocalTouchEventCallback(OVRTouchpad.TouchEvent touchEvent)
	{
		switch(touchEvent)
		{
			case(OVRTouchpad.TouchEvent.Left):
		
			targetSet--;
			if(targetSet < 0)
				targetSet = 3;

			SetCurrentTarget();

			break;
			
			case(OVRTouchpad.TouchEvent.Right):

			targetSet++;
			if(targetSet > 3)
				targetSet = 0;

			SetCurrentTarget();

			break;
		}
	}
}
