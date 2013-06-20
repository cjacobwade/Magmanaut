using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D pauseButton;
	public Texture2D playButton;
	private Texture2D currentButton;
	public GUISkin transparentBorder,homeIconSkin;
	public Texture2D menuBG;
	public Texture2D currentHomeIcon,HomeIcon,HomeIconPressed;
	private Vector2 mousePos_2D;
	private int currentScore;
	public GUISkin scoreSkin;
	// Use this for initialization
	
	
	void Start () 
	{
		Time.timeScale = 1;
		currentButton = pauseButton;
		currentHomeIcon = HomeIcon;
		Screen.orientation = ScreenOrientation.Landscape;
		GUIStyle myStyle = new GUIStyle();
		

		

	}
	
	// Update is called once per frame
	void Update () {
		CheckHomeButton();
	}
	
	void OnGUI()
	{
		//int xpos = Screen.width-(playButton.width + Screen.width/9);
		
		//HI BRADEN!!! BASING BUTTON POSITIONS ON PIXEL OFFSET WON'T ALLOW YOU TO SCALE BY SCREEN SIZE
		//you need to use screen.width and screen.height or zero for everything GUI related
		GUI.skin = transparentBorder;			
		if (Time.timeScale == 0)
		{
			GUI.DrawTexture(new Rect(Screen.width-Screen.width/8.4f,0,Screen.width/7,Screen.height),menuBG);//Transparent bar
			if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height*7/9,Screen.width/7,Screen.height/7),currentHomeIcon))//Home button
			{
				Application.LoadLevel("StartScreen");
			}
		}
		if(GUI.Button(new Rect(Screen.width-(Screen.width/7.7f),Screen.height/9,Screen.width/7,Screen.height/7),currentButton))//Play/pause button
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
		
			


		GUI.skin = scoreSkin;
		GUI.Label(new Rect(Screen.width/64,Screen.width/48,256,128),"Score: " + currentScore);

	}
	
	void CheckHomeButton() 
	{
		// Gets position of input
		mousePos_2D = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));
		
		// Compares it against boundaries of the button
		if ((mousePos_2D.x > Screen.width-(Screen.width/7.7f)) && (mousePos_2D.x < Screen.width-(Screen.width/7.7f) + Screen.width/7) && (mousePos_2D.y > Screen.height*7/9) && (mousePos_2D.y < Screen.height*7/9 + Screen.height/7))
			currentHomeIcon = HomeIconPressed;
		else currentHomeIcon = HomeIcon;;
	}
	
	public IEnumerator Timer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		currentScore = currentScore + 37;
		StartCoroutine(Timer (waitTime));
	}
}		
	
