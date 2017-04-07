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
