using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public int cost;
    public float ectoPerSecond;
    public float ectoPerHarvest;
    public float lifespan;
    public float timeToRipe;
    public GameObject plot;
    Color baseColor;
    public Color matureColor;
	bool animateDeath = false;

    // Use this for initialization
    void Start ()
    {
        baseColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        //transform.parent = null;
        transform.localRotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
        //transform.parent = plot.transform;

    }

    // Update is called once per frame
    void Update()
    {
		if(animateDeath)
		{
			transform.localScale += new Vector3(0.2f, 0.2f, 0);
			transform.position += new Vector3(0, .1f, 0);
			Color color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
			color.a -= .02f;
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
			if(transform.localScale.x > 8)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			
			lifespan -= Time.deltaTime;
			timeToRipe -= Time.deltaTime;
			if(timeToRipe <= 0)
			{
				transform.GetChild(0).GetComponent<SpriteRenderer>().color = matureColor;
			}
			else
			{
				transform.GetChild(0).GetComponent<SpriteRenderer>().color = baseColor;
			}
			if (lifespan <= 0)
			{
				plot.GetComponent<Plot>().RemoveFromPlot(gameObject);
				Destroy(gameObject);
			}
		}
    }
	
	public void Harvest()
	{
		animateDeath = true;
		GetComponent<BoxCollider>().enabled = false;
	}
}
