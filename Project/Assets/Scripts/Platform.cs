using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	GameObject spawner;
	GameObject destroyer;
	GameObject bgDestroyer;
	public float platLength;
	
	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.Find("Spawner");
		if(tag == "Platform")
			destroyer = GameObject.Find("Destroyer");
		if(tag == "Background")
			bgDestroyer = GameObject.Find("BGDestroyer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(spawner.GetComponent<Cycle>().platMove)
		{
			if(tag == "Platform")
				transform.Translate(new Vector3(0,0,-spawner.GetComponent<Cycle>().platMoveSpeed)*Time.deltaTime);//Move at a constant rate
			if(tag == "Background")
				transform.Translate(new Vector3(0,0,-spawner.GetComponent<Cycle>().backMoveSpeed)*Time.deltaTime);//Move at a constant rate
		}
		
		if(tag == "Platform")
		{
			if(transform.position.z < destroyer.transform.position.z)
				Destroy(this.gameObject);
		}
	
		if(tag == "Background")
		{
			if(transform.position.z < bgDestroyer.transform.position.z)
				Destroy(this.gameObject);
		}
	}
}
