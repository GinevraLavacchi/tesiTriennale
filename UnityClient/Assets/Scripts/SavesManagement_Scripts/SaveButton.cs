using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void Save()
    {
        DataPersistanceManager.instance.SaveGameButton();
    }
}