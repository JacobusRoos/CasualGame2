using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int ectoplasm;
    int level = 1;
    int experience;
    int maxPlots = 2;
    List<GameObject> plotList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddPlot(GameObject plot, Vector3 position)
    {
        if (!IsFull)
        {
            GameObject newPlot = Instantiate(plot, position, Quaternion.identity);
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

    //use these to get public variables
    public int Ectoplasm
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
    public void ChangeEctoplasm(int change)
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
    }
}
