using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float ectoplasm;
    public int level = 1;
    public int experience;
    int nextLevelExperience;
    int maxPlots = 2;
    List<GameObject> plotList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        ectoplasm = 50;
        level = 1;
        experience = 0;
        nextLevelExperience = 500;
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool anySouls = false;
		foreach(GameObject plot in plotList)
        {
            foreach(GameObject soul in plot.GetComponent<Plot>().SoulContent)
            {
                ectoplasm += soul.GetComponent<Soul>().ectoPerSecond * Time.deltaTime;
                anySouls = true;
            }
        }
        if(!anySouls && ectoplasm < 35)
        {
            ectoplasm += .1f * Time.deltaTime;
        }
        if(experience >= nextLevelExperience)
        {
            LevelUp();
        }
	}

    public void AddPlot(GameObject plot, Vector3 position)
    {
        if (!IsFull && CanAfford(plot.GetComponent<Plot>().cost))
        {
            GameObject newPlot = Instantiate(plot, position, Quaternion.identity);
            newPlot.GetComponent<Plot>().playerManager = this;
            ChangeEctoplasm(-newPlot.GetComponent<Plot>().cost);
            ChangeExperience(150);
            plotList.Add(newPlot);
        }
    }

    public bool IsFull
    {
        get
        {
            return plotList.Count == maxPlots;
        }
    }
    public bool CanAfford(float cost)
    {
        return ectoplasm >= cost;
    }

    //use these to get public variables
    public float Ectoplasm
    {
        get
        {
            return ectoplasm;
        }
    }
    public int Experience
    {
        get
        {
            return experience;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
    }

    //use these to change public variables
    public void ChangeEctoplasm(float change)
    {
        ectoplasm += change;
    }
    public void ChangeExperience(int change)
    {
        experience += change;
    }
    public void LevelUp()
    {
        level++;
        experience = 0;
        nextLevelExperience = level * 1000;
        if(level % 2 == 0)
        {
            maxPlots += 1;
        }
    }
}
