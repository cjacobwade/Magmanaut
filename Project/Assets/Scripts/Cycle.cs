using UnityEngine;
using System.Collections;

public class Cycle : MonoBehaviour {
	
	//Procedural Generation
	
		//Objects
	
			//Platforms
				public GameObject lastPlat;//Platform that spawned last
				public GameObject currentPlat;//Platform that most recently spawned
				public GameObject nextPlat;//Platform that will spawn next
				public GameObject[] allPlats;//Plats being rendered currently
		
			//Other
				public GameObject destroyer;//object that destroys platforms
	
	//Platform sets
		
		public GameObject[] basicPlat;
//		public GameObject[] cavePlat;
//		public GameObject[] lavaPlat;
//		public GameObject[] lakePlat;

	//Movement
	
		public int platMoveSpeed;
		public bool platMove;
	
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
		//Spawning
	
			bool readySpawn = false;//Has the next platform been planned?
	
			public float heightMin;//lowest y value of spawned plats
			public float heightMax;// highest y value of spawned plat
			float spawnHeight;//amount to be added to spawners yposition
	
			public float distMin;//lowest distance from other platforms
			public float distMax;//highest distance from other platforms
			float spawnDistance;//how far will this plat spawn from the last plat
	
			Vector3 spawnPosition;//where will new plats spawn
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!readySpawn)
			PlatPlan();
		else
		{
			//if((currentPlat.transform.position.z + spawnDistance) >= transform.position.z)
			if(currentPlat != null)
			{
				if(Vector3.Distance(currentPlat.transform.position,transform.position)>spawnDistance)
				{
					PlatSpawn();
					readySpawn = false;
				}
			}
		}
	}
	
	void PlatPlan()
	{
		int platNum = Random.Range(1,3);
		//choose which platform is going to spawn
		nextPlat = allPlats[platNum];
		spawnHeight = Random.Range(heightMin,heightMax);
		spawnDistance = currentPlat.GetComponent<Platform>().platLength + Random.Range(distMin,distMax);//length of platform + random distance
		readySpawn = true;
	}
	void PlatSpawn()
	{

		spawnPosition = new Vector3(transform.position.x,transform.position.y + spawnHeight, transform.position.z);
		
		lastPlat = currentPlat;
		//create platform and assign it to a position in allPlats
		currentPlat = Instantiate(nextPlat,spawnPosition, transform.rotation) as GameObject;
	}
}
