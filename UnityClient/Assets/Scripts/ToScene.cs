using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;
    public void Start()
    {

        audioManager = GameObject.FindWithTag("AudioManager");
    }
    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject workbench = GameObject.FindWithTag("Desk");
            GameObject chest = GameObject.FindWithTag("Chest");
            PlayerHarvest plh = collision.GetComponent<PlayerHarvest>();

            GameObject[] wheats = GameObject.FindGameObjectsWithTag("Wheat");
            GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("Tomato");
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                SceneManager.LoadScene("MinesForest");
                collision.transform.position=new Vector3(-8,-5,0);
                
                plh.enabled = false;
                //chest.GetComponent<SpriteRenderer>().enabled = false;
                //chest.GetComponent<Collider2D>().enabled = false;
                workbench.GetComponent<SpriteRenderer>().enabled = false;
                workbench.GetComponent<Collider2D>().enabled = false;

                foreach (var w in wheats)
                {
                    w.GetComponent<SpriteRenderer>().enabled = false;
                    w.GetComponent<Collider2D>().enabled = false;
                }
                foreach (var t in tomatoes)
                {
                    t.GetComponent<SpriteRenderer>().enabled = false;
                    t.GetComponent<Collider2D>().enabled = false;
                }
                AudioManager audio=audioManager.GetComponent<AudioManager>();
                audio.PlayCaveMusic();

            }
            else if (SceneManager.GetActiveScene().name=="MinesForest")
            {
                SceneManager.LoadScene("SampleScene");
                collision.transform.position=new Vector3(74.299f,-5,0);
                plh.enabled = true;
                //chest.GetComponent<SpriteRenderer>().enabled = true;
                //chest.GetComponent<Collider2D>().enabled = true;
                workbench.GetComponent<SpriteRenderer>().enabled = true;
                workbench.GetComponent<Collider2D>().enabled = true;
                foreach (var w in wheats)
                {
                    w.GetComponent<SpriteRenderer>().enabled = true;
                    w.GetComponent<Collider2D>().enabled = true;
                }
                foreach (var t in tomatoes)
                {
                    t.GetComponent<SpriteRenderer>().enabled = true;
                    t.GetComponent<Collider2D>().enabled = true;
                }
                AudioManager audio = audioManager.GetComponent<AudioManager>();
                audio.PlayBackgroundMusic();
            }
        }
    }
    
}
