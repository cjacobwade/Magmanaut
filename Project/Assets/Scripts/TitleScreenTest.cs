using UnityEngine;
using System.Collections;

public class TitleScreenTest : MonoBehaviour {
	
	
	public Texture2D buttonImage, buttonImagePressed;
	public Texture2D MagmanautTitle;
	int xpos_start,ypos_start,xpos_title,ypos_title;
	public GUISkin buttonStyle;
	private Texture2D currentbuttonImage;

	
	void Start(){
		// Set Screen to landscape
		Screen.orientation = ScreenOrientation.Landscape;
		xpos_start = (Screen.width/2)-(buttonImage.width/2);
		ypos_start = (Screen.height/2)-27;
		xpos_title = (Screen.width/2)-(MagmanautTitle.width/2-27);
		ypos_title = (Screen.height/2) - 200;
		currentbuttonImage = buttonImage;
	}
	
	void Update(){

	}
	
	void OnGUI() {
		        
		GUI.skin = buttonStyle;
		
		// Magmanaut Title Image
		GUI.Label(new Rect(xpos_title,ypos_title, MagmanautTitle.width, MagmanautTitle.height), MagmanautTitle);
		 if (Input.GetButton ("Fire1")){
			buttonImage = buttonImagePressed;
		} 
		
		// Start Button
		if (GUI.Button(new Rect(xpos_start,ypos_start,buttonImage.width,buttonImage.height), currentbuttonImage)){
		Application.LoadLevel("Testing");
	}
		if (GUI.RepeatButton(new Rect(xpos_start,ypos_start,buttonImage.width,buttonImage.height), buttonImage)){
        	
			currentbuttonImage = buttonImagePressed;
		
		}

	
    }
}