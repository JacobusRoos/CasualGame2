using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddToPlot(GameObject soul)
    {
        if (!IsFull() && playerManager.CanAfford(soul.GetComponent<Soul>().cost))
        {
            GameObject newSoul = Instantiate(soul, transform);
            bool[] freePositions = new bool[8];
            for(int i = 0; i < 8; i++)
            {
                freePositions[i] = true;
            }
            foreach(GameObject obj in soulContent)
            {
                int pos = (int)((obj.transform.localPosition.x + .25f) / .5f);
                int pos2 = -(int)((obj.transform.localPosition.z - .3f) / .2f);
                freePositions[(pos) + (pos2 * 2)] = false;
            }
            int closestFree = 0;
            while(!freePositions[closestFree])
            {
                closestFree++;
            }
            newSoul.transform.localPosition = new Vector3(-0.25f + (float)((closestFree % 2) * .5), 13.5f, .3f - (Mathf.Floor(closestFree / 2) * .2f));
            newSoul.GetComponent<Soul>().plot = gameObject;
            playerManager.ChangeEctoplasm(-newSoul.GetComponent<Soul>().cost);
            playerManager.ChangeExperience(10);
            soulContent.Add(newSoul);
        }
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
