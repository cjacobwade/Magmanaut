using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	public Texture2D playButton;
	Texture2D currentButton;
	public GUISkin transparentBorder;
	public Texture2D menuBG,pausePopup,pausePopupPlay,pausePopupHome,pausePopupPlay_Pressed;
	public Texture2D currentHomeIcon,HomeIcon,HomeIconPressed,currentPlayButton;
	Vector2 mousePos_2D;
	
	//Score
	
		public int scoreRate;
		int currentScore;
		public GUISkin skinScore;
		string stringScore;
		bool displayScore;
		public float waitTime;
	
	void Start () 
	{
		StartCoroutine(Timer(waitTime));
		Time.timeScale = 1;
		stringScore = "Score: " + currentScore;
		currentButton = pauseButton;
		currentHomeIcon = HomeIcon;
		currentPlayButton = pausePopupPlay;
		Screen.orientation = ScreenOrientation.Landscape;
		GUIStyle myStyle = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
		CheckHomeButton();
		CheckPlayButton();
		DisplayScore();
	}
	
	void OnGUI()
	{
		//int xpos = Screen.width-(playButton.width + Screen.width/9);
		
		//HI BRADEN!!! BASING BUTTON POSITIONS ON PIXEL OFFSET WON'T ALLOW YOU TO SCALE BY SCREEN SIZE
		//you need to use screen.width and screen.height or zero for everything GUI related
		GUI.skin = transparentBorder;	
		
		// Pause Popup Screen
		if (Time.timeScale == 0)
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),menuBG);//Transparent bar
			GUI.DrawTexture(new Rect((Screen.width/2)-300,Screen.height/14,600,600),pausePopup);//Pause Background Square

			if (GUI.Button(new Rect((Screen.width/2)-167,Screen.height/1.8f,400,200),currentHomeIcon)) // Home Button
			{
				Application.LoadLevel("StartScreen");
			}
			if(GUI.Button(new Rect((Screen.width/2)-167,Screen.height/3.5f,400,200),currentPlayButton)) // Play Button
		{
			displayScore = true;
			Time.timeScale = 1;
			currentButton = pauseButton;
			StartCoroutine(Timer (waitTime));
		}
		}
		
		// Pause button
		if(Time.timeScale == 1){
		if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height/9,Screen.width/7,Screen.height/7),currentButton))
		{	
				displayScore = false;
				Time.timeScale = 0;
				currentButton = playButton;
		}
		}
		GUI.skin = skinScore;
		GUI.Label(new Rect(Screen.width/64,Screen.width/48,256,128), stringScore);
	}
	
	void CheckHomeButton() 
	{
		// Gets position of input
		mousePos_2D = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos_2D.x > Screen.width/2.75f) && (mousePos_2D.x < Screen.width/2.75f + 400) && (mousePos_2D.y > Screen.height/1.75f+25) && (mousePos_2D.y < Screen.height/1.75f + 170))
			currentHomeIcon = HomeIconPressed;
		else currentHomeIcon = HomeIcon;
	}
	
	void CheckPlayButton() 
	{
		// Gets position of input
		mousePos_2D = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos_2D.x > Screen.width/2.75f) && (mousePos_2D.x < Screen.width/2.75f + 400) && (mousePos_2D.y > Screen.height/3.5f+25) && (mousePos_2D.y < Screen.height/3.5f + 170))
			currentPlayButton = pausePopupPlay_Pressed;
		else currentPlayButton = pausePopupPlay;
	}
	
	public IEnumerator Timer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		currentScore = currentScore + scoreRate;
		displayScore = true;
		if (Time.timeScale == 1)
			StartCoroutine(Timer (waitTime));
		else
			displayScore = false;
	}
	void DisplayScore(){
		if (displayScore){
			stringScore = "Score: " + currentScore;}
		if (!displayScore){
			stringScore = "";
		}
	}

}		
	
