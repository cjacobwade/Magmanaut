using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	public Texture2D playButton;
	private Texture2D currentButton;
	public GUISkin transparentBorder;
	public Texture2D menuBG;
	public Texture2D HomeIcon;
	// Use this for initialization
	
	
	void Start () 
	{
		Time.timeScale = 1;
		currentButton = pauseButton;
		Screen.orientation = ScreenOrientation.Landscape;
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		int xpos = Screen.width-(playButton.width+32);
		GUI.skin = transparentBorder;			
		if (Time.timeScale == 0){
			GUI.DrawTexture(new Rect(Screen.width-190,0,196,Screen.height),menuBG);
			if(GUI.Button(new Rect(xpos,425,128,128),HomeIcon)){
				Application.LoadLevel("StartScreen");
			}
		}
		


		if(GUI.Button(new Rect(xpos,32,currentButton.width,currentButton.height),currentButton))
		{	
			if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
				currentButton = pauseButton;
				
			}
			
			else if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				currentButton = playButton;

			}

		}
	}
}		
	
