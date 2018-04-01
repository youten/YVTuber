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
//-------------------------------------------------------------------------------------// ***** OVRPhonemeContext///// <summary>/// OVRPhonemeContext interfaces into the Oculus phoneme recognizer. /// This component should be added into the scene once for each Audio Source. ////// </summary>
public class OVRLipSyncContext : OVRLipSyncContextBase
{
    // * * * * * * * * * * * * *    // Public members    public float gain = 1.0f;	public bool audioMute = true;    public KeyCode loopback = KeyCode.L;    public KeyCode debugVisemes = KeyCode.D;    public bool showVisemes = false;    public bool delayCompensate = false;
    // * * * * * * * * * * * * *    // Private members    // Holds the state of previous frame
    private OVRLipSync.Frame debugFrame = new OVRLipSync.Frame();
    private float debugFrameTimer = 0.0f;
    private float debugFrameTimeoutValue = 0.1f;	// sec.    /// <summary>    /// Start this instance.    /// Note: make sure to always have a Start function for classes that have editor scripts.    /// </summary>
    void Start()
    {       // Add a listener to the OVRMessenger for touch events        OVRMessenger.AddListener<OVRTouchpad.TouchEvent>("Touchpad", LocalTouchEventCallback);    }
    /// <summary>    /// Run processes that need to be updated in our game thread    /// </summary>    void Update()
    {		// HACK: disable control debug-mode by key or mouse-click 		return;		/*        // Turn loopback on/off        if (Input.GetKeyDown(loopback))
		{            audioMute = !audioMute;            OVRLipSyncDebugConsole.Clear();            OVRLipSyncDebugConsole.ClearTimeout(1.5f);
			if (audioMute) {				OVRLipSyncDebugConsole.Log ("LOOPBACK MODE: ENABLED");
			} else {				OVRLipSyncDebugConsole.Log ("LOOPBACK MODE: DISABLED");			}        }        else if (Input.GetKeyDown(debugVisemes))        {            showVisemes = !showVisemes;
			if (showVisemes) {				Debug.Log ("DEBUG SHOW VISEMES: ENABLED");			}            else {                OVRLipSyncDebugConsole.Clear();                Debug.Log("DEBUG SHOW VISEMES: DISABLED");            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))        {            gain -= 1.0f;            if (gain < 1.0f) gain = 1.0f;
            string g = "LINEAR GAIN: ";            g += gain;
            OVRLipSyncDebugConsole.Clear();            OVRLipSyncDebugConsole.Log(g);            OVRLipSyncDebugConsole.ClearTimeout(1.5f);        }        else if (Input.GetKeyDown(KeyCode.RightArrow))        {            gain += 1.0f;
            if (gain > 15.0f)                gain = 15.0f;
            string g = "LINEAR GAIN: ";            g += gain;
            OVRLipSyncDebugConsole.Clear();            OVRLipSyncDebugConsole.Log(g);            OVRLipSyncDebugConsole.ClearTimeout(1.5f);        }        DebugShowVisemes();		*/    }
    /// <summary>    /// Raises the audio filter read event.    /// </summary>    /// <param name="data">Data.</param>    /// <param name="channels">Channels.</param>    void OnAudioFilterRead(float[] data, int channels)    {        // Do not spatialize if we are not initialized, or if there is no        // audio source attached to game object
        if ((OVRLipSync.IsInitialized() != OVRLipSync.Result.Success) || audioSource == null)            return;
        // increase the gain of the input to get a better signal input        for (int i = 0; i < data.Length; ++i)            data[i] = data[i] * gain;
        // Send data into Phoneme context for processing (if context is not 0)        lock (this)        {            if (Context != 0)            {                OVRLipSync.Flags flags = 0;
                // Set flags to feed into process                if (delayCompensate == true)                    flags |= OVRLipSync.Flags.DelayCompensateAudio;
				OVRLipSync.Frame frame = this.Frame;                OVRLipSync.ProcessFrameInterleaved(Context, data, flags, frame);            }        }
        // Turn off output (so that we don't get feedback from mics too close to speakers)        if (audioMute == true)        {            for (int i = 0; i < data.Length; ++i)                data[i] = data[i] * 0.0f;        }    }
    // * * * * * * * * * * * * *    // Public Functions    /// <summary>    /// Debugs the show visemes.    /// </summary>    void DebugShowVisemes()    {        if (showVisemes == false)            return;
        debugFrameTimer -= Time.deltaTime;        if (debugFrameTimer < 0.0f)        {            debugFrameTimer += debugFrameTimeoutValue;            debugFrame.CopyInput(Frame);        }
        string seq = "";        for (int i = 0; i < debugFrame.Visemes.Length; i++)        {            if (i < 10)                seq += "0";
            seq += i;            seq += ":";
            int count = (int)(50.0f * debugFrame.Visemes[i]);            for (int c = 0; c < count; c++)                seq += "*";
            //seq += (int)(debugFrame.Visemes[i] * 100.0f);             seq += "\n";        }        OVRLipSyncDebugConsole.Clear();        OVRLipSyncDebugConsole.Log(seq);    }

    // LocalTouchEventCallback    void LocalTouchEventCallback(OVRTouchpad.TouchEvent touchEvent)    {        string g = "LINEAR GAIN: ";
        switch (touchEvent)        {            case (OVRTouchpad.TouchEvent.SingleTap):                audioMute = !audioMute;                OVRLipSyncDebugConsole.Clear();                OVRLipSyncDebugConsole.ClearTimeout(1.5f);
                if (audioMute)                    OVRLipSyncDebugConsole.Log("LOOPBACK MODE: ENABLED");                else                    OVRLipSyncDebugConsole.Log("LOOPBACK MODE: DISABLED");
                break;
            case (OVRTouchpad.TouchEvent.Up):                gain += 1.0f;                if (gain > 15.0f)                    gain = 15.0f;
                g += gain;
                OVRLipSyncDebugConsole.Clear();                OVRLipSyncDebugConsole.Log(g);                OVRLipSyncDebugConsole.ClearTimeout(1.5f);				break;
            case (OVRTouchpad.TouchEvent.Down):                gain -= 1.0f;                if (gain < 1.0f) gain = 1.0f;
                g += gain;
                OVRLipSyncDebugConsole.Clear();                OVRLipSyncDebugConsole.Log(g);                OVRLipSyncDebugConsole.ClearTimeout(1.5f);
                break;
        }
    }
}