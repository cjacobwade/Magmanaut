using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	public Texture2D playButton;
	private Texture2D currentButton;
	// Use this for initialization
	
	void Start () 
	{
		currentButton = pauseButton;
		Screen.orientation = ScreenOrientation.Landscape;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width-pauseButton.width,0,pauseButton.width,pauseButton.height),currentButton)){
			if (Time.timeScale == 0){
				Time.timeScale = 1;
				currentButton = pauseButton;
			}
			else if (Time.timeScale == 1){
				Time.timeScale = 0;
				currentButton = playButton;
				if(GUI.Button(new Rect(Screen.width-pauseButton.width,15,75,50),"Home")){
					Application.LoadLevel("StartScreen");
				}
			}
		}
		
	}
}
