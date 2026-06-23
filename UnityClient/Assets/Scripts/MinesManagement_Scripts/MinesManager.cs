using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinesManager : MonoBehaviour
{

    private Stack<string> floors = new Stack<string>();
    private int floorNumber;

    [SerializeField] private Animator animator;

    private bool isAscending;
    private bool isDescending;

    private void Awake()
    {
        if(FindObjectsByType<MinesManager>(FindObjectsSortMode.InstanceID).Length >1)
        {
            Destroy(this.gameObject);
        } else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

    private void Start()
    {
        if (floorNumber == 0)
        {
            floors.Push("MinesFloor");
            floorNumber = 1;
        }
    }

    public void Descend()
    {
        animator.SetTrigger("Enter");
        floors.Push(SceneManager.GetActiveScene().name);
        floors.Push("MinesFloor" + Random.Range(1, 5)); 
        SceneManager.LoadScene(floors.Pop());
        floorNumber++;

        isDescending = true;
        StartCoroutine(SetPlayerPosition());
    }

    public void Ascend()
    {
        if (floors.Count != 0)
        {
            animator.SetTrigger("Enter");
            SceneManager.LoadScene(floors.Pop());
            floorNumber--;
            isAscending = true;
            StartCoroutine(SetPlayerPosition());
        }
    }

    private IEnumerator SetPlayerPosition()
    {
        yield return null;

        GameObject hole = GameObject.FindGameObjectWithTag("Hole");
        GameObject rope = GameObject.FindGameObjectWithTag("Rope");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(isAscending)
        {
            player.transform.position = hole.transform.position + new Vector3(0, -0.5f, 0);
            isAscending = false;
        } else if(isDescending)
        {
            player.transform.position = rope.transform.position + new Vector3(0, -1f, 0);
            isDescending = false;
        }
        animator.SetTrigger("Exit");
    }

    public int GetFloorNumber()
    {
        return floorNumber;
    }

}
