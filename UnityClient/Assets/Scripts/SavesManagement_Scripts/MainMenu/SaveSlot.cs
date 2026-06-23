using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profile = "";

    [Header("Content")]
    [SerializeField] private GameObject noData;
    [SerializeField] private GameObject hasData;
    [SerializeField] private TextMeshProUGUI dayCountText;
    [SerializeField] private TextMeshProUGUI slotNameText;
    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = GetComponent<Button>();

        if (saveSlotButton == null)
        {
            saveSlotButton = GetComponentInChildren<Button>();
        }
    }

    public void SetButtonText(GameData data)
    {
        if (data == null)
        {
            if (noData != null) noData.SetActive(true);
            if (hasData != null) hasData.SetActive(false);

            if (slotNameText != null)
                slotNameText.text = "EMPTY";
        }
        else
        {
            if (noData != null) noData.SetActive(false);
            if (hasData != null) hasData.SetActive(true);

            if (slotNameText != null)
                slotNameText.text = profile; // slot1, slot2, slot3

            if (dayCountText != null)
                dayCountText.text = "DAY " + data.day;
        }
    }

    public string GetProfileId()
    {
        return this.profile;
    }

    public void SetInteractable(bool isInteractable)
    {
        if (saveSlotButton != null)
        {
            saveSlotButton.interactable = isInteractable;
        }
        else
        {
            Debug.LogWarning("Nessun Button trovato in " + gameObject.name);
        }
    }

}
