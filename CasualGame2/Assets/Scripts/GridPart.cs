using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPart : MonoBehaviour 
{
	public GameManager gameManager;
    public GameObject plotMenu;
	
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
		if(transform.childCount == 0)
		{
            gameManager.ShowPlotMenu();
            gameManager.playerManager.DisplayPlotMenu(gameObject);
		}
	}
}
