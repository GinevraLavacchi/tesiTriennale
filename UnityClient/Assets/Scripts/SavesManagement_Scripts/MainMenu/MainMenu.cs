using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu slotsMenu;


    [SerializeField] private Button newGameButton;
    //[SerializeField] private Button continueButton;

    private void Start()
    {
        /*if(!DataPersistanceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
        }

        provo perché appena parte il gioco, si mischiano entrambi i bottoni e non capisco perché
        slotsMenu.DeactivateMenu();
        this.ActivateMenu();*/
    }
    public void OnNewGameClicked()
    {
        /*DisableMenuButtons();
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("MinesForest");*/
        slotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    /*public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("MinesForest");
    }*/

    public void OnLoadGameClicked()
    {
        slotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        //continueButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
