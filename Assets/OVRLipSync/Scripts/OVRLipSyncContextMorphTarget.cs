/************************************************************************************
Filename    :   OVRLipSyncContextMorphTarget.cs
Content     :   This bridges the viseme output to the morph targets
Created     :   August 7th, 2015
Copyright   :   Copyright 2015 Oculus VR, Inc. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.1 (the "License"); 
you may not use the Oculus VR Rift SDK except in compliance with the License, 
which is provided at the time of installation or download, or which 
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.1 

Unless required by applicable law or agreed to in writing, the Oculus VR SDK 
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
************************************************************************************/
using UnityEngine;
using System.Collections;

public class OVRLipSyncContextMorphTarget : MonoBehaviour 
{	
	// PUBLIC

	// Manually assign the skinned mesh renderer to this script
	public SkinnedMeshRenderer skinnedMeshRenderer = null;
	
	// Set the blendshape index to go to (-1 means there is not one assigned)
	public int [] VisemeToBlendTargets = new int[OVRLipSync.VisemeCount];

	// enable/disable sending signals to viseme engine
	public bool enableVisemeSignals = false;

	// button presses (1 through 0)that will send a signal to the lip-sync engine to trigger a viseme
	public int [] KeySendVisemeSignal = new int[10];

	// smoothing amount
	public int SmoothAmount = 100; 

	// PRIVATE

	// Look for a lip-sync Context (should be set at the same level as this component)
	private OVRLipSyncContextBase lipsyncContext = null;


	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		// morph target needs to be set manually; possibly other components will need the same
		if(skinnedMeshRenderer == null)
		{
			Debug.Log("LipSyncContextMorphTarget.Start WARNING: Please set required public components!");
			return;
		}

		// make sure there is a phoneme context assigned to this object
		lipsyncContext = GetComponent<OVRLipSyncContextBase>();
		if(lipsyncContext == null)
		{
			Debug.Log("LipSyncContextMorphTarget.Start WARNING: No phoneme context component set to object");
		}

		// Send smoothing amount to context
		lipsyncContext.Smoothing = SmoothAmount;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () 
	{
		if((lipsyncContext != null) && (skinnedMeshRenderer != null))
		{
			// trap inputs and send signals to phoneme engine for testing purposes

			// get the current viseme frame
			OVRLipSync.Frame frame = lipsyncContext.GetCurrentPhonemeFrame();
			if (frame != null)
			{
				SetVisemeToMorphTarget(frame);
			}

			// TEST visemes by capturing key inputs and sending a signal
			CheckForKeys();
		}
	}
	
	/// <summary>
	/// Sends the signals.
	/// </summary>
	void CheckForKeys()
	{
		if (enableVisemeSignals)
		{
			// Capture buttons 1 through 0 (1 = 0, 0 = 10 :) )
			CheckVisemeKey(KeyCode.Alpha1, 0, 100); 
			CheckVisemeKey(KeyCode.Alpha2, 1, 100); 
			CheckVisemeKey(KeyCode.Alpha3, 2, 100); 
			CheckVisemeKey(KeyCode.Alpha4, 3, 100); 
			CheckVisemeKey(KeyCode.Alpha5, 4, 100); 
			CheckVisemeKey(KeyCode.Alpha6, 5, 100); 
			CheckVisemeKey(KeyCode.Alpha7, 6, 100); 
			CheckVisemeKey(KeyCode.Alpha8, 7, 100); 
			CheckVisemeKey(KeyCode.Alpha9, 8, 100); 
			CheckVisemeKey(KeyCode.Alpha0, 9, 100); 
			CheckVisemeKey(KeyCode.Q,     10, 100); 
			CheckVisemeKey(KeyCode.W,     11, 100); 
			CheckVisemeKey(KeyCode.E,     12, 100); 
			CheckVisemeKey(KeyCode.R,     13, 100); 
			CheckVisemeKey(KeyCode.T,     14, 100); 
		}
	}
	
	/// <summary>
	/// Sets the viseme to morph target.
	/// </summary>
	void SetVisemeToMorphTarget(OVRLipSync.Frame frame)
	{
		for (int i = 0; i < VisemeToBlendTargets.Length; i++)
		{
			if(VisemeToBlendTargets[i] != -1)
			{
				// Viseme blend weights are in range of 0->1.0, we need to make range 100
				skinnedMeshRenderer.SetBlendShapeWeight(VisemeToBlendTargets[i], frame.Visemes[i] * 100.0f);
			}
		}
	}
	
	/// <summary>
	/// Sends the viseme signal.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="viseme">Viseme.</param>
	/// <param name="arg1">Arg1.</param>
	void CheckVisemeKey(KeyCode key, int viseme, int amount)
	{
		if (Input.GetKeyDown(key))
		{
			lipsyncContext.SetVisemeBlend(KeySendVisemeSignal[viseme], amount);		
		}
		if (Input.GetKeyUp(key))
		{
			lipsyncContext.SetVisemeBlend(KeySendVisemeSignal[viseme], 0);
		}
	}
}
