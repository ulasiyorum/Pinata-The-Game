using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject AudioManager;
    public GameObject[] muteItems;
    public Button buton;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if (buton.GetComponentInChildren<Text>().text.ToLower() == "menu")
        {
            menu.SetActive(true);
            buton.GetComponentInChildren<Text>().text = "Go Back";
            buton.GetComponentInChildren<Text>().fontSize = 24;
        }
        else if(buton.GetComponentInChildren<Text>().text.ToLower() == "go back")
        {
            menu.SetActive(false);
            buton.GetComponentInChildren<Text>().text = "MENU";
            buton.GetComponentInChildren<Text>().fontSize = 28;
        }
    }
    public void Mute()
    {
        if (muteItems[0].activeInHierarchy)
        {
            muteItems[0].SetActive(false);
            AudioManager.SetActive(false);
            muteItems[1].SetActive(true);
        }
        else if (muteItems[1].activeInHierarchy)
        {
            muteItems[1].SetActive(false);
            AudioManager.SetActive(true);
            muteItems[0].SetActive(true);
        }
    }
}
