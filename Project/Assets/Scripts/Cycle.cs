using UnityEngine;
using System.Collections;

public class Cycle : MonoBehaviour {
	
	public GameObject spawner;
	public GameObject[] platforms;
	public int platMoveSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		for(int i=0;i<platforms.Length;i++)
		{
			platforms[i].transform.Translate(new Vector3(0,0,-platMoveSpeed)*Time.deltaTime);
			if(platforms[i].transform.position.z < transform.position.z)
				platforms[i].transform.position = (new Vector3(transform.position.x,transform.position.y,spawner.transform.position.z));
		}
	}
}
