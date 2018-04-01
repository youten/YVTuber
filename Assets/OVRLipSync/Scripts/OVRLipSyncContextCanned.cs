/************************************************************************************
Filename    :   OVRLipSyncContext.cs
Content     :   Interface to Oculus Lip-Sync engine
Created     :   August 6th, 2015
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
using System;
using System.Runtime.InteropServices;


[RequireComponent(typeof(AudioSource))]

//-------------------------------------------------------------------------------------
// ***** OVRLipSyncContextCanned
//
/// <summary>
/// OVRLipSyncContextCanned drives a canned phoneme sequence based on a pre-generated asset. 
///
/// </summary>
public class OVRLipSyncContextCanned : OVRLipSyncContextBase
{
   	public OVRLipSyncSequence currentSequence;
   	
    /// <summary>
    /// Run processes that need to be updated in our game thread
    /// </summary>
    void Update()
    {
		if (audioSource.isPlaying && currentSequence != null)
		{
			OVRLipSync.Frame currentFrame = currentSequence.GetFrameAtTime(audioSource.time);
			this.Frame.CopyInput(currentFrame);
		}
    }
}