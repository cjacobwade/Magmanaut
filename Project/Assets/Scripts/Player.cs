using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Other
	
		public GameObject model;
		public GameObject mover;
	
	//Movement
	
		public int moveSpeed;
		public Vector3 velocity;
				
	//Jumping
	
		public LayerMask platLayer;//which layer are the platforms on
		public int jumpSpeed;
		public int doubleSpeed;//double jump speed
		public int gravitySpeed;
		bool isDouble = false;//double jumping?
	
	//Animation
		
		//Animations
			//Running
			//Jumping
			//Double Jumping
			//Deflecting
			//Special Abilities
			//Getting hit
			//Dying
			//Clambering up ledges
	
	//Sound
	
		public AudioClip[] soundIndex;
		//Sounds
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
		SideCheck();

		if(Input.GetKey(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}
	
	void Movement ()
	{
		CharacterController controller = GetComponent<CharacterController>();//Need this so we can reference the character controller component
		
		//On the ground
			if(controller.isGrounded)	
				OnGround();
		
		//In the air
			if(!controller.isGrounded)
				InAir();
		
		controller.Move(velocity*Time.deltaTime);//Always moving at current velocity
		if(transform.position.y < -3)//If fell off, restart level #Need to modularize in case levels would let you fall far or go higher up
			Application.LoadLevel(Application.loadedLevel);
	}
	
	void OnGround ()
	{
		PlayAnimation("Walk",2.5f);
		isDouble = false;
		if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
		{
			velocity.y = 0;
			velocity.y += jumpSpeed;
			PlayAnimation("Jump",1);
		}
	}
	
	void InAir ()
	{
		if(!model.animation["Jump"].enabled && !model.animation["Spin"].enabled)
			PlayAnimation("Fall",.5f);
		
		velocity.y += gravitySpeed*Time.deltaTime;
		if(!isDouble)
		{
			if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
			{
				isDouble = true;
				if(RandomBool())
					PlayAnimation("Spin",1.2f);
				else
					PlayAnimation("Spin",1.2f);//Change to spin the other way
				velocity.y = 0;
				velocity.y += doubleSpeed;
			}
		}
	}
	
	void SideCheck ()
	{
		//Check for collisions against platform sides
			//Debug.DrawRay(transform.position,-transform.forward,Color.red,.5f);//used to draw show the raycast's path
			if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y-.5f,transform.position.z),-transform.forward,.5f,platLayer)||Physics.Raycast(new Vector3(transform.position.x,transform.position.y+.5f,transform.position.z),-transform.forward,.5f,platLayer))
			{
				isDouble = true;
				mover.GetComponent<Cycle>().platMove = false;
			}
	}
	
	void PlayAnimation(string name,float speed)
	{
		model.animation[name].speed = speed;
		model.animation.Play(name);
	}
	
	bool RandomBool()
	{
		return (Random.value > 0.5f);	
	}
}
