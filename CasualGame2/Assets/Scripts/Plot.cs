using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plot : MonoBehaviour
{
    public int cost;
    public int capacity;
    public float extraMult;
    public PlayerManager playerManager;
    List<GameObject> soulContent = new List<GameObject>();
	public List<GameObject> bonusType = new List<GameObject>();
	public List<float> bonusAmount = new List<float>();

	// Use this for initialization
	void Start ()
    {
        soulContent = new List<GameObject>();
	}

    public void OnClick()
    {
        if (!playerManager.gameManager.QuickHarvest)
        {
            playerManager.gameManager.soulIsSelected = false;
            //AddToPlot(playerManager.gameManager.soulPrefab);
            if (GameObject.Find("GUI").transform.FindChild("SoulSelect").gameObject.activeSelf && playerManager.selectedPlot == gameObject)
            {
                playerManager.selectedPlot = null;
                GameObject.Find("GUI").transform.FindChild("QuickHarvest").gameObject.SetActive(true);
                GameObject.Find("GUI").transform.FindChild("ToPlayer").gameObject.SetActive(true);
            }
            else
            {
                playerManager.selectedPlot = gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddToPlot(GameObject soul)
    {
        if (!playerManager.GetComponent<PlayerManager>().gameManager.GetComponent<GameManager>().QuickHarvest && !IsFull() && playerManager.CanAfford(soul.GetComponent<Soul>().cost ))
        {
            GameObject newSoul = Instantiate(soul, transform);
            bool[] freePositions = new bool[capacity];
            for(int i = 0; i < capacity; i++)
            {
                freePositions[i] = true;
            }
            float spread = 4.5f / ((capacity / 2) + 1);
            foreach(GameObject obj in soulContent)
            {
                int pos = (int)((obj.transform.localPosition.x + 2f) / 4f);
                int pos2 = -(int)((obj.transform.localPosition.y - (spread * (capacity / 2) - 1)) / (spread * 2));
                freePositions[(pos) + (pos2 * 2)] = false;
            }
            int closestFree = 0;
            while(!freePositions[closestFree])
            {
                closestFree++;
            }
            newSoul.transform.localPosition = new Vector3(-2f + (float)((closestFree % 2) * 4), (spread * (capacity / 2) - 1) - (Mathf.Floor(closestFree / 2) * (spread * 2)), -2);
            newSoul.GetComponent<Soul>().plot = gameObject;
            playerManager.ChangeEctoplasm(-newSoul.GetComponent<Soul>().cost, false);
            playerManager.ChangeExperience(10);
            soulContent.Add(newSoul);
        }
    }

    public GameObject AddToPlotDirect(GameObject soul)
    {
        GameObject newSoul = Instantiate(soul, transform);
        bool[] freePositions = new bool[capacity];
        for (int i = 0; i < capacity; i++)
        {
            freePositions[i] = true;
        }
        float spread = 4.5f / ((capacity / 2) + 1);
        foreach (GameObject obj in soulContent)
        {
            int pos = (int)((obj.transform.localPosition.x + 2f) / 4f);
            int pos2 = -(int)((obj.transform.localPosition.y - (spread * (capacity / 2) - 1)) / (spread * 2));
            freePositions[(pos) + (pos2 * 2)] = false;
        }
        int closestFree = 0;
        while (!freePositions[closestFree])
        {
            closestFree++;
        }
        newSoul.transform.localPosition = new Vector3(-2f + (float)((closestFree % 2) * 4), (spread * (capacity / 2) - 1) - (Mathf.Floor(closestFree / 2) * (spread * 2)), -2);
        newSoul.GetComponent<Soul>().plot = gameObject;
        soulContent.Add(newSoul);
        return newSoul;
    }

    public void RemoveFromPlot(GameObject soul)
    {
        if (soulContent.Contains(soul))
        {
            soulContent.Remove(soul);
        }
    }

    public List<GameObject> SoulContent
    {
        get
        {
            return soulContent;
        }
    }
    bool IsEmpty()
    {
        return soulContent.Count == 0;
    }
    bool IsFull()
    {
        return soulContent.Count == capacity;
    }
}
