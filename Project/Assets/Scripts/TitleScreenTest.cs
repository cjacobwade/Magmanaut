using UnityEngine;
using System.Collections;

public class TitleScreenTest : MonoBehaviour {
	
	
	public Texture2D buttonImage;
	public Texture2D MagmanautTitle;
	int xpos_start,ypos_start,xpos_title,ypos_title;
	public GUISkin buttonStyle;
	
	void Start(){
		// Set Screen to landscape
		Screen.orientation = ScreenOrientation.Landscape;
		xpos_start = (Screen.width/2)-(buttonImage.width/2);
		ypos_start = (Screen.height/2)-27;
		xpos_title = (Screen.width/2)-(MagmanautTitle.width/2-27);
		ypos_title = (Screen.height/2) - 200;
	}
	
	void Update(){

	}
	
	void OnGUI() {
		        
		GUI.skin = buttonStyle;
		
		// Magmanaut Title Image
		GUI.Label(new Rect(xpos_title,ypos_title, MagmanautTitle.width, MagmanautTitle.height), MagmanautTitle);
		
		// Start Button
		if (GUI.Button(new Rect(xpos_start,ypos_start,buttonImage.width,buttonImage.height), buttonImage)){
        	Application.LoadLevel("Testing");}

		
    }
}