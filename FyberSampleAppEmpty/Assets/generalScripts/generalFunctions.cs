using UnityEngine;
using System.Collections;

public class generalFunctions : MonoBehaviour {
	

	void deactivateSelfObject() {
		this.gameObject.SetActive (false);
	}

	public void playsound(AudioClip audiotoplay) {
		AudioSource.PlayClipAtPoint(audiotoplay, Camera.main.transform.position, 1f);
	}
}
