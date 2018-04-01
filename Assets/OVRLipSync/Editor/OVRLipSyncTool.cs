using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class OVRLipSyncTool
{
	[MenuItem("Tools/Oculus/Generate Lip Sync Assets", false, 2000000)]	
	static void GenerateLipSyncAssets()
	{
		for (int i = 0; i < Selection.objects.Length; ++i)
		 {
			 Object obj = Selection.objects[i];
			 if (obj is AudioClip)
			 {
			 	AudioClip clip = (AudioClip)obj;
			 	OVRLipSyncSequence sequence = OVRLipSyncSequence.CreateSequenceFromAudioClip(clip);
			 	if (sequence != null)
			 	{
			 		string path = AssetDatabase.GetAssetPath(obj);
			 		string newPath = path.Replace(Path.GetExtension(path), "_lipSync.asset");
			 		AssetDatabase.CreateAsset(sequence, newPath);
			 		AssetDatabase.ImportAsset(newPath);
			 	}
			 }
		 }
		 AssetDatabase.Refresh();
	}	
}