using UnityEngine;
using System.Collections;

public class TitleScreenTest : MonoBehaviour {
	
	
	public Texture2D buttonImage, buttonImagePressed;
	public Texture2D MagmanautTitle;
	int xpos_start,ypos_start,xpos_title,ypos_title;
	public GUISkin buttonStyle;
	private Texture2D currentbuttonImage;
	Vector2 fingerPos,mousePos_2D;
	private bool isonbutton;
	
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
		
		// Button Pressing
		onButton();
	}
	
	void OnGUI() {
		
		// Removes button borders
		GUI.skin = buttonStyle;
		
		// Magmanaut Title Image
		GUI.Label(new Rect(xpos_title,ypos_title, MagmanautTitle.width, MagmanautTitle.height), MagmanautTitle);
		 
		
		// Start Button
		if (GUI.Button(new Rect(xpos_start,ypos_start,buttonImage.width,buttonImage.height), currentbuttonImage)){
			Application.LoadLevel("Testing");
	}
		
		
		}
	void onButton() 
	{
		// Gets position of input
		mousePos_2D = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos_2D.x > xpos_start) && (mousePos_2D.x < xpos_start + currentbuttonImage.width) && (mousePos_2D.y > ypos_start) && (mousePos_2D.y < ypos_start + currentbuttonImage.height))
			currentbuttonImage = buttonImagePressed;
		else currentbuttonImage = buttonImage;
	}
	
	
    }
