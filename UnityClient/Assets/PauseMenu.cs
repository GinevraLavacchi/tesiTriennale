using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject SampleSceneCanvas;
    [SerializeField] private GameObject TimeCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //SampleSceneCanvas = GameObject.FindWithTag("SampleSceneCanvas");
        //TimeCanvas = GameObject.FindWithTag("TimeCanvas");
        panel.SetActive(false);
    }

    public void ShowPanel()
        { panel.SetActive(true); Time.timeScale = 0f; }
    public void HidePanel()
        { panel.SetActive(false); Time.timeScale = 1f; }
    public void ToMain()
    {
       // SampleSceneCanvas.SetActive(false);
        //TimeCanvas.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }
}
