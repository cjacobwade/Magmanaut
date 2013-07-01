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
	
	//back
		public GameObject[] backgrounds;

	//Movement
	
		public int platMoveSpeed;
		public float backMoveSpeed;
		public bool platMove;
	
	//"Procedural Generation"
	
		bool platSpawn = false;//Has the next platform been planned?
		public bool backSpawn;

		public float heightMin;//lowest y value of spawned plats
		public float heightMax;// highest y value of spawned plat
		float spawnHeight;//amount to be added to spawners yposition

		public float distMin;//lowest distance from other platforms
		public float distMax;//highest distance from other platforms
		float spawnDistance;//how far will this plat spawn from the last plat
		float backSpawnDistance;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Platform cycling
		if(!platSpawn)
			PlatPlan();
		else
		{
			if(Vector3.Distance(currentPlat.transform.position,transform.position)>spawnDistance)
				PlatSpawn();
		}
	}
	
	void PlatPlan()
	{
		int platNum = Random.Range(1,3); //choose which platform is going to spawn
		nextPlat = allPlats[platNum]; //next plat will be the chosen plat
		spawnHeight = Random.Range(heightMin,heightMax);
		spawnDistance = currentPlat.GetComponent<Platform>().platLength + (Mathf.Sqrt(Mathf.Pow(Random.Range(distMin,distMax),2) + Mathf.Pow((spawnHeight),2)));//length of platform + random distance
		platSpawn = true;
	}
	void PlatSpawn()
	{
		Vector3 spawnPosition = new Vector3(transform.position.x,transform.position.y + spawnHeight, transform.position.z);
		lastPlat = currentPlat;
		//create platform and assign it to a position in allPlats
		currentPlat = Instantiate(nextPlat,spawnPosition, transform.rotation) as GameObject;
		platSpawn = false;
	}
}
