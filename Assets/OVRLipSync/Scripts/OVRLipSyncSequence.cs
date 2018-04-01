using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Sequence - holds ordered entries for playback
[System.Serializable]
public class OVRLipSyncSequence : ScriptableObject
{
	public List<OVRLipSync.Frame> entries = new List<OVRLipSync.Frame>();
	public float length;	// in seconds
	
	public OVRLipSync.Frame GetFrameAtTime(float time)
	{
		OVRLipSync.Frame frame = null;
		if (time < length && entries.Count > 0)
		{
			// todo: we could blend frame output here if we wanted.
			float percentComplete = time / length;
			frame = entries[(int)(entries.Count * percentComplete)];
		}
		return frame;
	}
	
#if UNITY_EDITOR

	private static readonly int sSampleSize = 1024;
	
	public static OVRLipSyncSequence CreateSequenceFromAudioClip(AudioClip clip)
	{
		OVRLipSyncSequence sequence = null;
		
		if (clip.loadType != AudioClipLoadType.DecompressOnLoad || clip.channels > 2)
		{
			// todo: just fix the clip
			Debug.LogError("Cannot process phonemes from an audio clip unless its load type is set to DecompressOnLoad.");
		}
		else
		{
			if (OVRLipSync.Initialize(clip.frequency, sSampleSize) != OVRLipSync.Result.Success)
			{
				Debug.LogError("Could not create Lip Sync engine.");
			}
			else 
			{
				uint context = 0;
				OVRLipSync.Result result = OVRLipSync.CreateContext(ref context, OVRLipSync.ContextProviders.Main);
				if (result != OVRLipSync.Result.Success)
				{
					Debug.LogError("Could not create Phoneme context. (" + result + ")");
					OVRLipSync.Shutdown();
				}
				else
				{
					List<OVRLipSync.Frame> frames = new List<OVRLipSync.Frame>();
					float[] samples = new float[sSampleSize * clip.channels];
					int totalSamples = clip.samples;
					for (int x = 0; x < totalSamples; x += sSampleSize)
					{
						// GetData loops at the end of the read.  Prevent that when it happens.
						if (x + samples.Length > totalSamples)
						{
							samples = new float[(totalSamples - x) * clip.channels];
						}
						clip.GetData(samples, x);
						OVRLipSync.Frame frame = new OVRLipSync.Frame();
						if (clip.channels == 2)
						{
							// interleaved = stereo data, alternating floats
							OVRLipSync.ProcessFrameInterleaved(context, samples, 0, frame);	
						}
						else
						{
							// mono
							OVRLipSync.ProcessFrame(context, samples, 0, frame);	
						}
						
						frames.Add(frame);
					}
					
					Debug.Log(clip.name + " produced " + frames.Count + " viseme frames, playback rate is " + (frames.Count / clip.length) + " fps");
					OVRLipSync.DestroyContext(context);
					OVRLipSync.Shutdown();
					
					sequence = ScriptableObject.CreateInstance<OVRLipSyncSequence>();
			 		sequence.entries = frames;
			 		sequence.length = clip.length;
				}
			}
		}
		return sequence;
	}
#endif
};