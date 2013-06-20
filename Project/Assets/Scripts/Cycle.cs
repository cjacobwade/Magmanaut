using UnityEngine;
using System.Collections;

public class Cycle : MonoBehaviour {
	
	//Other
	
		//Platforms
			public GameObject lastPlat;//Platform that spawned last
			public GameObject currentPlat;//Platform that most recently spawned
			public GameObject nextPlat;//Platform that will spawn next
			GameObject[] allPlats;//Plats being rendered currently
	
		public GameObject destroyer;//object that destroys platforms
	
	//Platform sets
		
		public GameObject[] basicPlat;
//		public GameObject[] cavePlat;
//		public GameObject[] lavaPlat;
//		public GameObject[] lakePlat;

	//Movement
	
		public int platMoveSpeed;
		public bool platMove = true;
	
	//"Procedural Generation"
	
		//'Randomly' select which piece to use
			//Types of chunks:
				//Size
					//Small
					//Medium
					//Large
					//Trap?
					//Enemy/Boss
				//Area
					//Basic (Cavernous dark area - GOW)
					//Cave (everything is dark but your path and possible paths)
					//Lava (lit up by Magma - fight in OoT with Volvagia)
					//Lake (Underwater lake - Pikmin or Dwemers in Skyrim)
		//Placement
			//Max, min distance to next chunks
			//Max, min height of next chunks
			//Max, min rotations of next chunks
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlatMovement();
	}
	
	void PlatMovement()
	{
		if(platMove)
		{
			for(int i=0;i<allPlats.Length;i++)//If this doesn't make sense look up for loops
			{
				//For each platform:
				allPlats[i].transform.Translate(new Vector3(0,0,platMoveSpeed)*Time.deltaTime);//Move at a constant rate
				if(allPlats[i].transform.position.z > transform.position.z)//Go back to start
					allPlats[i].transform.position = (new Vector3(transform.position.x,allPlats[i].transform.position.y,destroyer.transform.position.z));
			}
		}
		else
		{
			for(int i=0;i<allPlats.Length;i++)
				allPlats[i].transform.Translate(new Vector3(0,0,0)*Time.deltaTime);
		}	
	}
}
