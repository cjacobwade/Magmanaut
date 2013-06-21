using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	GameObject spawner;
	GameObject destroyer;
	public float platLength;
	
	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.Find("Spawner");
		destroyer = GameObject.Find("Destroyer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(spawner.GetComponent<Cycle>().platMove)
			transform.Translate(new Vector3(0,0,-spawner.GetComponent<Cycle>().platMoveSpeed)*Time.deltaTime);//Move at a constant rate
		
		if(transform.position.z < destroyer.transform.position.z)
			Destroy(this.gameObject);
	}
}
