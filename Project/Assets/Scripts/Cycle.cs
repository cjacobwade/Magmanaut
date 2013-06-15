using UnityEngine;
using System.Collections;

public class Cycle : MonoBehaviour {
	
	//Other
	
		public GameObject spawner;
		public GameObject[] platforms;
	
	//Movement
	
		public int platMoveSpeed;
		public bool platMove = true;
	
	//"Procedural Generation"
	
		//'Randomly' select which piece to use
			//Types of chunks:
				//Basic
				//Enemy
				//Small
				//Large
				//Cave (everything is dark but your path and possible paths)
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
			for(int i=0;i<platforms.Length;i++)//If this doesn't make sense look up for loops
			{
				//For each platform:
				platforms[i].transform.Translate(new Vector3(0,0,platMoveSpeed)*Time.deltaTime);//Move at a constant rate
				if(platforms[i].transform.position.z > transform.position.z)//Go back to start
					platforms[i].transform.position = (new Vector3(transform.position.x,platforms[i].transform.position.y,spawner.transform.position.z));
			}
		}
		else
		{
			for(int i=0;i<platforms.Length;i++)
				platforms[i].transform.Translate(new Vector3(0,0,0)*Time.deltaTime);
		}	
	}
}
