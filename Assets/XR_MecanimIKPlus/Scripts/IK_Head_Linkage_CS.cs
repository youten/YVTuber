using System.Collections;
using UnityEngine;

namespace MecanimIKPlus
{
	
	public class IK_Head_Linkage_CS : MonoBehaviour
	{

		public Transform eyeTransform;
		public Transform neckTransform;
		public Transform headTransform;
		public Vector3 fixingAngles; // Unity-Chan => 0.0f, -90.0f, -90.0f


		void LateUpdate ()
		{
			Rotate ();
		}

		void Rotate ()
		{
			neckTransform.rotation = Quaternion.Lerp(Quaternion.identity, eyeTransform.rotation, 0.5f) * Quaternion.Euler(fixingAngles);
			headTransform.rotation = eyeTransform.rotation * Quaternion.Euler(fixingAngles);
		}

	}

}
