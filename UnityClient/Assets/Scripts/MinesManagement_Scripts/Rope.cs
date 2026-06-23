using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private MinesManager minesManager;
    private bool nearRope;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && nearRope)
        {
            minesManager.Ascend();
        }
    }

    private void Start()
    {
        minesManager = FindObjectOfType<MinesManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Rope")
        {
            nearRope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Rope")
        {
            nearRope = false;
        }
    }
}
