using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class FloorCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private MinesManager minesManager;
    private int counter;
    private void Awake()
    {
        if (FindObjectsByType<FloorCounter>(FindObjectsSortMode.InstanceID).Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        minesManager = FindAnyObjectByType<MinesManager>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (minesManager == null)
        {
            minesManager = FindAnyObjectByType<MinesManager>();
        }

        if (minesManager == null || text == null)
            return;

        counter = minesManager.GetFloorNumber();
        text.text = counter.ToString("D2");
    }
}
