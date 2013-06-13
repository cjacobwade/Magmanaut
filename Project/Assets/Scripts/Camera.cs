using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	// Use this for initialization
	void Start () 
	{
		Screen.orientation = ScreenOrientation.Landscape;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width-pauseButton.width,0,pauseButton.width,pauseButton.height),pauseButton)){
			if (Time.timeScale == 0){
				Time.timeScale = -1;
			}
			else if (Time.timeScale == 1){
				Time.timeScale = 0;
			}
		}
		
	}
}
