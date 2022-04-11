using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Steering : MonoBehaviour
{
    public ActionBasedController m_leftController;
    public ActionBasedController m_rightController;
    public Transform m_offset;
    public Transform m_steeringWheel;
    public Transform m_steeringWheelChild;
    public WheelCollider m_FLwheel;
    public WheelCollider m_FRwheel;
    public WheelCollider m_RLwheel;
    public WheelCollider m_RRwheel;
    public Transform m_FLwheelTransform;
    public Transform m_FRwheelTransform;
    public float m_accelerationForce;
    public float m_brakeForce;
    public float m_maxSteerAngle;

    private Transform m_target;
    private Vector3 m_fromVector;
    private bool m_steered;
    private float m_angleBetween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_target = other.transform;

            m_offset.position = m_target.position;
            m_offset.localPosition = new Vector3(m_offset.localPosition.x, 0, m_offset.localPosition.z);
            Vector3 dir = m_offset.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir, transform.up);

            m_steeringWheelChild.SetParent(null);
            m_steeringWheel.rotation = rot;
            m_steeringWheelChild.SetParent(m_steeringWheel);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            m_target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_target = null;
            m_steered = false;
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (m_target)
        {
            m_offset.position = m_target.position;
            m_offset.localPosition = new Vector3(m_offset.localPosition.x, 0, m_offset.localPosition.z);
            Vector3 dir = m_offset.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir, transform.up);
            m_steeringWheel.rotation = rot;
            if (m_steered)
            {
                m_angleBetween = Vector3.Angle(m_fromVector, dir);
                Vector3 cross = Vector3.Cross(m_fromVector, dir);
                if (cross.y < 0)
                {
                    m_angleBetween = -m_angleBetween;
                }
                m_fromVector = dir;
                Debug.Log(m_angleBetween);

                float angle = m_FLwheel.steerAngle;
                angle += m_angleBetween / 10;
                angle = Mathf.Clamp(angle, -m_maxSteerAngle, m_maxSteerAngle);
                m_FLwheel.steerAngle = angle;
                m_FRwheel.steerAngle = angle;
                AngleWheel(m_FLwheel, m_FLwheelTransform);
                AngleWheel(m_FRwheel, m_FRwheelTransform);
            }
            else
            {
                m_steered = true;
                m_fromVector = dir;
            }
        }
        if((m_leftController.activateAction.action.ReadValue<float>() > 0.0f) &&(m_rightController.activateAction.action.ReadValue<float>() > 0.0f))
        {
            Debug.Log("Bitte Drucken Sie nur ein Trigger button ");
        }
        else
        {
            if ((m_leftController.activateAction.action.ReadValue<float>() > 0.0f) && (m_rightController.activateAction.action.ReadValue<float>()!> 0.0f))
            {
                m_FLwheel.motorTorque = -(m_leftController.activateAction.action.ReadValue<float>() * m_accelerationForce);
                m_FRwheel.motorTorque = -(m_leftController.activateAction.action.ReadValue<float>() * m_accelerationForce);
                m_RLwheel.motorTorque = -(m_leftController.activateAction.action.ReadValue<float>() * m_accelerationForce);
                m_RRwheel.motorTorque = -(m_leftController.activateAction.action.ReadValue<float>() * m_accelerationForce);
                Debug.Log(m_leftController.activateAction.action.ReadValue<float>());
                //Debug.Log("Brake");
                //m_FLwheel.brakeTorque = m_brakeForce;
                //m_FRwheel.brakeTorque = m_brakeForce;
                //m_RLwheel.brakeTorque = m_brakeForce;
                //m_RRwheel.brakeTorque = m_brakeForce;
            }
            else
            {
                m_FLwheel.brakeTorque = m_brakeForce;
                m_FRwheel.brakeTorque = m_brakeForce;
                m_RLwheel.brakeTorque = m_brakeForce;
                m_RRwheel.brakeTorque = m_brakeForce;
                m_FLwheel.motorTorque = 0;
                m_FRwheel.motorTorque = 0;
                m_RLwheel.motorTorque = 0;
                m_RRwheel.motorTorque = 0;

                //m_FLwheel.brakeTorque = 0;
                //m_FRwheel.brakeTorque = 0;
                //m_RLwheel.brakeTorque = 0;
                //m_RRwheel.brakeTorque = 0;
            }

            if ((m_rightController.activateAction.action.ReadValue<float>() > 0.0f) && (m_leftController.activateAction.action.ReadValue<float>()!> 0.0f))
            {
                m_FLwheel.motorTorque = m_rightController.activateAction.action.ReadValue<float>() * m_accelerationForce;
                m_FRwheel.motorTorque = m_rightController.activateAction.action.ReadValue<float>() * m_accelerationForce;
                m_RLwheel.motorTorque = m_rightController.activateAction.action.ReadValue<float>() * m_accelerationForce;
                m_RRwheel.motorTorque = m_rightController.activateAction.action.ReadValue<float>() * m_accelerationForce;
            }
            else
            {
                m_FLwheel.brakeTorque = m_brakeForce;
                m_FRwheel.brakeTorque = m_brakeForce;
                m_RLwheel.brakeTorque = m_brakeForce;
                m_RRwheel.brakeTorque = m_brakeForce;
                m_FLwheel.motorTorque = 0;
                m_FRwheel.motorTorque = 0;
                m_RLwheel.motorTorque = 0;
                m_RRwheel.motorTorque = 0;
            }
        }        
    }
    
    void AngleWheel(WheelCollider w, Transform t)
    {
        Vector3 pos;
        Quaternion rot;
        w.GetWorldPose(out pos, out rot);
        t.rotation = rot;
    }
}
