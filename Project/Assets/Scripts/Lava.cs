using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	
	GameObject spawner;
	public float platSpeed;
	public float scrollSpeed;
	float currentTime;
	Renderer lava;
	
	// Use this for initialization
	void Awake () 
	{
		spawner = GameObject.Find("Spawner");
		lava = renderer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float offset = currentTime*scrollSpeed;
		if(spawner.GetComponent<Cycle>().platMove)
		{
			currentTime+= Time.deltaTime*platSpeed;
			if(tag == "Lava")
				lava.material.mainTextureOffset  = new Vector2 (0,-offset);
			if(tag == "Background")
				lava.material.mainTextureOffset  = new Vector2 (offset,0);
		}
		else
		{
			if(tag == "Lava")
			{
				currentTime+= Time.deltaTime/3;
				lava.material.mainTextureOffset  = new Vector2 (0,-offset);
			}
		}
	}
}
