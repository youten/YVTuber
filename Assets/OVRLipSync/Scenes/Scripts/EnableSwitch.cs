using UnityEngine;
using System.Collections;

public class EnableSwitch : MonoBehaviour
{
	public GameObject[] SwitchTargets;

	/// <summary>
	/// Sets the active GameObject
	/// </summary>
	/// <returns><c>true</c>, if active was set, <c>false</c> otherwise.</returns>
	/// <param name="target">Target.</param>
	public bool SetActive(int target)
	{
		if((target < 0) || (target >= SwitchTargets.Length))
			return false;

		for (int i = 0; i < SwitchTargets.Length; i++)
			SwitchTargets[i].SetActive(false);

		SwitchTargets[target].SetActive(true);

		return true;
	}
}

