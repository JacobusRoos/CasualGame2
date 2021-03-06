﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float ectoplasm;
    public int level = 1;
    public int experience;
    public float scytheRank;
    float maxScytheRank;
    int nextScytheLevel;
    int nextLevelExperience;
    int maxPlots = 2;
    public GameManager gameManager;
    List<GameObject> plotList = new List<GameObject>();
    public List<GameObject> startingStuff = new List<GameObject>();
    public List<int> levelUnlocks = new List<int>();
    Dictionary<int, GridPart> idToGrid = new Dictionary<int, GridPart>();
    public List<GameObject> levelObjects = new List<GameObject>();
    public List<GameObject> availablePlots = new List<GameObject>();
    public List<GameObject> availableSouls = new List<GameObject>();

    public GameObject PlayerInfoUI;
    public GameObject CharacterMenu;

    public GameObject selectedPlot;

    private string[] ectoplasmNotation;

    // Use this for initialization
    void Start ()
    {
        ectoplasm = 50.01f;
        level = 1;
        experience = 0;
        nextLevelExperience = 500;
        
        scytheRank = 1;
        maxScytheRank = 1;
        nextScytheLevel = 2;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        ectoplasmNotation = new string[]{ "K", "M", "B", "T", "Qa", "Qi"};

        var grid = GameObject.FindGameObjectWithTag("Grid");
        foreach(var cell in grid.GetComponentsInChildren<GridPart>())
        {
            idToGrid.Add(cell.GetInstanceID(), cell);
        }
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
        if(!anySouls && ectoplasm < 10)
        {
            ectoplasm = 10;
        }
        if(experience >= nextLevelExperience)
        {
            LevelUp();
        }

        UpdateUI();
    }
    
    private int nextScytheCost()
    {
        return (int)(3000 * (scytheRank - .9f));
    }

    public void UpgradeScythe()
    {
        if (scytheRank < maxScytheRank && ectoplasm >= nextScytheCost())
        {
            ectoplasm -= nextScytheCost();
            scytheRank += .1f;
        }
    }

    public void UpdateUI()
    {
        if(selectedPlot != null)
        {
            GameObject.Find("GUI").transform.FindChild("SoulSelect").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("GUI").transform.FindChild("SoulSelect").gameObject.SetActive(false);
        }

        PlayerInfoUI.transform.GetChild(0).GetComponent<Text>().text = "Level " + level;
        PlayerInfoUI.transform.GetChild(1).GetComponent<Text>().text = GenerateEctoplasmString() + " Ecto";
        PlayerInfoUI.transform.GetChild(2).GetComponent<Slider>().value = experience;
        PlayerInfoUI.transform.GetChild(2).GetComponent<Slider>().maxValue = nextLevelExperience;
        PlayerInfoUI.transform.GetChild(4).GetComponent<Text>().text = (maxPlots - plotList.Count) + "/" + maxPlots;

        CharacterMenu.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Level " + level;
        CharacterMenu.transform.GetChild(0).GetChild(3).GetComponent<Slider>().value = experience;
        CharacterMenu.transform.GetChild(0).GetChild(3).GetComponent<Slider>().maxValue = nextLevelExperience;
        CharacterMenu.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = experience + "/" + nextLevelExperience;
        CharacterMenu.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = "Ectoplasm : " + GenerateEctoplasmString() + "";
        CharacterMenu.transform.GetChild(0).GetChild(7).GetComponent<Text>().text = (maxPlots - plotList.Count) + "/" + maxPlots;
        CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(2).GetComponent<Text>().text = scytheRank + "x";
        if (scytheRank < maxScytheRank)
        {
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 200f/255);
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(4).GetComponent<Text>().text = "Cost : " + nextScytheCost();
        }
        else
        {
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(3).GetComponent<Image>().color = new Color(184f/255, 0, 0, 200f / 255);
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(200f/255, 0, 0, 200f / 255);
            CharacterMenu.transform.GetChild(0).GetChild(8).GetChild(4).GetComponent<Text>().text = "Unlock an upgrade at level " + nextScytheLevel;
        }

        if (level >= 3)
        {
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("College Soul").FindChild("Cover").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("College Soul").GetComponent<Button>().interactable = true;
        }
        if (level >= 7)
        {
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Construct Soul").FindChild("Cover").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Construct Soul").GetComponent<Button>().interactable = true;
        }
        if (level >= 11)
        {
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);
            CharacterMenu.transform.GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Astro Soul").FindChild("Cover").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("SoulSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Astro Soul").GetComponent<Button>().interactable = true;
        }
        if (level >= 5)
        {
            CharacterMenu.transform.GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
            CharacterMenu.transform.GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("PlotSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("City Plot").FindChild("Cover").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("PlotSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("City Plot").GetComponent<Button>().interactable = true;
        }
        if (level >= 10)
        {
            CharacterMenu.transform.GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
            CharacterMenu.transform.GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("PlotSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Moon Plot").FindChild("Cover").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("PlotSelect").GetChild(1).GetChild(0).GetChild(0).FindChild("Moon Plot").GetComponent<Button>().interactable = true;
        }
    }
    public void AddPlot(GameObject plot)
    {
        GameObject parent = gameManager.selectedGrid;
        if (!IsFull && CanAfford(plot.GetComponent<Plot>().cost))
        {
            GameObject newPlot = Instantiate(plot, parent.transform);
            newPlot.transform.localPosition = new Vector3(-.04f, .15f, 0);
            newPlot.GetComponent<Plot>().playerManager = this;
            ChangeEctoplasm(-newPlot.GetComponent<Plot>().cost, false);
            ChangeExperience(150);
            plotList.Add(newPlot);

            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(false);
        }
    }
    public void AddPlot(GameObject plot, GameObject parent)
    {
        if (!IsFull && CanAfford(gameManager.GetComponent<GameManager>().plotPrefab.GetComponent<Plot>().cost))
        {
            GameObject newPlot = Instantiate(gameManager.GetComponent<GameManager>().plotPrefab, parent.transform);
			newPlot.transform.localPosition = new Vector3(-.04f, .15f, 0);
            newPlot.GetComponent<Plot>().playerManager = this;
            ChangeEctoplasm(-newPlot.GetComponent<Plot>().cost, false);
            ChangeExperience(150);
            plotList.Add(newPlot);

            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(false);
        }
    }


    public GameObject AddPlotDirect(GameObject plot, int parentId)
    {
        GridPart parent = idToGrid[parentId];
        GameObject newPlot = Instantiate(plot, parent.transform);
        newPlot.transform.localPosition = new Vector3(-.04f, .15f, 0);
        newPlot.GetComponent<Plot>().playerManager = this;
        plotList.Add(newPlot);
        return newPlot;
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

    public List<GameObject> Plots
    {
        get { return plotList; }
    }

    //use these to change public variables
    public void ChangeEctoplasm(float change, bool harvest)
    {
        if (harvest)
        {
            ectoplasm += (change * scytheRank);
        }
        else
        {
            ectoplasm += change;
        }
    }
    public void ChangeExperience(int change)
    {
        experience += change;
    }
    public void LevelUp()
    {
        level++;
        experience = 0;
        nextLevelExperience = (level * 250) + 250;
        if (level % 2 == 0)
        {
            maxPlots += 1;
        }

        if (levelUnlocks.Contains(level))
        {
            if (levelObjects[levelUnlocks.IndexOf(level)].GetComponent<Plot>() != null)
            {
                availablePlots.Add(levelObjects[levelUnlocks.IndexOf(level)]);
            }
            else
            {
                availableSouls.Add(levelObjects[levelUnlocks.IndexOf(level)]);
            }
            //gameManager.soulPrefab = levelSouls[levelUnlocks.IndexOf(level)];
        }

        if (level == nextScytheLevel)
        {
            maxScytheRank += .1f;
            nextScytheLevel += 3;
        }
    }

    public void ChangeSoul(int id)
    {
        if (id < availableSouls.Count)
        {
            selectedPlot.GetComponent<Plot>().AddToPlot(availableSouls[id]);
        }
    }

    private string GenerateEctoplasmString()
    {
        string ectoplasmString = "";

        int tenFactor = 0;

        long ectoplasmHolder = (long)ectoplasm;

        while(ectoplasmHolder >= 1)
        {
            ectoplasmHolder /= 10;

            tenFactor++;
        }
        

        if(tenFactor <= 4)
        {
            //Debug.Log(tenFactor);

            ectoplasmString = ((int) (ectoplasm * Mathf.Pow(10, 4 - tenFactor)) / Mathf.Pow(10, 4 - tenFactor)).ToString();
        }
        else
        {
            ectoplasmHolder = (long)ectoplasm / (long)(Mathf.Pow(10, tenFactor - 4));


            ectoplasmString = ((float)ectoplasmHolder / (int)(Mathf.Pow(10, 3 - (tenFactor - 1) % 3))) + ectoplasmNotation[(tenFactor - 4) / 3];
        }

        

        return ectoplasmString;
    }
}
