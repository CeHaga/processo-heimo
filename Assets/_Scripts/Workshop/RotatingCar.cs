using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCar : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.15f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
