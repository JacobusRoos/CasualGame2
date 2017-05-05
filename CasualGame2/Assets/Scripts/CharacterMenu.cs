using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour {

    public RectTransform GUITransform;

    private Vector2 showPosition;
    private Vector2 hidePosition;

	// Use this for initialization
	void Start () {
        showPosition = this.GetComponent<RectTransform>().anchoredPosition;
        hidePosition = new Vector2(showPosition.x + GUITransform.rect.width, hidePosition.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = showPosition;
    }

    public void Hide()
    {
        this.GetComponent<RectTransform>().anchoredPosition = hidePosition;
    }

}
