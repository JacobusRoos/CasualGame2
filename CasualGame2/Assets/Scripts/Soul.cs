using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public float lifespan;
    public GameObject plot;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            plot.GetComponent<Plot>().RemoveFromPlot(gameObject);
            Destroy(gameObject);
        }
	}
}
