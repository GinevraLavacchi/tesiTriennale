using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private MinesManager minesManager;
    private bool nearHole;

    private void Start()
    {
        minesManager = FindObjectOfType<MinesManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && nearHole)
        {
            minesManager.Descend();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag == "Hole")
        {
            nearHole = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Hole")
        {
            nearHole = false;
        }
    }

}
