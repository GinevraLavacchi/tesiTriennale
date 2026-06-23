using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private GameObject stoneType;
    [SerializeField] private int minDrops = 1;
    [SerializeField] private int maxDrops = 3;
    [SerializeField] private int hit;

    public void TakeHit()
    {
        hit--;
        if (hit == 0)
        {
            DestroyStone(stoneType);
        }
    }

    public void DestroyStone(GameObject stoneType)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player playerscript= player.GetComponent<Player>();
        playerscript.ToolUsed();
        Destroy(gameObject);

        int numDrops = Random.Range(minDrops, maxDrops + 1);

        for (int i = 0; i < numDrops; i++)
        {
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Instantiate(stoneType, dropPosition, Quaternion.identity);
        }
    }
}
