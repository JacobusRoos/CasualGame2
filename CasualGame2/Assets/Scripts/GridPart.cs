using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPart : MonoBehaviour 
{
	public GameManager gameManager;
<<<<<<< HEAD
    public GameObject plotMenu;
=======
    public bool canPlace;
>>>>>>> refs/remotes/origin/SoulSelect
	
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
<<<<<<< HEAD
		if(transform.childCount == 0)
		{
            gameManager.plotIsSelected = true;
            gameManager.ShowPlotMenu();
            gameManager.playerManager.DisplayPlotMenu(gameObject);
		}
=======
        if (canPlace)
        {
            GameObject.Find("GUI").transform.FindChild("PlotSelect").gameObject.SetActive(true);
            gameManager.selectedGrid = gameObject;
            /*if (transform.childCount == 0)
            {
                gameManager.playerManager.AddPlot(gameManager.plotPrefab, gameObject);
            }*/
        }
>>>>>>> refs/remotes/origin/SoulSelect
	}
}
