using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	GameObject spawner;
	GameObject emptyHolder;
	GameObject destroyer;
	//GameObject bgDestroyer;
	public float platLength;
	
	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.Find("Spawner");
		destroyer = GameObject.Find("Destroyer");
		emptyHolder = GameObject.Find("EmptyHolder");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(spawner.GetComponent<Cycle>().platMove)
		{
			if(tag == "Platform")
				transform.Translate(new Vector3(0,0,-spawner.GetComponent<Cycle>().platMoveSpeed)*Time.deltaTime);//Move at a constant rate
			if(tag == "Decor")
				transform.Translate(new Vector3(0,0,-spawner.GetComponent<Cycle>().platMoveSpeed)*Time.deltaTime);//Move at a constant rate
		}
		if(transform.position.z < destroyer.transform.position.z||(tag == "Decor" && transform.position.y < destroyer.transform.position.y))
		{
			//One of these is right...
				transform.parent = emptyHolder.transform;
				gameObject.SetActive(false);
				//Destroy(this.gameObject);
		}
	}
}
