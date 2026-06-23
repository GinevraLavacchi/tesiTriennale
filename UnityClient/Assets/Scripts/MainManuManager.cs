using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuMangaer : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject loadPage;
    [SerializeField] private Animator animator;
    [SerializeField] private Texture2D mouseArrow;
    //public AudioClip button;
    public void Awake()
    {
        settings.SetActive(false);
        loadPage.SetActive(false);
    }
    private void Start()
    {
        Cursor.SetCursor(mouseArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void StartNewGame()
    {
        animator.SetTrigger("isClicked");
        //audio.PlaySoundEffect(audio.buttonSound);
        StartCoroutine(Wait());
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("SampleScene");
    }
    public void ShowSettings()
    {
        animator.SetTrigger("isClicked");
        //button = audio.buttonSound;
        //audio.PlayCaveMusic();
        settings.SetActive(true);
    }
    public void ShowloadPage()
    {
        animator.SetTrigger("isClicked");
        //audio.PlaySoundEffect(audio.buttonSound);
        loadPage.SetActive(true); 
    }
    public void CloseSettings()
    {
        //audio.PlaySoundEffect(audio.buttonSound);
        settings.SetActive(false);
    }
    public void CloseloadPage()
    {
        //audio.PlaySoundEffect(audio.buttonSound);
        loadPage.SetActive(false);
    }
}
