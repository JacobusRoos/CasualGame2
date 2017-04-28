using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithDesaturated : MonoBehaviour {

    public Sprite coloredImage;
    public Sprite desaturetedImage;
    public bool activated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InvertActive()
    {
        activated = !activated;
        if (activated)
        {
            GetComponent<Image>().sprite = coloredImage;
        }
        else
        {
            GetComponent<Image>().sprite = desaturetedImage;
        }
    }
}
