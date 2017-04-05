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

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        timeToRipe -= Time.deltaTime;
        if(timeToRipe <= 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(214f/255, 214f/255, 255f/255, 200f/255);
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255f / 255, 214f / 255, 214f / 255, 200f / 255);
        }
        if (lifespan <= 0)
        {
            plot.GetComponent<Plot>().RemoveFromPlot(gameObject);
            Destroy(gameObject);
        }
    }
}
