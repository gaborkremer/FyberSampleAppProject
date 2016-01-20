/*
This script is for gem store. It allows to switch over to the coin store, to buy gems or complete offers from the offer wall to earn more coins.



*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class scene04GemStore : MonoBehaviour {

	public GameObject ShowedGemText; //gameobject for the gems text
	public GameObject rewardGemsText; //gameobject of the rewarded gems
	public GameObject rewardCanvas; //gameobject of the canvas for the reward text
	
	int gems = 0; //gems owned, set to 0 at scene start

	void Start () {
		updateGemText (); //update text for amount of gems at start of the scene
	}



	//load different scenes set in the editor
	public void loadScene(string String) {
		Application.LoadLevel(String);
	}

	

	/// <summary>
	/// Update the gems with different amounts
	/// </summary>
	/// <param name="getGems">Get gems.</param>
	public void updateGems (int getGems) {
		gems += getGems; //update gems amount
		updateGemText (); //trigger gems text update

	}

	/// <summary>
	/// Update and show gems reward text
	/// </summary>
	/// <param name="gemsReward">Gems reward.</param>
	public void updateRewardText (string gemsReward) {
		rewardGemsText.GetComponent<Text> ().text = gemsReward; //update text for rewarded gems
		rewardCanvas.SetActive (true); //set the canvas active  to show the text
		rewardGemsText.GetComponentInParent<Animator> ().Rebind (); //reset animator
	}

	//update existing gems text
	void updateGemText () {
		if (gems < 2) { //check amount (single or multiple)
			ShowedGemText.GetComponent<Text> ().text = "You have " + gems.ToString() + " gem"; //update text
		} else {
			ShowedGemText.GetComponent<Text> ().text = "You have " + gems.ToString() + " gems"; //update text
		}
	}
	
	/// <summary>
	/// To play a sound set in the editor
	/// </summary>
	/// <param name="audiotoplay">Audioclip to be played</param>
	public void playsound(AudioClip audiotoplay) {
		AudioSource.PlayClipAtPoint(audiotoplay, Camera.main.transform.position, 1f);
	}




}
