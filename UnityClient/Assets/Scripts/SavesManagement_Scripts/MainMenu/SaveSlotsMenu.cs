using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    private SaveSlot[] saveSlots;
    private bool isLoading;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void ActivateMenu(bool isLoading)
    {
        this.gameObject.SetActive(true);
        this.isLoading = isLoading;

        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot slot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(slot.GetProfileId(), out profileData);

            slot.SetButtonText(profileData);

            if (profileData == null && isLoading)
            {
                slot.SetInteractable(false);
            }
            else
            {
                slot.SetInteractable(true);
            }
        }
    }
    private IEnumerator LoadAfterScene()
    {
        yield return null;
        yield return null;

        DataPersistanceManager.instance.LoadGame();
    }
    public void OnSaveSlotClicked(SaveSlot slot)
    {
        DataPersistanceManager.instance.ChangeSelectedProfile(slot.GetProfileId());

        Time.timeScale = 1f;

        if (!isLoading)
        {
            DataPersistanceManager.instance.NewGame();
            SceneManager.LoadSceneAsync("SampleScene");
        }
        else
        {
            DataPersistanceManager.instance.LoadGameAfterSceneChange();
            SceneManager.LoadSceneAsync("SampleScene");
        }
    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void DeactivateMenu()
    { 
        this.gameObject.SetActive(false); 
    }
}
