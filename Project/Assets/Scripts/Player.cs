using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//Other
	
		public GameObject gameCamera;
		public GameObject model;
		public GameObject platSpawner;
	
	//Movement
	
		CharacterController controller;
		public int moveSpeed;
		public Vector3 velocity;
		public bool gameStart;
				
	//Jumping
	
		public LayerMask platLayer;//which layer are the platforms on
		public int jumpSpeed;
		public int doubleSpeed;//double jump speed
		public int gravitySpeed;
		public bool grounded = false;
		public bool isJump = false;
		public bool isDouble = false;//double jumping?
	
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
		controller = GetComponent<CharacterController>();
		//currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement();
		SideCheck();
		BottomCheck();

		if(Input.GetKey(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);
			//gameCamera.GetComponent<Camera>().playerFell = true;

	}
	
	void Movement ()
	{
		//CharacterController controller = GetComponent<CharacterController>();//Need this so we can reference the character controller component
		if(grounded)
			OnGround();
		else
			InAir();
		
		controller.Move(velocity*Time.deltaTime);//Always moving at current velocity
		if(transform.position.y < -3)//If fell off, restart level #Need to modularize in case levels would let you fall far or go higher up
			gameCamera.GetComponent<Camera>().playerFell = true;
	}
	
	void OnGround()
	{
		if(platSpawner.GetComponent<Cycle>().platMove)
			PlayAnimation("Walk",2.5f);
		else
			PlayAnimation("Idle",.7f);
		isDouble = true;
		//velocity.y = -3;
		if(Input.GetKeyDown(KeyCode.Space)||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
		{
			if(!gameStart)
			{
				platSpawner.GetComponent<Cycle>().platMove = true;
				gameStart = true;
			}
			else
			{
				grounded = false;
				velocity.y = 0;
				velocity.y += jumpSpeed;
				PlayAnimation("Jump",1);
				StartCoroutine(Timer(.05f,"Jump"));
			}
		}
	}
	
	void InAir ()
	{
		if(!model.animation["Jump"].enabled && !model.animation["Spin"].enabled && !model.animation["Spin2"].enabled)
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
					PlayAnimation("Spin2",1.2f);//Change to spin the other way
				velocity.y = 0;
				velocity.y += doubleSpeed;
			}
		}
	}
	
	void SideCheck ()
	{
	
		Vector3 playerTop = new Vector3(transform.position.x,transform.position.y+.5f,transform.position.z); //Top of player
		Vector3 playerBottom = new Vector3(transform.position.x,transform.position.y-.5f,transform.position.z);//Bottom of player
		//Check for collisions against platform sides
			//Debug.DrawRay(playerTop,transform.forward,Color.red,.5f);//used to draw show the raycast's path
			//Debug.DrawRay(playerBottom,transform.forward,Color.red,.5f);
			if(Physics.Raycast(playerTop,transform.forward,.7f,platLayer)||(Physics.Raycast(playerBottom,transform.forward,.5f,platLayer)))
			{
				isDouble = true;
				platSpawner.GetComponent<Cycle>().platMove = false;
			}
	}
	
	void BottomCheck()
	{		
		Vector3 playerFront = new Vector3(transform.position.x,transform.position.y,transform.position.z + .15f);
		Vector3 playerBack = new Vector3(transform.position.x,transform.position.y,transform.position.z - .15f);
		//Debug.DrawRay(playerFront,-transform.up,Color.red,.7f);//used to draw show the raycast's path
		//Debug.DrawRay(playerBack,-transform.up,Color.red,7f);//used to draw show the raycast's path
		if(Physics.Raycast(playerFront,-transform.up,.7f,platLayer)||(Physics.Raycast(playerBack,-transform.up,.7f,platLayer)))
			grounded = true;
		else
		{
			if(grounded)
				isDouble = false;
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
			isDouble=false;
	}
	
	bool RandomBool()
	{
		return (Random.value > 0.5f);	
	}
}
