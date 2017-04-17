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

    private GameObject selectedSoul;
    public GameObject selectedImage;
    private bool soulIsSelected;

    public GameObject soulMenu;

	private Vector3 prevMousePosition;
	public Vector4 limit;
	
	private Vector2 distanceTraveled = new Vector2(0, 0);
	
	private bool canMove;

	// Use this for initialization
	void Start ()
    {
        selectedSoul = null;
        soulIsSelected = false;
        selectedImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selectedSoul == null)
        {
            soulMenu.GetComponent<SoulMenu>().Hide();

            soulIsSelected = false;

            selectedSoul = null;

            selectedImage.SetActive(false);
        }

        if(soulIsSelected)
        {
            soulMenu.transform.GetChild(1).GetComponent<Text>().text = ((int)selectedSoul.GetComponent<Soul>().lifespan).ToString();
        }

		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            selectedImage.SetActive(false);



            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Soul")
                {
                    SelectSoul(hit.collider.gameObject);
<<<<<<< HEAD
                    //if (hit.collider.GetComponent<Soul>().timeToRipe <= 0)
                    //{
                    //    playerManager.ChangeEctoplasm(hit.collider.GetComponent<Soul>().ectoPerHarvest);
                    //    playerManager.ChangeExperience(20);
                    //}
                    //hit.collider.GetComponent<Soul>().plot.GetComponent<Plot>().RemoveFromPlot(hit.collider.gameObject);
=======
                    if (hit.collider.GetComponent<Soul>().timeToRipe <= 0)
                    {
                        playerManager.ChangeEctoplasm(hit.collider.GetComponent<Soul>().ectoPerHarvest);
                        playerManager.ChangeExperience(20);
                    }
                    hit.collider.GetComponent<Soul>().plot.GetComponent<Plot>().RemoveFromPlot(hit.collider.gameObject);
					hit.collider.GetComponent<Soul>().Harvest();
>>>>>>> 3f707d402a863fc2ad24926c390a2ad7cb350549
                    //Destroy(hit.collider.gameObject);
                }
                else
                {
                    soulMenu.GetComponent<SoulMenu>().Hide();

                    soulIsSelected = false;

                    selectedSoul = null;

                    selectedImage.SetActive(false);
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
			if(canMove)
			{
				if(distanceTraveled.x + .25f * (Input.mousePosition.x - prevMousePosition.x) < 30 && distanceTraveled.x + .25f * (Input.mousePosition.x - prevMousePosition.x) > -30)
				{
					Camera.main.transform.Translate((.25f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.x += .25f * (Input.mousePosition.x - prevMousePosition.x);
				}
				if(distanceTraveled.y + .25f * (Input.mousePosition.y - prevMousePosition.y) < 30 && distanceTraveled.y + .25f * (Input.mousePosition.y - prevMousePosition.y) > -30)
				{
					Camera.main.transform.Translate(-(.25f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.y += .25f * (Input.mousePosition.y - prevMousePosition.y);
				}
			}
            //combines the previous 2 lines into 1
            //Camera.main.transform.Translate((.5f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * 40)) - (.5f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * 40)), 0, (.5f * (Input.mousePosition.y - prevMousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * 40)) + (.5f * (Input.mousePosition.x - prevMousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * 40)), Space.World);
			if(canMove || Vector2.Distance(Input.mousePosition, prevMousePosition) > 75)
			{
				prevMousePosition = Input.mousePosition;
				canMove = true;
			}
        }
		if(!Input.GetMouseButton(0))
        {
			canMove = false;
		}
	}

    private void SelectSoul(GameObject Soul)
    {
        RectTransform GUIRect = GUICanvas.GetComponent<RectTransform>();

        Vector2 viewPosition = mainCamera.WorldToViewportPoint(Soul.transform.position);

        Vector2 soulScreenpos = new Vector2(((viewPosition.x * GUIRect.sizeDelta.x) - (GUIRect.sizeDelta.x * .5f)), ((viewPosition.y * GUIRect.sizeDelta.y) - (GUIRect.sizeDelta.y * .5f)));

        selectedImage.GetComponent<RectTransform>().anchoredPosition = soulScreenpos;

        selectedImage.SetActive(true);

        selectedSoul = Soul;

        soulIsSelected = true;

        DisplaySelectedSoulInfo();
    }

    private void DisplaySelectedSoulInfo()
    {
        soulMenu.GetComponent<SoulMenu>().Show();

        soulMenu.transform.GetChild(0).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerSecond.ToString();

        soulMenu.transform.GetChild(1).GetComponent<Text>().text = ((int)selectedSoul.GetComponent<Soul>().lifespan).ToString();

        soulMenu.transform.GetChild(2).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerHarvest.ToString();
    }

    

    /// <summary>
    /// Need an easy way to exit the game to avoid Android doing stupid things
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
