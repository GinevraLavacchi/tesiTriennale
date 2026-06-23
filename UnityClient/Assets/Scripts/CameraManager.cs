using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public Transform target;

    private Vector3 vel= Vector3.zero;

    private void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition=target.position+offset;
        targetPosition.z=transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref vel,damping);
    }
}
