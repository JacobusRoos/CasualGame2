using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject plotPrefab;
    public GameObject soulPrefab;
    public PlayerManager playerManager;

	// Use this for initialization
	void Start ()
    {
		
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
                if(hit.collider.tag == "PlotPoint")
                {
                    hit.collider.GetComponent<Plot>().AddToPlot(soulPrefab);
                }
                if (hit.collider.tag == "Ground")
                {
                    Vector2 hitPoint = new Vector2(Mathf.Floor(hit.point.x / 10), Mathf.Floor(hit.point.z / 10));
                    playerManager.AddPlot(plotPrefab, new Vector3((hitPoint.x * 10) + 5, 0.05f, (hitPoint.y * 10) + 5));
                }
            }
        }
	}
}
