using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotMenu : MonoBehaviour
{

    private Vector2 plotMenuShow;
    private Vector2 plotMenuHide;

    private Vector2 quickReapShow;
    private Vector2 quickReapHide;

    private Vector2 characterMenuShow;
    private Vector2 characterMenuHide;

    public RectTransform GUITransform;

    public GameObject quickReapButton;
    public GameObject characterMenu;

    // Use this for initialization
    void Start() {
        plotMenuShow = this.GetComponent<RectTransform>().anchoredPosition;
        plotMenuHide = new Vector2(plotMenuShow.x, plotMenuShow.y - GUITransform.rect.height);

        quickReapShow = quickReapButton.GetComponent<RectTransform>().anchoredPosition;
        quickReapHide = new Vector2(quickReapShow.x, quickReapShow.y - GUITransform.rect.height);

        characterMenuShow = characterMenu.GetComponent<RectTransform>().anchoredPosition;
        characterMenuHide = new Vector2(characterMenuShow.x, characterMenuShow.y - GUITransform.rect.height);

        this.GetComponent<RectTransform>().anchoredPosition = plotMenuHide;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowMenu()
    {
        this.GetComponent<RectTransform>().anchoredPosition = plotMenuShow;
        quickReapButton.GetComponent<RectTransform>().anchoredPosition = quickReapHide;
        characterMenu.GetComponent<RectTransform>().anchoredPosition = characterMenuHide;
    }

    public void HideMenu()
    {
        this.GetComponent<RectTransform>().anchoredPosition = plotMenuHide;
        quickReapButton.GetComponent<RectTransform>().anchoredPosition = quickReapShow;
        characterMenu.GetComponent<RectTransform>().anchoredPosition = characterMenuShow;
    }
}
