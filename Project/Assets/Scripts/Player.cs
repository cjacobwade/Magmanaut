using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
#region Variable Declarations	
	//Movement
	
		CharacterController controller;
		public int moveSpeed;
		public Vector3 velocity;
		public Vector3 actualSpeed;
		public bool gameStart;
				
	//Jumping
	
		public LayerMask platLayer;//which layer are the platforms on
		public int jumpSpeed;
		public int doubleSpeed;//double jump speed
		public int gravitySpeed;
		public bool grounded;
		public bool isDouble;//double jumping?
		public bool isJumping;

		Vector3 playerFront;
		Vector3 playerBack;
		Vector3 playerTop; //Top of player
		Vector3 playerBottom;//Bottom of player
	
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
	
		bool deathSound = false;
		//Steps
		//Jump
		//Double Jump
		//Deflecting
		//Special Abilities
		//Getting hit/Losing health
		//Dying
	
	//Health
	
//		public int maxHealth;
//		int currentHealth;
//		public float hitTime;//Time before vulerable after being hit
	
	//Swipe
	
		public Vector2 touchPos;
		//public Rect leftTouch;//touch box boundaries
		//public Rect rightTouch;
		//Sensitivity?
	
	//Object Refs
		public GameObject gameCamera;
		public GameObject model;
		public GameObject platSpawner;
		public GameObject soundMaker;
		public GameObject leftHand;
		public GameObject rightHand;
		public TrailRenderer trail;
		GameObject holder;
	
#endregion
	
	// Use this for initialization
	void Start() 
	{
		controller = GetComponent<CharacterController>();
		controller.detectCollisions = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerInput();
		Movement();
		SideCheck();
		BottomCheck();
		if(Input.GetKey(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
	}
	
	void PlayerInput()
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.x > (Screen.width-(Screen.width/7.7f))&& Input.GetTouch(0).position.y > (Screen.width-(Screen.width/7.7f)))
		{
			print ("Pause");
			//pause the game
		}
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.x < (Screen.width-(Screen.width/7.7f))&& Input.GetTouch(0).position.y < (Screen.width-(Screen.width/7.7f)))
		{
			if(grounded)
				Jump();
			else
			{
				if(!isDouble)
					DoubleJump();
			}
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(grounded)
				Jump();
			else
			{
				if(!isDouble)
					DoubleJump();
			}
		}
	}
	
	void Jump()
	{
		if(!gameStart)
		{
			platSpawner.GetComponent<Cycle>().platMove = true;
			gameStart = true;
		}
		else
		{
			if(!isJumping)
			{
				grounded = false;
				velocity.y = 0;
				velocity.y += jumpSpeed;
				if(velocity.y >= 0)
				{
					PlayAnimation("Jump",1f);
					StartCoroutine(Timer(.1f,"Jump"));
					isJumping = true;
				}
				else
				{
					isJumping = true;
					isDouble = false;
				}
			}
		}
	}
	
	void DoubleJump()
	{
		isDouble = true;
		if(RandomBool())
			PlayAnimation("Spin",1.2f);
		else
			PlayAnimation("Spin2",1.2f);//Change to spin the other way
		velocity.y = 0;
		velocity.y += doubleSpeed;
	}
	
	void Movement ()
	{
		controller.Move(velocity*Time.deltaTime);//Always moving at current velocity
		if(velocity.y<-9.8)
			velocity.y = -9.8f;
		actualSpeed = controller.velocity;//for debugging
		if(!isJumping)
		{
			if(velocity.y<0)
			{
				isJumping = true;
				isDouble = false;
			}
		}

		//Check to see if on platforms or in air
		if(grounded)
			OnGround();
		else
			InAir();

	}
	
	void FallCheck()
	{
		if(transform.position.y < -3)//If fell off, restart level #Need to modularize in case levels would let you fall far or go higher up
		{
			if(!deathSound)
			{
				soundMaker.GetComponent<Audio>().PlaySound(0,1,100,false);
				deathSound=true;
			}
			gameCamera.GetComponent<Camera>().playerFell = true;
		}
	}
	
	void OnGround()
	{
		isJumping = false;
		isDouble = true;
		if(platSpawner.GetComponent<Cycle>().platMove)
			PlayAnimation("Walk",2.5f);
		else
			PlayAnimation("Idle",.7f);
	}
	
	void InAir ()//Need to condense this
	{
		if(!model.animation["Jump"].enabled && !model.animation["Spin"].enabled && !model.animation["Spin2"].enabled)
			PlayAnimation("Fall",.5f);
		velocity.y += gravitySpeed*Time.deltaTime;
//		if(!isDouble)
//		{
//			if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
//			{
//				isDouble = true;
//				if(RandomBool())
//					PlayAnimation("Spin",1.2f);
//				else
//					PlayAnimation("Spin2",1.2f);//Change to spin the other way
//				velocity.y = 0;
//				velocity.y += doubleSpeed;
//			}
//		}
		FallCheck();
		SpinTrail();
	}
	
	void SpinTrail()
	{
		if(model.animation["Spin2"].enabled)
		{
			trail.transform.parent = leftHand.transform;
			trail.transform.localPosition = Vector3.zero;
			trail.startWidth = .15f;
		}
		else if(model.animation["Spin"].enabled)
		{
			trail.transform.parent = rightHand.transform;
			trail.transform.localPosition = Vector3.zero;
			trail.startWidth = .15f;
		}
		else
			trail.startWidth = 0;
	}
	
	void SideCheck ()
	{
		playerTop = new Vector3(transform.position.x,transform.position.y+.5f,transform.position.z); //Top of player
		playerBottom = new Vector3(transform.position.x,transform.position.y-.55f,transform.position.z);//Bottom of player
		playerFront = new Vector3(transform.position.x,transform.position.y+.5f,transform.position.z+.5f);

		//Debug
			Debug.DrawRay(playerTop,transform.forward,Color.red,0);//used to draw show the raycast's path
			Debug.DrawRay(playerBottom,transform.forward,Color.red,0);
			Debug.DrawRay(playerFront,-transform.up,Color.green,0);//For debug only
		if(Physics.Raycast(playerTop,transform.forward,.5f,platLayer)||(Physics.Raycast(playerBottom,transform.forward,.5f,platLayer)))
		{
			isDouble = true;
			platSpawner.GetComponent<Cycle>().platMove = false;
		}
	}
	
	void BottomCheck()
	{		
		playerFront = new Vector3(transform.position.x,transform.position.y,transform.position.z + .15f);
		playerBack = new Vector3(transform.position.x,transform.position.y,transform.position.z - .15f);
		playerBottom = new Vector3(transform.position.x,transform.position.y -.55f,transform.position.z + .15f);//For debug only
		
		//Debug
			Debug.DrawRay(playerFront,-transform.up,Color.red,0);//used to draw show the raycast's path
			Debug.DrawRay(playerBack,-transform.up,Color.red,0);//used to draw show the raycast's path
			Debug.DrawRay(playerBottom,-transform.forward,Color.green,0);//used to draw show the raycast's path
		if(Physics.Raycast(playerFront,-transform.up,.7f,platLayer)||(Physics.Raycast(playerBack,-transform.up,.7f,platLayer)))
			grounded = true;
		else
		{
			grounded = false;
		}
	}
	
	void PlayAnimation(string name,float speed)
	{
		model.animation[name].speed = speed;
		model.animation.Play(name);
	}
	
	IEnumerator Timer(float waitTime,string function)
	{
		yield return new WaitForSeconds(waitTime);
		if(function == "Jump")
			isDouble = false;
	}
	
	bool RandomBool()
	{
		return (Random.value > 0.5f);	
	}
	

}
