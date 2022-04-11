using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public XRNode inputSource;

    private float horizontalInput;
    private float verticalInput;
    private float currentsteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FrontLeftCollider;
    [SerializeField] private WheelCollider FrontRightCollider;
    [SerializeField] private WheelCollider RearLeftCollider;
    [SerializeField] private WheelCollider RearRightCollider;

    [SerializeField] private Transform FrontLeftTransform;
    [SerializeField] private Transform FrontRightTransform;
    [SerializeField] private Transform RearLeftTransform;
    [SerializeField] private Transform RearRightTransform;
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        FrontLeftCollider.motorTorque = verticalInput * motorForce;
        FrontRightCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        FrontRightCollider.brakeTorque = currentbreakForce;
        FrontLeftCollider.brakeTorque = currentbreakForce;
        RearRightCollider.brakeTorque = currentbreakForce;
        RearLeftCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
       currentsteerAngle = maxSteerAngle * horizontalInput;
        FrontRightCollider.steerAngle = currentsteerAngle;
        FrontLeftCollider.steerAngle = currentsteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftCollider, FrontLeftTransform);
        UpdateSingleWheel(RearLeftCollider, RearLeftTransform);
        UpdateSingleWheel(FrontRightCollider, FrontRightTransform);
        UpdateSingleWheel(RearRightCollider, RearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
