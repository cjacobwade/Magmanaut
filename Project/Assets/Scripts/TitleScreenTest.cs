using UnityEngine;
using System.Collections;

public class TitleScreenTest : MonoBehaviour {
	
	
public Texture2D buttonImage; 	
	int xpos;
	int ypos;
	
	void Update(){
	xpos = (Screen.width/2)-(buttonImage.width/2);
	ypos = (Screen.height/2)-(buttonImage.height/2);
	}
	
	void OnGUI() {
        GUI.Button(new Rect(xpos,ypos,buttonImage.width,buttonImage.height), buttonImage);
        

    }
}