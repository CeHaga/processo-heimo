using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float rotationHeight = 0.005f;
    void Update()
    {
        // Float and rotate the collectible
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * rotationHeight, transform.position.z);
        transform.Rotate(0, rotationSpeed, 0);
    }
}
