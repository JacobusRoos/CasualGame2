using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMenu : MonoBehaviour {

    public RectTransform GUITransform;

    private Vector2 showPosition;
    private Vector2 hidePosition;

    public GameObject ReapButton;
    private Vector2 buttonShowPosition;
    private Vector2 buttonHidePosition;

    // Use this for initialization
    void Start () {
        showPosition = this.GetComponent<RectTransform>().anchoredPosition;
        hidePosition = new Vector2(hidePosition.x, hidePosition.y - GUITransform.rect.width / 2f);

        buttonShowPosition = ReapButton.GetComponent<RectTransform>().anchoredPosition; 
        buttonHidePosition = new Vector2(buttonHidePosition.x, buttonHidePosition.y - GUITransform.rect.width / 2f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hide()
    {
        this.GetComponent<RectTransform>().anchoredPosition = hidePosition;
        ReapButton.GetComponent<RectTransform>().anchoredPosition = buttonHidePosition;
    }

    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = showPosition;
        ReapButton.GetComponent<RectTransform>().anchoredPosition = buttonShowPosition;
    }
}
