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
	public Texture2D backgroundTexture;
	public GUISkin buttonStyle;
	
	void Start(){
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	void Update(){
	xpos = (Screen.width/2)-(buttonImage.width/2);
	ypos = (Screen.height/3);
	}
	
	void OnGUI() {
		
		GUI.Label (new Rect ((Screen.width/2)-(MagmanautTitle.width/2),Screen.height/16, MagmanautTitle.width, MagmanautTitle.height),MagmanautTitle);
        
		GUI.skin = buttonStyle;
		if (GUI.Button(new Rect(xpos,ypos,buttonImage.width,buttonImage.height), buttonImage)){
        Application.LoadLevel("Testing");}

    }
}