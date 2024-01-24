using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float rotationHeight = 0.005f;
    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = Time.time - startTime;
        float height = transform.position.y + Mathf.Sin(t) * rotationHeight;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        transform.Rotate(0, rotationSpeed, 0);
    }
}
