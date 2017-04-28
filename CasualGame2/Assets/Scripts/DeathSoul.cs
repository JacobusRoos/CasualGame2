using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoul : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localScale += new Vector3(0.004f, 0.004f, 0);
		transform.position += new Vector3(0, 0, 0.02f);
		Color color;
		color = transform.GetComponent<SpriteRenderer>().color;
		color.a -= .02f;
		transform.GetComponent<SpriteRenderer>().color = color;
		
		if(color.a <= 0)
		{
			Destroy(gameObject);
		}
	}
}
