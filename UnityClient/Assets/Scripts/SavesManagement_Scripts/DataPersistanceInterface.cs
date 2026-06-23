using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataPersistanceInterface
{
    void LoadData(GameData data);

    void SaveData(ref GameData data); //ref because the implementing script is allowed to modify  the data
}
