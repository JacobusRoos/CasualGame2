using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject plotPrefab;
    public GameObject soulPrefab;
    public PlayerManager playerManager;

    public Camera mainCamera;
    public Canvas GUICanvas;

    public GameObject selectedSoul;
    public Image selectedImage;
    private bool soulIsSelected;
	
	private Vector3 prevMousePosition;
	public Vector4 limit;

	// Use this for initialization
	void Start ()
    {
        selectedSoul = null;
        soulIsSelected = false;
        LoadSave();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Soul")
                {
                    SelectSoul(hit.collider.gameObject);
                    if (hit.collider.GetComponent<Soul>().timeToRipe <= 0)
                    {
                        playerManager.ChangeEctoplasm(hit.collider.GetComponent<Soul>().ectoPerHarvest);
                        playerManager.ChangeExperience(20);
                    }
                    hit.collider.GetComponent<Soul>().plot.GetComponent<Plot>().RemoveFromPlot(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "PlotPoint")
                {
                    if (hit.collider.GetComponent<Plot>() == null)
                    {
                        hit.collider.transform.parent.parent.GetComponent<Plot>().AddToPlot(soulPrefab);
                    }
                    else
                    {
                        hit.collider.transform.parent.GetComponent<Plot>().AddToPlot(soulPrefab);
                    }
                }
                if (hit.collider.tag == "Ground")
                {
                    Vector2 hitPoint = new Vector2(Mathf.Floor(hit.point.x / 10), Mathf.Floor(hit.point.z / 10));
                    playerManager.AddPlot(plotPrefab, new Vector3((hitPoint.x * 10) + 5, 0.05f, (hitPoint.y * 10) + 5));
                }
            }
		    prevMousePosition = Input.mousePosition;
        }
		
		if(Input.GetMouseButton(0))
        {
			Camera.main.transform.Translate((.25f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
			Camera.main.transform.Translate(-(.25f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
            //combines the previous 2 lines into 1
            //Camera.main.transform.Translate((.5f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * 40)) - (.5f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * 40)), 0, (.5f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * 40)) + (.5f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * 40)), Space.World);
            prevMousePosition = Input.mousePosition;
        }
	}

    private void SelectSoul(GameObject Soul)
    {
        RectTransform GUIRect = GUICanvas.GetComponent<RectTransform>();

        Vector2 viewPosition = mainCamera.WorldToViewportPoint(Soul.transform.position);

        Vector2 soulScreenpos = new Vector2(((viewPosition.x * GUIRect.sizeDelta.x) - (GUIRect.sizeDelta.x * .5f)), ((viewPosition.y * GUIRect.sizeDelta.y) - (GUIRect.sizeDelta.y * .5f)));

        selectedImage.GetComponent<RectTransform>().anchoredPosition = soulScreenpos;
    }

    /// <summary>
    /// Called automatically by Unity when the app is switched out of
    /// </summary>
    void OnApplicationPause(bool pausing)
    {
        if (pausing) SaveGame();
    }

    public void SaveGame()
    {
        try
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "newsave.sav"));
            SaveData save = new SaveData() {
                CreationTimestamp = System.DateTime.UtcNow,
                Plots = new Dictionary<SerializableVector3, SerializablePlot>()
            };
            foreach(var plotObj in playerManager.Plots)
            {
                var plot = plotObj.GetComponent<Plot>();
                SerializablePlot splot = new SerializablePlot() { Souls = new SerializableSoul[plot.SoulContent.Count] };
                for(int i = 0; i < plot.SoulContent.Count; i++)
                {
                    var soul = plot.SoulContent[i].GetComponent<Soul>();
                    SerializableSoul ssoul = new SerializableSoul() {
                        EctoPerHarvest = soul.ectoPerHarvest,
                        EctoPerSecond = soul.ectoPerSecond,
                        Lifespan = soul.lifespan,
                        TimeToRipe = soul.timeToRipe
                    };
                    splot.Souls[i] = ssoul;
                }
                save.Plots.Add(plotObj.transform.position, splot);
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
            FileStream file = File.OpenRead(Path.Combine(Application.persistentDataPath, "newsave.sav"));
            object rawsave = bf.Deserialize(file);
            SaveData save = (SaveData)rawsave;
            foreach(var plot in save.Plots)
            {
                GameObject instantiated = playerManager.AddPlotDirect(plotPrefab, plot.Key);
                Plot newPlot = instantiated.GetComponent<Plot>();
                foreach(var savedSoul in plot.Value.Souls)
                {
                    GameObject instantiatedSoul = newPlot.AddToPlotDirect(soulPrefab);
                    Soul newSoul = instantiatedSoul.GetComponent<Soul>();
                    newSoul.ectoPerHarvest = savedSoul.EctoPerHarvest;
                    newSoul.ectoPerSecond = savedSoul.EctoPerSecond;
                    newSoul.lifespan = savedSoul.Lifespan;
                    newSoul.timeToRipe = savedSoul.TimeToRipe;
                }
            }
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

    /// <summary>
    /// Need an easy way to exit the game to avoid Android doing stupid things
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
