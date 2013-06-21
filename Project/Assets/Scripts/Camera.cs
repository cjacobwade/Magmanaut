using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	
	public GameObject player;
	
	//Images
		public Texture2D pauseButton;
		public Texture2D playButton;
		public Texture2D menuBG,menuBorder,playButtonPressed;
		public Texture2D homeButton,homeButtonPressed;
		Texture2D currenthomeButton;
		Texture2D currentPlayButton;
		Texture2D currentButton;
	
	//GUI Skins
		public GUISkin transparentBorder;
		public GUISkin skinScore;
	
	//Score
		public int scoreRate;//rate of change
		int currentScore;
		bool displayScore = true;
		string stringScore;
	
	//Other
		Vector2 mousePos;
		public float waitTime;//time between changes

	void Start () 
	{
		StartCoroutine(Timer(waitTime));
		Time.timeScale = 1;
		stringScore = "Score: " + currentScore;
		currentButton = pauseButton;
		currenthomeButton = homeButton;
		currentPlayButton = playButton;
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckHomeButton();
		CheckPlayButton();
		DisplayScore();
	}
	
	void OnGUI()
	{
		GUI.skin = transparentBorder;	
		
		if (Time.timeScale == 0)//IF PAUSED
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),menuBG);//Transparent Background
			GUI.DrawTexture(new Rect((Screen.width/2)-300,Screen.height/14,600,600),menuBorder);//Pause Background Square

			if (GUI.Button(new Rect((Screen.width/2)-167,Screen.height/1.8f,400,200),currenthomeButton)) // Home Button
				Application.LoadLevel("StartScreen");
			
			if(GUI.Button(new Rect((Screen.width/2)-167,Screen.height/3.5f,400,200),currentPlayButton)) // Play Button
			{
				displayScore = true;
				Time.timeScale = 1;
				currentButton = pauseButton;
				StartCoroutine(Timer (waitTime));
			}
		}
		
		// Pause button
		if(Time.timeScale == 1)
		{
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
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > Screen.width/2.75f) && (mousePos.x < Screen.width/2.75f + 400) && (mousePos.y > Screen.height/1.75f+25) && (mousePos.y < Screen.height/1.75f + 170))
			currenthomeButton = homeButtonPressed;
		else currenthomeButton = homeButton;
	}
	
	void CheckPlayButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > Screen.width/2.75f) && (mousePos.x < Screen.width/2.75f + 400) && (mousePos.y > Screen.height/3.5f+25) && (mousePos.y < Screen.height/3.5f + 170))
			currentPlayButton = playButtonPressed;
		else currentPlayButton = playButton;
	}
	
	public IEnumerator Timer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);//Wait for x seconds
		if(player.GetComponent<Player>().gameStart)
			currentScore = currentScore + scoreRate;
		if (Time.timeScale == 1)
			StartCoroutine(Timer (waitTime));
		else
			displayScore = false;
	}
	
	void DisplayScore()
	{
		if (displayScore)
			stringScore = "Score: " + currentScore;
		if (!displayScore)
			stringScore = "";
	}

}		
	
