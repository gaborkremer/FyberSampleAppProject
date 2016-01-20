/*
This script is for the login script. It allows to select a user, set user data, (start up SDK) nad load the main screen.



*/


using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class scene01Login : MonoBehaviour {

	public Font FyberFont; //Fyber font set in editor
	public string[] usersNames ; //set of user names
	public string[] userAges ; //set of user ages

	DeviceOrientation DevOri; //to store device orientation

	GUIStyle customStyle; //costum style for labels

	float touchStart; //temp info on where touch began
	float adjustY; // Y adjustment of the user selection list
	float adjustYTemp; //store previous temoorary value of Y adjustment
	float listPositioner; //positioning helper for the list
	float dragOriginY; //start of mouse drag
	float listWidth; //the width of the list of user names
	float listHeight; // the hight of the list of user names
	float listYdiff; //distance of entries in the list

	int listBorders = 5; //the border of fade out - list element outside of this limit is not shown

	int userSelected; //the index of the selected user in the list


	void Start () {
		adjustY = Mathf.Floor (usersNames.Length / 2f) * listYdiff; //set list adjustment to the mddle of the list based on it's lenght and difference between items
		customStyle = new GUIStyle ("label"); //assign style for labels
		customStyle.alignment = TextAnchor.MiddleCenter; // set text allignment 
		customStyle.font = FyberFont; //assign Fyber font to the GUI fonts
		OrientationUpdate (); //run orientation update to adjust values
	}



	void Update () {
		//check if the device orientation changed since the prefious frame, and update settings if needed
		if (Input.deviceOrientation != DevOri) {
			OrientationUpdate ();
		}

		//Quit app on hardware back button
		if (Input.GetButtonUp("Cancel")) {
			Application.Quit();
		}

		//check for touch and the delta of touch movement
		if (Input.touchCount == 1 && Input.touches [0].phase == TouchPhase.Moved) {
			adjustY += Input.GetTouch(0).deltaPosition.y*1.5f; //adjust list positioning based on touch
		}

		//check if mouse button was down (true in a single frame)
		if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1)) {
			dragOriginY = Input.mousePosition.y; //store where mouse was clicked
			adjustYTemp = adjustY; //store the previous adjustment of the list
		}

		//check if mouse button is pushed (true in every frame when button is pushed)
		if (Input.GetMouseButton (0) || Input.GetMouseButton (1)) {
			adjustY = adjustYTemp + (Input.mousePosition.y - dragOriginY) / 2f; //adjust list positioning based on mouse movement
		}

	
		//check list adjustment
		//make sure list is scrolling back to a fixed position where the selected item is in the middle
		//asjust selected item index based on list positioning
		for (int u = 0; u < usersNames.Length; u++) {
			if (adjustY > (u*listYdiff - listYdiff/2f) && adjustY <= (u*listYdiff + listYdiff/2f)) {
				userSelected = u;
				if (Input.touchCount == 0 && !Input.GetMouseButton (0) && !Input.GetMouseButton (1)) {
					adjustY = Mathf.Lerp(adjustY, u*listYdiff, Time.deltaTime*20f);
				}
			}
		}

		// set maximum and minimum of list adjustment, to disable scorring outside of the list
		adjustY = Mathf.Clamp (adjustY, 0, listYdiff * (usersNames.Length-1));
	}

	/// <summary>
	/// To update list settings based on the orientation and store the current oriantetion.
	/// </summary>
	void OrientationUpdate () {
		DevOri = Input.deviceOrientation; //update device orientation to the current one
		if (Screen.width <= Screen.height ) { //handle landscape or square scenario
			customStyle.fontSize = Mathf.FloorToInt(Screen.width / 18f); //set the font size based on screen size
			listPositioner = 3; //set wthe adjust helper of the list
			listWidth = Screen.width * 2f; //set list width
			listHeight = Mathf.FloorToInt (Screen.height / 15f); //set list height
			listYdiff = Mathf.FloorToInt (Screen.height / 15f); //set list item distance
		} else { //handle portrait scenario 
			customStyle.fontSize = Mathf.FloorToInt(Screen.height / 18f); //set the font size based on screen size
			listPositioner = 4; //set wthe adjust helper of the list
			listWidth = Screen.width * 2f; //set list width
			listHeight = Mathf.FloorToInt (Screen.width / 15f);//set list height
			listYdiff = Mathf.FloorToInt (Screen.width / 15f); //set list item distance
		}

	}

	/// <summary>
	/// Set user data, start SDK, and load the main screen
	/// </summary>
	/// <param name="String">The string passed over to load the scene after login</param>
	public void login(string String) {
		Application.LoadLevel(String); //load scene
	}



	/// <summary>
	/// To play a sound set in the editor
	/// </summary>
	/// <param name="audiotoplay">Audioclip to be played</param>
	public void playsound(AudioClip audiotoplay) {
		AudioSource.PlayClipAtPoint(audiotoplay, Camera.main.transform.position, 1f); //set the audio to play where the camera is with maximum volume (1)
	}


	void OnGUI () {
		//set up a box for the selected user
		GUI.Box(new Rect(Screen.width/2f-listWidth/2f,Screen.height/listPositioner, listWidth, listHeight), "");

		//draw all users from the list
		for (int i = 0; i < usersNames.Length; i++) {
			if (Mathf.Abs(i-userSelected) > listBorders) {
				customStyle.normal.textColor = new Color(GUI.color.r,GUI.color.g,GUI.color.b, 0f); //not show entries from the list outside fo the border
			}
			else {
				customStyle.normal.textColor = new Color(GUI.color.r,GUI.color.g,GUI.color.b, (float) 1/Mathf.Pow((Mathf.Abs(i-userSelected)+1),1.5f)); //set alpha based on the distance from the selected user
			}
			GUI.Label(new Rect(Screen.width/2f - listWidth/2f,Screen.height/listPositioner + i * listYdiff - adjustY, listWidth, listHeight), usersNames[i], customStyle); //draw the entries from the list
		}


	}



	

}
