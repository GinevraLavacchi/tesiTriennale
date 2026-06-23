using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private DayNightCycle time;
    private bool onBed;

    private void Start()
    {
        time = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<DayNightCycle>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && onBed)
        {
            time.Sleep();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onBed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onBed = false;
    }



}
    
