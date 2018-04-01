using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

namespace MecanimIKPlus
{
	
	public class XR_Basic_Function_CS : MonoBehaviour
	{

		public string recenterInputName = "Submit";
		public string reloadInputName = "Fire3";

		void Start ()
		{
			XRDevice.SetTrackingSpaceType (TrackingSpaceType.Stationary);
		}

		void Update ()
		{
			Functions ();
		}

		void Functions ()
		{
			// Recenter.
			if (Input.GetButtonDown (recenterInputName)) {
				InputTracking.Recenter ();
			}
			// Reload the scene.
			if (Input.GetButtonDown (reloadInputName)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			// Quit
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Application.Quit ();
			}
		}

	}

}
