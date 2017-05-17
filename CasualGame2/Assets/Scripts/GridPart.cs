using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPart : MonoBehaviour 
{
	public GameManager gameManager;
    public bool canPlace;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void PlacePlot()
	{
        if (canPlace)
        {
            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(true);
            gameManager.selectedGrid = gameObject;
            /*if (transform.childCount == 0)
            {
                gameManager.playerManager.AddPlot(gameManager.plotPrefab, gameObject);
            }*/
        }
	}
}
