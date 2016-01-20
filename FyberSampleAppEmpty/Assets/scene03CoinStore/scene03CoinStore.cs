/*
This script is for coin store. It allows to switch over to the gem store, to buy coins or watch a video to earn more coins.



*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class scene03CoinStore : MonoBehaviour {

	public GameObject ShowedCoinText; //gameobject for the coins text
	public GameObject rewardCoinsText; //gameobject of the rewarded coins
	public GameObject rewardCanvas; //gameobject of the canvas for the reward text

	int coins = 0; //coins owned, set to 0 at scene start
	
	void Start () {
		updateCoinText (); //update text for amount of coins at start of the scene
	}
	

	//load different scenes set in the editor
	public void loadScene(string String) {
		Application.LoadLevel(String);
	}

	
	/// <summary>
	/// Update the coins with different amounts
	/// </summary>
	/// <param name="getCoins">Get coins.</param>
	public void updateCoins (int getCoins ) {
		coins += getCoins; //update coins amount
		updateCoinText (); //trigger coin text update
	}

	/// <summary>
	/// Update and show coins reward text
	/// </summary>
	/// <param name="coinsReward">Coins reward.</param>
	public void updateRewardText (string coinsReward) {
		rewardCoinsText.GetComponent<Text> ().text = coinsReward; //update text for rewarded coins
		rewardCanvas.SetActive (true); //set the canvas active  to show the text
		rewardCoinsText.GetComponentInParent<Animator> ().Rebind (); //reset animator
	}

	//update existing coin text
	void updateCoinText () {
		if (coins < 2) { //check amount (single or multiple)
			ShowedCoinText.GetComponent<Text> ().text = "You have " + coins.ToString() + " coin"; //update text
		} else {
			ShowedCoinText.GetComponent<Text> ().text = "You have " + coins.ToString() + " coins"; //update text
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
