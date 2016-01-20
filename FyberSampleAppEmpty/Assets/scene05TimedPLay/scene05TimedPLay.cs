/*
This script is for teh timed play. It shows a timer for remaining time, before the user needs to get to the next level.
It shows the actual level.
On getting to the next level it resets time and increases level counter.
If the game is over (time is up) it will offer to watch a video and rescue the game.
When the game is rescued the player is able to continue the game play with some extra time to allow getting to the next level.



*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class scene05TimedPLay : MonoBehaviour {


	public float startTime; // set time for start of the level
	public float rescueTime; //set time if level ended and rescued
	public GameObject levelText; // gameobject text for the actual level in the game
	public GameObject timeText; // game object text for the remaining time in the level
	public GameObject gameOverCanvas; //canvas to show when game is over
	public GameObject nextLevelButton; //button to go to the next level
	public string showLevel; //the string to use for the level
	public AudioClip clockTick; //audio for clock ticking

	int level = 1 ; //the level set to 1 at scene start
	float remainingTime; //to keep track of time remianed for finishing the level
	float elapsedTime = 0; //to keep track of time itself
	bool countTime = true; //for tracking if time needs to be counted or not

	string minutes; //string for show minutes on the clock
	string seconds; //string to show seconds on the clock


	// Use this for initialization
	void Start () {
		remainingTime = startTime; //set remaining time to the startig time
		updateTime (); //trigger text update for the time
		levelText.GetComponent<Text> ().text = showLevel.Replace("<level>", level.ToString()); //change level text to actual
	
	}


	// Update is called once per frame
	void Update () {


		if (countTime) { //check if time needs ti be counted
			elapsedTime += Time.deltaTime; //increase elapsed time


			if (elapsedTime >= 1) { //check if more then 1 second is elapsed
				elapsedTime -= 1; //lower elepased time with a second
				playsound(clockTick); //play a sound of clock tick
				remainingTime -= 1; //count down the time from the remianing one
				updateTime (); //trigger time text update
			}

			if (remainingTime > 5 && remainingTime < 11) { //check for remaining time 
				timeText.GetComponent<Text>().color = new Color(1,1,0); //change time color to yellow
			}
			else if (remainingTime <= 5) { //check for remaining time
				timeText.GetComponent<Text>().color = new Color(1,0,0); //change time color to red
			}

			if (remainingTime < 0) { //check if no time left for the level
				countTime = false; //stop counting time
				remainingTime = 0; //set remaining time to 0 (to actually show 0)
				elapsedTime = 0; //set elapsed time to 0
				gameOverCanvas.SetActive(true); //activate game over canvas
				timeText.SetActive(false); //de-activate time text object
				nextLevelButton.GetComponent<Button> ().interactable = false; //disable next level button
			}

		}
	
	}

	//load different scenes set in the editor
	public void loadScene(string String) {
		Application.LoadLevel(String);
	}

	/// <summary>
	/// Get to the next level in the game
	/// </summary>
	public void nextlevel (){
		level++; //update level counter
		remainingTime = startTime; //re-set remianing time to the level start time
		elapsedTime = 0; //re-set elapsed time to 0
		timeText.GetComponent<Text>().color = new Color(0,1,0); //change time text back to green
		levelText.GetComponent<Text> ().text = showLevel.Replace("<level>", level.ToString()); //update level text
		updateTime (); //trigger time text update

	}

	/// <summary>
	/// The function to upate time text.
	/// </summary>
	void updateTime () {
		minutes = Mathf.Floor(remainingTime / 60).ToString("00"); //get the minutes from remaining time
		seconds = Mathf.Floor(remainingTime % 60).ToString("00"); //get the seconds from remaining time
		
		timeText.GetComponent<Text> ().text = minutes + ":" + seconds; //update time text
	}

	/// <summary>
	/// This is to rescue the user in the level if game is over and rescue option was used
	/// </summary>
	public void rescueLevel() {
		remainingTime = rescueTime; //set remaining time to rescu time amount
		countTime = true; //start to count time again
		gameOverCanvas.SetActive(false); //disable game over canvas
		timeText.SetActive(true); //re-enable time text
		nextLevelButton.GetComponent<Button> ().interactable = true; //re-cativate next level button
		updateTime (); //trigger time text update
	}

	/// <summary>
	/// This function is for watching a video.
	/// </summary>
	public void watchVideo() {
		rescueLevel (); //trigger rescue for watching the video
	}

	/// <summary>
	/// To play a sound set in the editor
	/// </summary>
	/// <param name="audiotoplay">Audioclip to be played</param>
	public void playsound(AudioClip audiotoplay) {
		AudioSource.PlayClipAtPoint(audiotoplay, Camera.main.transform.position, 1f);
	}



}
