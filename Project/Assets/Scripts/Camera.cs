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
		public Texture2D fallOutline,fallRestart,fallRestart_pressed,fallHome,fallHome_pressed,currentFallHome,currentFallRestart;
	
	//GUI Skins
		public GUISkin transparentBorder;
		public GUISkin skinScore;
		public GUISkin fallScreenScore;
	
	//Score
		public int scoreRate;//rate of change
		int currentScore;
		bool displayScore = true;
		string stringScore;
	
	//Fall Screen
		float fallButtonsXPOS;
		float fallRestartYPOS;
		float fallHomeYPOS;
	
	//Other
		Vector2 mousePos;
		public float waitTime;//time between changes
		public float buttonSizeDivisor,menuSizeDivisor;	
		private float pausebuttonSizeX,menuOutlineSizeY,menuOutlineYOffset;
		public bool playerFell;
		private bool pauseScreen,fellScreen;
		private float scoreLabelWidth;
	
	void Start () 
	{
	//Start timer
		StartCoroutine(ScoreTimer(waitTime));
		
		Time.timeScale = 1;
		Screen.orientation = ScreenOrientation.Landscape;
		
	// Whether to display pause popup or player fell popup
		pauseScreen = true;
		fellScreen = false;
		
	//Display Score
		stringScore = "Score: " + currentScore;
		
	//Button hover
		currentButton = pauseButton;
		currenthomeButton = homeButton;
		currentPlayButton = playButton;
		currentFallHome = fallHome;
		currentFallRestart = fallRestart;
		
	//Pause/Fall Screen Size and offset from top of screen
		menuOutlineSizeY = Screen.height / menuSizeDivisor;
		menuOutlineYOffset = (Screen.height - menuOutlineSizeY)/2;
	
	//Button Sizes and placement
		pausebuttonSizeX = Screen.width / buttonSizeDivisor;
		
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckHomeButton();
		CheckPlayButton();
		CheckFallRestartButton();
		CheckFallHomeButton();
		DisplayScore();
	}
	
	void OnGUI()
	{
		fallRestartYPOS = (Screen.height-menuOutlineYOffset*7-(fallHome.height));
		fallHomeYPOS = (Screen.height-menuOutlineYOffset*4-(fallHome.height));
		GUI.skin = transparentBorder;	
		
		if (Time.timeScale == 0)//IF PAUSED
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),menuBG);//Transparent Background
			
		// Show PAUSE screen
			if (pauseScreen)
			{
			GUI.DrawTexture(new Rect((Screen.width/2)-(menuOutlineSizeY/2),menuOutlineYOffset,menuOutlineSizeY,menuOutlineSizeY),menuBorder);//Pause Background Square
				
				if (GUI.Button(new Rect((Screen.width/2)-(pausebuttonSizeX/2),Screen.height/2.35f-((pausebuttonSizeX/2)/2),pausebuttonSizeX,pausebuttonSizeX/2),currentPlayButton)) // Play Button
				{
					displayScore = true;
					Time.timeScale = 1;
					currentButton = pauseButton;
					StartCoroutine(ScoreTimer (waitTime));
				}
			
				if (GUI.Button(new Rect((Screen.width/2)-(pausebuttonSizeX/2),Screen.height/1.35f-((pausebuttonSizeX/2)/2),pausebuttonSizeX,pausebuttonSizeX/2),currenthomeButton)) // Home Button
					Application.LoadLevel("StartScreen");
			}
			
		// Show PLAYER FELL screen
			if (fellScreen)
			{
				StartCoroutine(FailTimer(1));
				displayScore = false;
				GUI.DrawTexture(new Rect((Screen.width/2)-(menuOutlineSizeY/2),menuOutlineYOffset,menuOutlineSizeY,menuOutlineSizeY),fallOutline);//Fall Background Square}
				GUI.skin = fallScreenScore;
				if (currentScore > 10000000)
					fallScreenScore.label.fontSize = 55;
				GUI.Label(new Rect(Screen.width/2-(menuOutlineSizeY/1.42f),360,400,400)," " + currentScore);
				
			// Home/Restart Buttons
				GUI.skin = transparentBorder;	
				if(GUI.Button(new Rect((Screen.width/2),fallRestartYPOS,(menuOutlineSizeY/2),menuOutlineSizeY/4),currentFallRestart))
					Application.LoadLevel(Application.loadedLevel);
				if(GUI.Button(new Rect((Screen.width/2),fallHomeYPOS,(menuOutlineSizeY/2),menuOutlineSizeY/4),currentFallHome))
					Application.LoadLevel("StartScreen");
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
		
		if(playerFell)
		{
			Time.timeScale = 0;
			fellScreen = true;
			pauseScreen = false;
		}

		GUI.skin = skinScore;
		GUI.Label(new Rect(Screen.width/64,Screen.width/48,600,128), stringScore);
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
	
	void CheckFallRestartButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > (Screen.width/2 + menuOutlineSizeY/25f)) && (mousePos.x < (Screen.width/2 + (menuOutlineSizeY/2)-(menuOutlineSizeY/25))) && (mousePos.y > fallRestartYPOS + menuOutlineSizeY/15) && (mousePos.y < fallRestartYPOS + (menuOutlineSizeY/4) - (menuOutlineSizeY/15)))
			currentFallRestart = fallRestart_pressed;
		else currentFallRestart = fallRestart;
	}
	
	void CheckFallHomeButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > (Screen.width/2 + menuOutlineSizeY/25f)) && (mousePos.x < (Screen.width/2 + (menuOutlineSizeY/2)-(menuOutlineSizeY/25))) && (mousePos.y > fallHomeYPOS + menuOutlineSizeY/15) && (mousePos.y < fallHomeYPOS + (menuOutlineSizeY/4) - (menuOutlineSizeY/15)))
			currentFallHome = fallHome_pressed;
		else currentFallHome = fallHome;
	}
	
	public IEnumerator ScoreTimer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);//Wait for x seconds
		if(!playerFell)
		{
			if(player.GetComponent<Player>().gameStart)
				currentScore = currentScore + scoreRate;
			if (Time.timeScale == 1)
				StartCoroutine(ScoreTimer (waitTime));
			else
				displayScore = false;
		}
	}
	
	public IEnumerator FailTimer(float waitTime)
	{	
		yield return new WaitForSeconds(waitTime);//Wait for x seconds
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
	
