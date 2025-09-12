using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeButtonsHelper : MonoBehaviour
{
    public Button selectGamesButton;
    public Button quitButton;
    public Button continueButton;
    public GameObject selectMenu;
    public GameObject mainMenu;

    private void Start()
    {
        selectGamesButton.onClick.AddListener (delegate { GameManager.instance.SelectGamesButton(); });
        quitButton.onClick.AddListener (delegate { GameManager.instance.Quit(); });
        continueButton.onClick.AddListener (delegate { GameManager.instance.Continue(); });

        GameManager.instance.selectMenu = selectMenu;
        GameManager.instance.mainMenu = mainMenu;
    }
}
