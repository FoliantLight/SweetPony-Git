using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private GameObject authPanel;
    private GameObject regPanel;
    private GameObject menuPanel;

    void Awake()
    {
        authPanel = GameObject.Find("Auth");
        regPanel = GameObject.Find("Reg");
        menuPanel = GameObject.Find("Menu");
    }

    // Use this for initialization
    void Start () {
        authPanel.SetActive(false);
        regPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
	
    public void setActivePanel(string input)
    {
        authPanel.SetActive(false);
        regPanel.SetActive(false);
        menuPanel.SetActive(false);

        switch(input)
        {
            case "auth":
                authPanel.SetActive(true);
                break;
            case "menu":
                menuPanel.SetActive(true);
                break;
            case "reg":
                regPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
