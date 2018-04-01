using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class OVRLipSyncTestAudio : MonoBehaviour 
{
	public AudioSource audioSource = null;

	// Use this for initialization
	void Start () 
	{
		// First thing to do, cache the unity audio source (can be managed by the
		// user if audio source can change)
		if (!audioSource) audioSource = GetComponent<AudioSource>();
		if (!audioSource) return; // this should never happen

		string path = Application.dataPath;
		path += "/../";
		path += "TestViseme.wav";

		WWW www = new WWW( "file:///" + path );
		while ( !www.isDone )
		{
			Debug.Log(www.progress);
		}

		if(www.GetAudioClip() != null)
		{
			audioSource.clip = www.GetAudioClip();
			audioSource.loop = true; 	// Set the AudioClip to loop
			audioSource.mute = false; 	// Mute the sound, we don't want the player to hear it
			audioSource.Play ();
		}
	}	
}
