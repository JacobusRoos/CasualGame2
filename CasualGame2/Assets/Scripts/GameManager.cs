using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GameObject characterMenu;

	private Vector3 prevMousePosition;
	public Vector4 limit;
	
	public Vector2 distanceTraveled = new Vector2(0, 0);
	
	private bool canMove;
	private bool quickHarvest = false;
	
	//private Button quickHarvestButton;

    private PointerEventData pointerData;

    private List<RaycastResult> rayResults;

    private string initialTag;
    private int initialRay;

    public GameObject CharacterMenu;

    // Use this for initialization
    void Start ()
    {
        selectedSoul = null;
        soulIsSelected = false;
        selectedImage.SetActive(false);

        rayResults = new List<RaycastResult>();

        characterMenu.GetComponent<CharacterMenu>().Hide();

        initialTag = "";
        initialRay = 0;

        //quickHarvestButton = GameObject.Find("GUI").transform.FindChild("QuickHarvest").GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(selectedSoul);

        if (selectedSoul == null || !soulIsSelected)
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

        

        if (Input.GetMouseButtonDown(0))
        {
            pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition;

            EventSystem.current.RaycastAll(pointerData, rayResults);

            prevMousePosition = Input.mousePosition;

            if(rayResults.Count > 0)
            {
                initialTag = rayResults[0].gameObject.tag;
            }

            initialRay = rayResults.Count;
        }
		
		if(Input.GetMouseButton(0))
        {

            if (initialRay == 0 || initialTag == "PlotPoint")
            {
                Debug.Log("soul is not clicked");
                soulIsSelected = false;
            }

            if (canMove)
			{
				if(distanceTraveled.x + (.25f * (prevMousePosition.x - Input.mousePosition.x)) < 30 && distanceTraveled.x + (.25f * (prevMousePosition.x - Input.mousePosition.x)) > -30)
				{
					Camera.main.transform.Translate((.25f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.x += .25f * (prevMousePosition.x - Input.mousePosition.x);
				}
				if(distanceTraveled.y + (.25f * (prevMousePosition.y - Input.mousePosition.y)) < 30 && distanceTraveled.y + (.25f * (prevMousePosition.y - Input.mousePosition.y)) > -30)
				{
					Camera.main.transform.Translate(-(.25f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), 0, (.25f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * -Camera.main.transform.eulerAngles.y)), Space.World);
					distanceTraveled.y += .25f * (prevMousePosition.y - Input.mousePosition.y);
				}
			}
            //combines the previous 2 lines into 1
            //Camera.main.transform.Translate((.5f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Cos(Mathf.Deg2Rad * 40)) - (.5f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Sin(Mathf.Deg2Rad * 40)), 0, (.5f * (prevMousePosition.y - Input.mousePosition.y) * Mathf.Cos(Mathf.Deg2Rad * 40)) + (.5f * (prevMousePosition.x - Input.mousePosition.x) * Mathf.Sin(Mathf.Deg2Rad * 40)), Space.World);
			if(canMove || Vector2.Distance(Input.mousePosition, prevMousePosition) > 10 && (initialRay == 0 || initialTag == "PlotPoint"))
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

    public void SelectSoul(GameObject Soul)
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

    public void HarvestSelectedSoul()
    {
        selectedSoul.GetComponent<Soul>().Harvest();

        selectedSoul = null;
    }

    private void DisplaySelectedSoulInfo()
    {
        soulMenu.GetComponent<SoulMenu>().Show();

        soulMenu.transform.GetChild(0).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerSecond.ToString();

        soulMenu.transform.GetChild(1).GetComponent<Text>().text = ((int)selectedSoul.GetComponent<Soul>().lifespan).ToString();

        soulMenu.transform.GetChild(2).GetComponent<Text>().text = selectedSoul.GetComponent<Soul>().ectoPerHarvest.ToString();
    }

    public void ToggleQuickHarvest()
	{
		quickHarvest = !quickHarvest;
	}

    public bool QuickHarvest
	{
		get
		{
			return quickHarvest;
		}
    }

    public void SetCharacterMenu(bool activation)
    {
        if(activation)
        {
            characterMenu.GetComponent<CharacterMenu>().Show();
        }
        else
        {
            characterMenu.GetComponent<CharacterMenu>().Hide();
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
