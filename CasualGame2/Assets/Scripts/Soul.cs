using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Soul : MonoBehaviour
{
    public int cost;
    public float ectoPerSecond;
    public float ectoPerHarvest;
    public float lifespan;
    private float maxLifespan;
    public float timeToRipe;
    public GameObject plot;
    public Color baseColor;
    public Color matureColor;
	bool animateDeath = false;

    public GameObject soulFade;

    // Use this for initialization
    void Start ()
    {
        baseColor = transform.GetComponent<Image>().color;
        //transform.parent = null;
        transform.rotation = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;

        maxLifespan = lifespan;
        //transform.parent = plot.transform;

        //ectoPerSecond = 2;

        //ectoPerHarvest = 10;
    }
	
	public void OnClick()
	{
		if(!plot.GetComponent<Plot>().playerManager.gameManager.QuickHarvest)
		{
			plot.GetComponent<Plot>().playerManager.gameManager.SelectSoul(gameObject);
		}
		else
		{
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
        if (timeToRipe <= 0)
        {
			float harvestMult = plot.GetComponent<Plot>().extraMult;
			for(int i = 0; i < plot.GetComponent<Plot>().bonusType.Count; i++)
			{
				if(plot.GetComponent<Plot>().bonusType[i].name + "(Clone)" == name)
				{
					harvestMult += plot.GetComponent<Plot>().bonusAmount[i] - 1;
				}
			}
            plot.GetComponent<Plot>().playerManager.ChangeEctoplasm(ectoPerHarvest * harvestMult, true);
            plot.GetComponent<Plot>().playerManager.ChangeExperience(50);
        }
        plot.GetComponent<Plot>().RemoveFromPlot(gameObject);
        GameObject fade = Instantiate(soulFade, transform.position, transform.rotation);
        fade.GetComponent<SpriteRenderer>().color = transform.GetComponent<Image>().color;
        fade.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
        Destroy(gameObject);
    }

    public float MaxLifespan
    {
        get
        {
            return maxLifespan;
        }
    }
}
