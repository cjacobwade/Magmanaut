using UnityEngine;
using System.Collections;

public class TitleScreenTest : MonoBehaviour {
	
	
	public Texture2D buttonImage;
	public Texture2D MagmanautTitle;
	int xpos;
	int ypos;
	public int changeSizeX_button;
	public int changeSizeY_button;
	public int changeSizeX_title;
	public int changeSizeY_title;
	public GUISkin buttonStyle;
	
	void Start(){
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	void Update(){
	xpos = (Screen.width/2)-(buttonImage.width/2);
	ypos = (Screen.height/2)-(buttonImage.height/2);
	}
	
	void OnGUI() {
		GUI.skin = buttonStyle;
		GUI.Label (new Rect (xpos-changeSizeX_title,ypos-changeSizeY_title, MagmanautTitle.width, MagmanautTitle.height),MagmanautTitle);
        
		if (GUI.Button(new Rect(xpos,ypos,buttonImage.width-changeSizeX_button,buttonImage.height-changeSizeY_button), buttonImage)){
        Application.LoadLevel("Testing");}

    }
}