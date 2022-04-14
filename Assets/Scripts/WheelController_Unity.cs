using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelController_Unity : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float maxBrakingTorque;

    public XRNode inputSource1;
    public XRNode inputSource2;
    private Vector2 inputAxis1;
    private Vector2 inputAxis2;
    private bool leftTrigger;
    public void FixedUpdate()
    {
        ////the motion with arrow keys ans wasd
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //assigning the devices in unity to the variables
        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputSource1);
        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);

        ////Getting input from left trigger to brake or not brake
        //leftTrigger = device1.TryGetFeatureValue(CommonUsages.gripButton, out leftTrigger);
        //Debug.Log(leftTrigger);
        
        if (device1.TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
        {
            Debug.Log(leftTrigger);
            float motor = 0;
            float steering = 0;
            float brake = maxBrakingTorque;
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
                if (axleInfo.brake)
                {
                    axleInfo.leftWheel.brakeTorque = brake;
                    axleInfo.rightWheel.brakeTorque = brake;
                }
            }
        }
        else
        {
            //Getting the joystick input of left hand for forward and reverse motion
            device1.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis1);
            float motor = maxMotorTorque * inputAxis1.y;

            //Getting the joystick input of right hand for left and right motion        
            device2.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis2);
            float steering = maxSteeringAngle * inputAxis2.x;

            float brake = 0;
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
                if (axleInfo.brake)
                {
                    axleInfo.leftWheel.brakeTorque = brake;
                    axleInfo.rightWheel.brakeTorque = brake;
                }
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
    public bool brake;
}