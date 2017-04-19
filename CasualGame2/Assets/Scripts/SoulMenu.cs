using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMenu : MonoBehaviour {

    private Vector2 showPosition;
    private Vector2 hidePosition;

	// Use this for initialization
	void Start () {
        hidePosition = this.GetComponent<RectTransform>().anchoredPosition;
        showPosition = new Vector2(hidePosition.x, hidePosition.y + 160);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hide()
    {
        this.GetComponent<RectTransform>().anchoredPosition = hidePosition;
    }

    public void Show()
    {
        this.GetComponent<RectTransform>().anchoredPosition = showPosition;
    }
}
