using UnityEngine;
using System.Collections;

public class gamesound : MonoBehaviour {

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1){
			Destroy(gameObject);
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
