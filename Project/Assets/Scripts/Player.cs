using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Movement
	
		public int moveSpeed;
		public Vector3 velocity;
				
	//Jumping
	
		public int jumpSpeed;
		public int doubleSpeed;//double jump speed
		public int gravitySpeed;
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
		Movement();
		
		if(Input.GetKey(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}
	
	void Movement()
	{
		CharacterController controller = GetComponent<CharacterController>();
		
		//Move
//		if(Input.GetKey(KeyCode.A))
//			velocity.z = moveSpeed;
//		
//		else if(Input.GetKey(KeyCode.D))
//			velocity.z = -moveSpeed;
//		
//		else
//			velocity.z = 0;
		
		//In the air
		if(!controller.isGrounded)
		{
			velocity.y += gravitySpeed*Time.deltaTime;
			if(!isDouble)
			{
				if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
				{
					isDouble = true;
					velocity.y = 0;
					velocity.y += doubleSpeed;
				}
			}
		}
		
		//On the ground
		if(controller.isGrounded)	
		{
			isDouble = false;
			if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				velocity.y = 0;
				velocity.y += jumpSpeed;
			}
		}
		
		//Check side collisions
		if((controller.collisionFlags & CollisionFlags.Sides) != 0)
			velocity.z = 0;
		
		controller.Move(velocity*Time.deltaTime);//Always be moving at current velocity
		if(transform.position.y < -3)//If fell off, restart level
			Application.LoadLevel(Application.loadedLevel);
		
	}
	
}
