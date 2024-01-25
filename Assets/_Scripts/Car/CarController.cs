using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    [Header("Car Settings")]
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform[] wheelsGroup;
    private Transform frontLeftWheelTransform, frontRightWheelTransform;
    private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [Header("Events")]
    public UnityEvent onCollectibleCollected;

    [Header("SFX")]
    [SerializeField] private AudioSource honkSource;
    [SerializeField] private AudioClip honk;
    [SerializeField] private AudioSource engineSource;
    [SerializeField] private AudioClip engine;
    private float enginePitch = 0.5f;
    private float carSpeed;

    private void Start()
    {
        foreach (Transform wheelGroup in wheelsGroup)
        {
            if (!wheelGroup.gameObject.activeSelf) continue;
            foreach (Transform wheel in wheelGroup)
            {
                if (wheel.CompareTag("FLWheel")) frontLeftWheelTransform = wheel;
                if (wheel.CompareTag("FRWheel")) frontRightWheelTransform = wheel;
                if (wheel.CompareTag("BLWheel")) rearLeftWheelTransform = wheel;
                if (wheel.CompareTag("BRWheel")) rearRightWheelTransform = wheel;
            }
            break;
        }

        StartMotorSound();
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateMotorSound();
    }

    private void UpdateMotorSound()
    {
        carSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        enginePitch = carSpeed / 25f;
        engineSource.pitch = enginePitch;
        Debug.Log(enginePitch);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            onCollectibleCollected.Invoke();
        }
        if (other.CompareTag("Void"))
        {
            SceneManager.LoadScene("Workshop");
        }
    }

    public void Honk(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!honkSource) return;
        honkSource.PlayOneShot(honk);
    }

    public void Brake(InputAction.CallbackContext context)
    {
        isBreaking = true;
        if (context.canceled) isBreaking = false;
    }

    public void Drive(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
    }

    public void BackToWorkshop(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Workshop");
    }

    private void StartMotorSound()
    {
        if (!engineSource) return;
        engineSource.clip = engine;
        engineSource.loop = true;
        engineSource.Play();
    }
}
