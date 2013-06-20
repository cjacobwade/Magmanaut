using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	public Texture2D playButton;
	Texture2D currentButton;
	public GUISkin transparentBorder;
	public Texture2D menuBG;
	public Texture2D currentHomeIcon,HomeIcon,HomeIconPressed;
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
		Screen.orientation = ScreenOrientation.Landscape;
		GUIStyle myStyle = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
		CheckHomeButton();
		DisplayScore();
	}
	
	void OnGUI()
	{
		//int xpos = Screen.width-(playButton.width + Screen.width/9);
		
		//HI BRADEN!!! BASING BUTTON POSITIONS ON PIXEL OFFSET WON'T ALLOW YOU TO SCALE BY SCREEN SIZE
		//you need to use screen.width and screen.height or zero for everything GUI related
		GUI.skin = transparentBorder;	
		
		// Home button
		if (Time.timeScale == 0)
		{
			GUI.DrawTexture(new Rect(10,10,Screen.width/1.01f,Screen.height/1.01f),menuBG);//Transparent bar
			if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height*7/9,Screen.width/7,Screen.height/7),currentHomeIcon))//Home button
			{
				Application.LoadLevel("StartScreen");
			}
		}
		
		// Play/pause button
		if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height/9,Screen.width/7,Screen.height/7),currentButton))
		{	
			if (Time.timeScale == 0)
			{
				displayScore = true;
				Time.timeScale = 1;
				currentButton = pauseButton;
				StartCoroutine(Timer (waitTime));
			}
			
			else if (Time.timeScale == 1)
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
		if ((mousePos_2D.x > Screen.width-(Screen.width/7.7f)) && (mousePos_2D.x < Screen.width-(Screen.width/7.7f) + Screen.width/7) && (mousePos_2D.y > Screen.height*7/9) && (mousePos_2D.y < Screen.height*7/9 + Screen.height/7))
			currentHomeIcon = HomeIconPressed;
		else currentHomeIcon = HomeIcon;
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
	
