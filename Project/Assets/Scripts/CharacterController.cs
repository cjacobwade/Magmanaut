using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	
	//Movement
	
		public int moveSpeed;
				
	//Jumping
	
		public int jumpSpeed;
		public int doubleSpeed;//double jump speed
		public int gravitySpeed;
		bool isJumping = false;
		bool isDouble = false;//double jumping?
	
	//Animation
	
		//Running
		//Jumping
		//Double Jumping
		//Deflecting
		//Special Abilities
		//Getting hit
		//Dying
	
	//Sound
	
		public AudioClip[] soundIndex;
		//Steps
		//Jump
		//Double Jump
		//Deflecting
		//Special Abilities
		//Getting hit/Losing health
		//Dying
	
	//Health
	
		public int maxHealth;
		int currentHealth;
		public float hitTime;//Time before vulerable after being hit
	
	//Swipe
	
		public Rect leftTouch;//touch box boundaries
		public Rect rightTouch;
		//Sensitivity?
	
	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetTouch(0).tapCount>0)
			transform.Translate(new Vector3(0,moveSpeed,0)*Time.deltaTime);
		
		
	}
}
