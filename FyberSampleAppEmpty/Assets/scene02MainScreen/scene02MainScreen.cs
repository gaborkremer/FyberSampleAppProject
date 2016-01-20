/*
This script is for the main screen. It allows to access the shop for gems and coins, shows a progress bar with a castle, and provides access to the actual game, which is a timed game.



*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class scene02MainScreen : MonoBehaviour {


	public GameObject progressBar; //progress bar object set in editor
	public GameObject castle; // castle object set in the editor
	public AudioClip custleFinishedSound; // sound for castle finished
	public AudioClip custleDestroySound; //sound for castle destroyed
	public float rewardFloat; //the amount of reward set in editor

	float elapsedTime = 0; //float to measure elapsed time, set to 0 at scene start
	float progressBarProcent = 0; //float to keep the fill amount of the progress bar, set to 0 at scene start
	
	int castleProgress = 1; //states of the castle set to 1 (not started) at scene start
	// 1 - not started
	// 2 - started
	// 3 - upgraded
	// 4 - upgraded more
	// 5 - finished


	
	// Update is called once per frame
	void Update () {

		//Quit app on hardware back button
		if (Input.GetButtonUp("Cancel")) {
			Application.Quit();
		}

		//check if castle is in progress (not "not started" and not "finished")
		if (castleProgress != 1 && castleProgress != 5) {
			elapsedTime += Time.deltaTime; //update elpsed time
			if (elapsedTime >= 1) { //if elpsed time is over or equal to a second
				elapsedTime -= 1; //count down 1 sec form the elapsed time
				progressBarProcent += 0.01f; //update progress bar with 1%
				progressBarProcent = Mathf.Clamp (progressBarProcent, 0, 1); //keep limits for the progress bar not to go outside 0% and 100%

			}

		}

		//check the progress of the castle 
		if (progressBarProcent >= 1/3f && progressBarProcent < 2/3f && castleProgress != 3) {
			castle.GetComponent<Animator>().SetTrigger("progresscastle"); //set trigger for next castle state
			castleProgress = 3; //change state of the castle
		}
		if (progressBarProcent >= 2/3f && progressBarProcent < 1f && castleProgress != 4) {
			castle.GetComponent<Animator>().SetTrigger("progressmorecastle"); //set trigger for next castle state
			castleProgress = 4; //change state of the castle
		}

		//check if cprogress bar is full and castle is not set to finished yet
		if (progressBarProcent == 1 && castleProgress != 5) {
			finishcastle ();  //trigger castle finish process
		}

		//update the fill amount of the porgress bar
		progressBar.GetComponent<Image> ().fillAmount = progressBarProcent;



	}


	//load different scenes set in the editor
	public void loadScene(string String) {
		Application.LoadLevel(String);
	}



	/// <summary>
	/// Speed up process triggered on click on the castle
	/// </summary>
	public void speedUp() {
		if (progressBarProcent == 0) { //check if progress is not started yet
			castleProgress = 2; //set castle to started
			castle.GetComponent<Animator>().SetTrigger("startcastle"); //trigger castle started animation
			castle.GetComponent<AudioSource>().Play(); //start to play sound ob building castle
		} else if (progressBarProcent == 1) { //check if castle if finsihed
			progressBarProcent = 0; //reset progress bar to 0
			castleProgress = 1; //set castle state as not starter
			castle.GetComponent<Animator>().SetTrigger("resetcastle"); //trigger castle not started animation
			AudioSource.PlayClipAtPoint(custleDestroySound, Camera.main.transform.position, 0.5f); //play caste destroyed sound
		} else { //if castle is in progress
			updateProgress (rewardFloat); //update prgress with the reward
		}
	}

	
	/// <summary>
	/// To udate progress if castle is clicked during it is in progress
	/// </summary>
	/// <param name="GetFloat">Get the amount of reward to update progress bar</param>
	void updateProgress (float GetFloat) {
		progressBarProcent += GetFloat; //update progress bar
		if (progressBarProcent >= 1/3f && progressBarProcent < 2/3f && castleProgress != 3) { //check the progress of the bar
			castle.GetComponent<Animator>().SetTrigger("progresscastle"); //set trigger for next castle state
			castleProgress = 3; //change state of the castle
		}
		if (progressBarProcent >= 2/3f && progressBarProcent < 1 && castleProgress != 4) { //check the progress of the bar
			castle.GetComponent<Animator>().SetTrigger("progressmorecastle"); //set trigger for next castle state
			castleProgress = 4; //change state of the castle
		}

		if (progressBarProcent >= 1) {
			progressBarProcent = 1;
			if (castleProgress != 5) {
				finishcastle ();
			}

		}
	
	}


	/// <summary>
	/// The process to set castel to finished
	/// </summary>
	void finishcastle () {
		castleProgress = 5; //set castle finished
		castle.GetComponent<Animator>().SetTrigger("finishcastle"); //set anim trigger for finished castle
		castle.GetComponent<AudioSource>().Stop(); //stop building sound
		AudioSource.PlayClipAtPoint(custleFinishedSound, Camera.main.transform.position, 1f); //play castle finished sound
	}

	/// <summary>
	/// To play a sound set in the editor
	/// </summary>
	/// <param name="audiotoplay">Audioclip to be played</param>
	public void playsound(AudioClip audiotoplay) {
		AudioSource.PlayClipAtPoint(audiotoplay, Camera.main.transform.position, 1f);
	}
	
	
	
	
}
