using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMenu : MonoBehaviour {

    public RectTransform GUITransform;

    private Vector2 showPosition;
    private Vector2 hidePosition;

    public GameObject ReapButton;
    public GameObject QuickReapButton;

    private Vector2 buttonShowPosition;
    private Vector2 buttonHidePosition;

    private Vector2 quickButtonShowPosition;
    private Vector2 quickButtonHidePosition;

    // Use this for initialization
    void Start () {
        showPosition = this.GetComponent<RectTransform>().anchoredPosition;
        hidePosition = new Vector2(showPosition.x, showPosition.y - GUITransform.rect.width / 2f);

        buttonShowPosition = ReapButton.GetComponent<RectTransform>().anchoredPosition; 
        buttonHidePosition = new Vector2(buttonShowPosition.x, buttonShowPosition.y - GUITransform.rect.width / 2f);

        quickButtonShowPosition = QuickReapButton.GetComponent<RectTransform>().anchoredPosition;
        quickButtonHidePosition = new Vector2(quickButtonShowPosition.x, quickButtonShowPosition.y - GUITransform.rect.width / 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hide()
    {
        this.GetComponent<RectTransform>().anchoredPosition = hidePosition;
        ReapButton.GetComponent<RectTransform>().anchoredPosition = buttonHidePosition;
        QuickReapButton.GetComponent<RectTransform>().anchoredPosition = quickButtonShowPosition;
    }

    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = showPosition;
        ReapButton.GetComponent<RectTransform>().anchoredPosition = buttonShowPosition;
        QuickReapButton.GetComponent<RectTransform>().anchoredPosition = quickButtonHidePosition;
    }
}
