using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float ectoplasm;
    public int level = 1;
    public int experience;
    int nextLevelExperience;
    int maxPlots = 2;
    GameManager gameManager;
    List<GameObject> plotList = new List<GameObject>();
    public List<int> levelUnlocks = new List<int>();
    public List<GameObject> levelSouls = new List<GameObject>();

    public GameObject PlayerInfoUI;

    private string[] ectoplasmNotation;

    // Use this for initialization
    void Start ()
    {
        ectoplasm = 50000;
        level = 1;
        experience = 0;
        nextLevelExperience = 500;
         
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        PlayerInfoUI.transform.GetChild(0).GetComponent<Text>().text = "Level " + level;

        PlayerInfoUI.transform.GetChild(1).GetComponent<Text>().text = (int)(ectoplasm) + "";

        ectoplasmNotation = new string[]{ "K", "M", "B", "T", "Qa", "Qi"};
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

        PlayerInfoUI.transform.GetChild(1).GetComponent<Text>().text = GenerateEctoplasmString();
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
        nextLevelExperience = level * 500;
        if(level % 2 == 0)
        {
            maxPlots += 1;
        }

        if(levelUnlocks.Contains(level))
        {
            gameManager.soulPrefab = levelSouls[levelUnlocks.IndexOf(level)];
        }
    }

    private string GenerateEctoplasmString()
    {
        string ectoplasmString = "";

        int tenFactor = 0;

        int ectoplasmHolder = (int)ectoplasm;

        while(ectoplasmHolder > 1)
        {
            ectoplasmHolder /= 10;

            tenFactor++;
        }

        if(tenFactor <= 4)
        {
            ectoplasmString = ((int)ectoplasm).ToString();
        }
        else
        {
            ectoplasmHolder = (int)ectoplasm / (10 ^ (tenFactor - 3));


            ectoplasmString = (float)ectoplasmHolder / 1000 + ectoplasmNotation[tenFactor / 3];
        }

        return ectoplasmString;
    }
}
