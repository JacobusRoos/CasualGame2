using System.Collections;
using System.Collections.Generic;
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

	// Use this for initialization
	void Start ()
    {
        selectedSoul = null;
        soulIsSelected = false;
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
    /// Need an easy way to exit the game to avoid Android doing stupid things
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
