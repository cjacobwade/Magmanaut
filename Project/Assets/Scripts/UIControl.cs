using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour {
	
	public GUITexture element;
	public float top;
	public float left;
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		element.border.left = (int)left;
		element.border.right = element.texture.width;
		element.border.top = (int)top;
		element.border.bottom = element.texture.height;
	}
}
