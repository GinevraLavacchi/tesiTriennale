using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cave : MonoBehaviour
{
    private GameObject canvas;
    private Scene MinesFloor;

    private void Start()
    {
            //canvas = GameObject.FindWithTag("CounterCanvas");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas = GameObject.FindWithTag("CounterCanvas");
        if (collision.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name == "MinesForest")
            {
                SceneManager.LoadScene("MinesFloor");
                collision.transform.position = new Vector3(10, -12, 0);
            } else if(SceneManager.GetActiveScene().name == "MinesFloor")
            {
                GameObject.Destroy(canvas);
                SceneManager.LoadScene("MinesForest");
                collision.transform.position = new Vector3(20.5f, 0, 0);
            }
        }
    }
}
