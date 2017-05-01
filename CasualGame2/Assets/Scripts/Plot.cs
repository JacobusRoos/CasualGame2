using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Plot : MonoBehaviour
{
    public int cost;
    public int capacity;
    public PlayerManager playerManager;
    List<GameObject> soulContent = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        soulContent = new List<GameObject>();
	}
	
	public void OnClick()
	{
		Debug.Log("Click On Plot - " + gameObject.name);
		AddToPlot(playerManager.gameManager.soulPrefab);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddToPlot(GameObject soul)
    {
        if (!IsFull() && playerManager.CanAfford(soul.GetComponent<Soul>().cost))
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
            playerManager.ChangeEctoplasm(-newSoul.GetComponent<Soul>().cost);
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
