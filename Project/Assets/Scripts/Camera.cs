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
		public GUISkin fallScreenScore,fallScreenScoreHeading;
	
	//Score
		public int scoreRate;//rate of change
		int currentScore;
		float highScore = 0;
		bool displayScore = true;
		string stringScore;
	
	//Fall Screen
		float fallButtonsXPOS;
		float fallRestartYPOS;
		float fallHomeYPOS;
		float screenDPI;
	
	//Other
		Vector2 mousePos;
		public float waitTime;//time between changes
		public float buttonSizeDivisor,menuSizeDivisor;	
		private float pausebuttonSizeX,menuOutlineSizeY,menuOutlineYOffset;
		public bool playerFell;
		private bool pauseScreen,fellScreen;
		private float scoreLabelWidth;
	
	void Start ()//needs to be refactored
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
		
		screenDPI = Screen.dpi;
		
		highScore = PlayerPrefs.GetFloat("Player Score");
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
		GUI.skin = transparentBorder;//Pause buttons are transparent
		
		
		if (Time.timeScale == 0)//When paused
		{
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),menuBG);//transparent background
			PauseGUI();
		}
		else
			GameGUI();

		GUI.skin = skinScore;//Score font
		GUI.Label(new Rect(Screen.width/64,Screen.width/48,600,128), stringScore);
	}
	
	void GameGUI()
	{
		if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height/18,Screen.width/7,Screen.height/7),currentButton))
		{	
			displayScore = false;
			Time.timeScale = 0;
			currentButton = playButton;
		}
	}
	
	void PauseGUI()
	{
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
		
		if(playerFell)
		{
			Time.timeScale = 0;
			fellScreen = true;
			pauseScreen = false;
		}
		
		if (fellScreen)
		{
			StartCoroutine(FailTimer(1));
			displayScore = false;
			GUI.DrawTexture(new Rect((Screen.width/2)-(menuOutlineSizeY/2),menuOutlineYOffset,menuOutlineSizeY,menuOutlineSizeY),fallOutline);//Fall Background Square}
			GUI.skin = fallScreenScoreHeading;
			if (Screen.dpi < 250)
				fallScreenScoreHeading.label.fontSize = 25;
			else 
				fallScreenScoreHeading.label.fontSize = 35;
			GUI.Label(new Rect(Screen.width/2+Screen.width/64,Screen.height/2,400,400),"SCORE");
			GUI.Label(new Rect(Screen.width/2+Screen.width/64,Screen.height/1.4f,400,400),"HI-SCORE");
			GUI.skin = fallScreenScore;
			if (currentScore > 10000000)
				fallScreenScore.label.fontSize = 55;
			GUI.Label(new Rect(Screen.width/2+Screen.width/64,Screen.height/1.8f,400,400)," " + currentScore);
			if (currentScore > highScore)
			{
				PlayerPrefs.SetFloat("Player Score", currentScore);
				highScore = currentScore;
			}
			GUI.Label(new Rect(Screen.width/2+Screen.width/64,Screen.height/1.3f,400,400)," " + highScore);
			
		// Home/Restart Buttons
			GUI.skin = transparentBorder;	
			if(GUI.Button(new Rect((Screen.width/2)-menuOutlineSizeY/2.05f,fallRestartYPOS,(menuOutlineSizeY/2),menuOutlineSizeY/4),currentFallRestart))
				Application.LoadLevel(Application.loadedLevel);
			if(GUI.Button(new Rect((Screen.width/2)-menuOutlineSizeY/2.05f,fallHomeYPOS,(menuOutlineSizeY/2),menuOutlineSizeY/4),currentFallHome))
				Application.LoadLevel("StartScreen");
		}
	}
	
	void CheckHomeButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > Screen.width/2.75f) && (mousePos.x < Screen.width/2.75f + 400) && (mousePos.y > Screen.height/1.75f+25) && (mousePos.y < Screen.height/1.75f + 170))
			currenthomeButton = homeButtonPressed;
		else 
			currenthomeButton = homeButton;
	}
	
	void CheckPlayButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > Screen.width/2.75f) && (mousePos.x < Screen.width/2.75f + 400) && (mousePos.y > Screen.height/3.5f+25) && (mousePos.y < Screen.height/3.5f + 170))
			currentPlayButton = playButtonPressed;
		else 
			currentPlayButton = playButton;
	}
	
	void CheckFallRestartButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > (Screen.width/2-menuOutlineSizeY/2.05f)) && (mousePos.x < ((Screen.width/2-menuOutlineSizeY/2.05f) + menuOutlineSizeY/2)) && (mousePos.y > fallRestartYPOS + menuOutlineSizeY/15) && (mousePos.y < fallRestartYPOS + (menuOutlineSizeY/4) - (menuOutlineSizeY/15)))
			currentFallRestart = fallRestart_pressed;
		else 
			currentFallRestart = fallRestart;
	}
	
	void CheckFallHomeButton() 
	{
		// Gets position of input
		mousePos = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos.x > (Screen.width/2-menuOutlineSizeY/2.05f)) && (mousePos.x < ((Screen.width/2-menuOutlineSizeY/2.05f) + menuOutlineSizeY/2)) && (mousePos.y > fallHomeYPOS + menuOutlineSizeY/15) && (mousePos.y < fallHomeYPOS + (menuOutlineSizeY/4) - (menuOutlineSizeY/15)))
			currentFallHome = fallHome_pressed;
		else 
			currentFallHome = fallHome;
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
	
