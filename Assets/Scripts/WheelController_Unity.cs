using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelController_Unity : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public XRNode inputSource1;
    public XRNode inputSource2;
    private Vector2 inputAxis1;
    private Vector2 inputAxis2;
    public void FixedUpdate()
    {
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputSource1);
        device1.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis1);
        float motor = maxMotorTorque * inputAxis1.x;

        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);
        device2.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis2);
        float steering = maxMotorTorque * inputAxis2.y;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}