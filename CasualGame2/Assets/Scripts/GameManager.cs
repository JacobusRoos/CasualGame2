using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject plotPrefab;
    public GameObject soulPrefab;
    public PlayerManager playerManager;

    public Camera mainCamera;
    public Canvas GUICanvas;

    private GameObject selectedSoul;
    public GameObject selectedImage;
    
    public bool plotIsSelected;

    public bool soulIsSelected;

    public GameObject soulMenu;
    public GameObject characterMenu;

    private Vector3 prevMousePosition;
	public Vector4 limit;
	
	public Vector2 distanceTraveled = new Vector2(0, 0);
	
	private bool canMove;
	private bool quickHarvest = false;
	
	//private Button quickHarvestButton;

    private PointerEventData pointerData;

    private List<RaycastResult> rayResults;

    private string initialTag;
    private int initialRay;

    public GameObject CharacterMenu;

    public GameObject selectedGrid;

    public List<GameObject> allSouls;
    public List<GameObject> allPlots;

    // Use this for initialization
    void Start ()
    {
        selectedSoul = null;
        soulIsSelected = false;
        LoadSave();
        plotIsSelected = false;
        selectedImage.SetActive(false);

        rayResults = new List<RaycastResult>();

        characterMenu.GetComponent<CharacterMenu>().Hide();

        initialTag = "";
        initialRay = 0;

        //quickHarvestButton = GameObject.Find("GUI").transform.FindChild("QuickHarvest").GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(selectedSoul);

        if ((selectedSoul == null || !soulIsSelected) && !plotIsSelected)
        {
            soulMenu.GetComponent<SoulMenu>().Hide();

            soulIsSelected = false;

            selectedSoul = null;

            selectedImage.SetActive(false);
        }

        if(soulIsSelected)
        {
            soulMenu.transform.GetChild(1).GetComponent<Slider>().value = selectedSoul.GetComponent<Soul>().lifespan;
            soulMenu.transform.GetChild(1).GetComponent<Slider>().transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(1-(selectedSoul.GetComponent<Soul>().lifespan / selectedSoul.GetComponent<Soul>().MaxLifespan), selectedSoul.GetComponent<Soul>().lifespan/ selectedSoul.GetComponent<Soul>().MaxLifespan, 0, 1);
            soulMenu.transform.GetChild(2).GetComponent<Text>().text = ((int)selectedSoul.GetComponent<Soul>().lifespan).ToString() + "s left alive";
        }

        if (Input.GetMouseButtonDown(0))
        {
            pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition;

            EventSystem.current.RaycastAll(pointerData, rayResults);

            prevMousePosition = Input.mousePosition;

            if(rayResults.Count > 0)
            {
                initialTag = rayResults[0].gameObject.tag;
            }

            initialRay = rayResults.Count;
        }


		if(Input.GetMouseButton(0))
        {
            //click on nothing
            if (initialRay == 0)
            {
                soulIsSelected = false;
                playerManager.selectedPlot = null;
                selectedGrid = null;
            }
            //click on ground
            else if(initialTag == "PlotPoint")
            {
                //Debug.Log("soul is not clicked");
                soulIsSelected = false;
                playerManager.selectedPlot = null;
            }


            if(initialRay == 0 || initialTag == "Soul")
            {
                plotIsSelected = false;
            }

            if (canMove)
			{
                /*if(distanceTraveled.x + (.25f * (prevMousePosition.x - Input.mousePosition.x)) < 30 && distanceTraveled.x + (.25f * (prevMousePosition.x - Input.mousePosition.x)) > -30)
				{
					Camera.main.transform.Translate((.15f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.15f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.x += .15f * (prevMousePosition.x - Input.mousePosition.x);
				}
				if(distanceTraveled.y + (.25f * (prevMousePosition.y - Input.mousePosition.y)) < 30 && distanceTraveled.y + (.25f * (prevMousePosition.y - Input.mousePosition.y)) > -30)
				{
					Camera.main.transform.Translate(-(.15f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.15f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.y += .15f * (prevMousePosition.y - Input.mousePosition.y);
                }*/
                Camera.main.transform.Translate((.15f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.15f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
                Camera.main.transform.Translate(-(.15f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.15f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
                if (Camera.main.transform.position.x > 60)
                {
                    Camera.main.transform.position = new Vector3(60, Camera.main.transform.position.y, Camera.main.transform.position.z);
                }
                if (Camera.main.transform.position.x < 0)
                {
                    Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
                }
                if (Camera.main.transform.position.z > 0)
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
                }
                if (Camera.main.transform.position.z < -60)
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -60);
                }
            }
            //combines the previous 2 lines into 1
            //Camera.main.transform.Translate((.5f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * 40)) - (.5f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * 40)), 0, (.5f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * 40)) + (.5f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * 40)), Space.World);
			if(canMove || Vector2.Distance(Input.mousePosition, prevMousePosition) > 10 && (initialRay == 0 || initialTag == "PlotPoint"))
			{
				prevMousePosition = Input.mousePosition;
				canMove = true;
			}
        }
		if(!Input.GetMouseButton(0))
        {
			canMove = false;
        }
        if (playerManager.selectedPlot == null && selectedGrid == null)
        {
            GameObject.Find("GUI").transform.FindChild("QuickHarvest").gameObject.SetActive(true);
            GameObject.Find("GUI").transform.FindChild("ToPlayer").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("GUI").transform.FindChild("QuickHarvest").gameObject.SetActive(false);
            GameObject.Find("GUI").transform.FindChild("ToPlayer").gameObject.SetActive(false);
        }
        if (selectedGrid != null)
        {
            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(true);
        }
        else
        {
            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(false);
        }
    }

    public void CloseSoulSelect()
    {
        playerManager.selectedPlot = null;
    }
    public void ClosePlotSelect()
    {
        selectedGrid = null;
    }

    public void SelectSoul(GameObject Soul)
    {
        playerManager.selectedPlot = null;

        RectTransform GUIRect = GUICanvas.GetComponent<RectTransform>();

        Vector2 viewPosition = mainCamera.WorldToViewportPoint(Soul.transform.position);

        Vector2 soulScreenpos = new Vector2(((viewPosition.x * GUIRect.sizeDelta.x) - (GUIRect.sizeDelta.x * .5f)), ((viewPosition.y * GUIRect.sizeDelta.y) - (GUIRect.sizeDelta.y * .5f)));

        selectedImage.GetComponent<RectTransform>().anchoredPosition = soulScreenpos;

        selectedImage.SetActive(true);

        selectedSoul = Soul;

        soulIsSelected = true;

        DisplaySelectedSoulInfo();
    }

    public void HarvestSelectedSoul()
    {
        selectedSoul.GetComponent<Soul>().Harvest();

        selectedSoul = null;

        GameObject.Find("GUI").transform.FindChild("QuickHarvest").gameObject.SetActive(true);
        GameObject.Find("GUI").transform.FindChild("ToPlayer").gameObject.SetActive(true);
    }

    private void DisplaySelectedSoulInfo()
    {
        soulMenu.GetComponent<SoulMenu>().Show();

        soulMenu.transform.GetChild(0).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerSecond.ToString() + " Ecto per second";

        soulMenu.transform.GetChild(1).GetComponent<Slider>().maxValue = selectedSoul.GetComponent<Soul>().MaxLifespan;
        soulMenu.transform.GetChild(1).GetComponent<Slider>().value = selectedSoul.GetComponent<Soul>().lifespan;

        soulMenu.transform.GetChild(2).GetComponent<Text>().text = ((int)selectedSoul.GetComponent<Soul>().lifespan).ToString() + "s left alive";

        soulMenu.transform.GetChild(3).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerHarvest.ToString() + " Ecto on Harvest";
    }

    public void ToggleQuickHarvest()
	{
		quickHarvest = !quickHarvest;
	}
    
    void OnApplicationFocus(bool gainedFocus)
    {
        if (!gainedFocus) SaveGame();
    }
	
    void OnApplicationPause(bool pausing)
    {
        if (pausing) SaveGame();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        try
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "newsave3.sav"));
            SaveData save = new SaveData() {
                CreationTimestamp = System.DateTime.UtcNow,
                Ectoplasm = playerManager.Ectoplasm,
                Level = playerManager.Level,
                Experience = playerManager.Experience,
                ScytheRank = playerManager.scytheRank,
                Plots = new Dictionary<int, SerializablePlot>()
            };
            foreach(var plotObj in playerManager.Plots)
            {
                var plot = plotObj.GetComponent<Plot>();
                SerializablePlot splot = new SerializablePlot() { Souls = new SerializableSoul[plot.SoulContent.Count] };
                switch(plot.cost)
                {
                    case 35:
                        splot.Type = SerializablePlot.PlotType.Base;
                        break;
                    case 125:
                        splot.Type = SerializablePlot.PlotType.City;
                        break;
                    case 300:
                        splot.Type = SerializablePlot.PlotType.Moon;
                        break;
                }
                for(int i = 0; i < plot.SoulContent.Count; i++)
                {
                    var soul = plot.SoulContent[i].GetComponent<Soul>();
                    SerializableSoul ssoul = new SerializableSoul() {
                        EctoPerHarvest = soul.ectoPerHarvest,
                        EctoPerSecond = soul.ectoPerSecond,
                        Lifespan = soul.lifespan,
                        TimeToRipe = soul.timeToRipe,
                        BaseColor = soul.baseColor,
                        MatureColor = soul.matureColor
                    };
                    switch(soul.cost)
                    {
                        case 10:
                            ssoul.Type = SerializableSoul.SoulType.Base;
                            break;
                        case 25:
                            ssoul.Type = SerializableSoul.SoulType.College;
                            break;
                        case 50:
                            ssoul.Type = SerializableSoul.SoulType.Construction;
                            break;
                        case 100:
                            ssoul.Type = SerializableSoul.SoulType.Astronaut;
                            break;
                    }
                    splot.Souls[i] = ssoul;
                }
                var parentGrid = plotObj.GetComponentInParent<GridPart>();
                save.Plots.Add(parentGrid.GetInstanceID(), splot);
            }
            bf.Serialize(file, save);
            file.Close();
        }
        catch (IOException ex)
        {
            Debug.Log("There was an error thrown by the OS when trying to save! Exception: " + ex.Message);
        }
    }

    private void LoadSave()
    {
        try
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            FileStream file = File.OpenRead(Path.Combine(Application.persistentDataPath, "newsave3.sav"));
            object rawsave = bf.Deserialize(file);
            SaveData save = (SaveData)rawsave;
            playerManager.ectoplasm = save.Ectoplasm;
            for(int i = save.Level; i > 1; i--)
            {
                playerManager.LevelUp();
            }
            playerManager.scytheRank = save.ScytheRank;
            playerManager.experience = save.Experience;

            TimeSpan ts = DateTime.UtcNow - save.CreationTimestamp;
            float deltaTime = (float)ts.TotalSeconds;

            foreach(var plot in save.Plots)
            {
                GameObject instantiated = playerManager.AddPlotDirect(allPlots[(int)plot.Value.Type], plot.Key);
                Plot newPlot = instantiated.GetComponent<Plot>();
                foreach(var savedSoul in plot.Value.Souls)
                {
                    GameObject instantiatedSoul = newPlot.AddToPlotDirect(allSouls[(int)savedSoul.Type]);
                    Soul newSoul = instantiatedSoul.GetComponent<Soul>();
                    newSoul.ectoPerHarvest = savedSoul.EctoPerHarvest;
                    newSoul.ectoPerSecond = savedSoul.EctoPerSecond;
                    newSoul.lifespan = savedSoul.Lifespan - deltaTime;
                    newSoul.timeToRipe = savedSoul.TimeToRipe - deltaTime;
                    newSoul.baseColor = savedSoul.BaseColor;
                    newSoul.transform.GetComponent<Image>().color = savedSoul.BaseColor;
                    newSoul.matureColor = savedSoul.MatureColor;
                    if (newSoul.lifespan < 0) playerManager.ectoplasm += savedSoul.Lifespan * newSoul.ectoPerSecond;
                    else playerManager.ectoplasm += deltaTime * newSoul.ectoPerSecond;
                }
            }
            file.Close();
        }
        catch(FileNotFoundException)
        {
            Debug.Log("No save file found, ignoring.");
        }
        catch (IOException ex)
        {
            Debug.Log("There was an error thrown by the OS when trying to load the save file! Exception: " + ex.Message);
        }
	}

    public bool QuickHarvest
	{
		get
		{
			return quickHarvest;
		}
    }

    public void SetCharacterMenu(bool activation)
    {
        if(activation)
        {
            characterMenu.GetComponent<CharacterMenu>().Show();
        }
        else
        {
            characterMenu.GetComponent<CharacterMenu>().Hide();
            activation = true;
        }
    }

    public void SetCredits(bool active)
    {
        GameObject.Find("Credits").SetActive(active);
    }

    /// <summary>
    /// Need an easy way to exit the game to avoid Android doing stupid things
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
