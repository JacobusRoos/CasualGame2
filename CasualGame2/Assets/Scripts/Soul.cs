using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        baseColor = transform.GetComponent<Image>().color;
        //transform.parent = null;
        //transform.localRotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
        //transform.parent = plot.transform;

    }
	
	public void OnClick()
	{
		Debug.Log("Click On Soul - " + gameObject.name);
		if(!plot.GetComponent<Plot>().playerManager.gameManager.QuickHarvest)
		{
			plot.GetComponent<Plot>().playerManager.gameManager.SelectSoul(gameObject);
		}
		else
		{
			if (timeToRipe <= 0)
			{
				plot.GetComponent<Plot>().playerManager.ChangeEctoplasm(ectoPerHarvest);
				plot.GetComponent<Plot>().playerManager.ChangeExperience(20);
			}
			plot.GetComponent<Plot>().RemoveFromPlot(gameObject);
			Harvest();
		}
	}

    // Update is called once per frame
    void Update()
    {
		if(animateDeath)
		{
			transform.localScale += new Vector3(0.002f, 0.004f, 0);
			transform.position += new Vector3(0, 0, 0.01f);
			Color color = transform.GetComponent<Image>().color;
			color.a -= .02f;
			transform.GetComponent<Image>().color = color;
			if(color.a <= 0)
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
				transform.GetComponent<Image>().color = matureColor;
			}
			else
			{
				transform.GetComponent<Image>().color = baseColor;
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
		GetComponent<Button>().enabled = false;
	}
}
