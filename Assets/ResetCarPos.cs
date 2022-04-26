using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCarPos : MonoBehaviour
{
    //Vector3 startPos;
    //public GameObject GO;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    startPos = new Vector3(GO.transform.position.x, GO.transform.position.y, GO.transform.position.z);
    //}

    //// Update is called once per frame
    //public void resetButton()
    //{
    //    GO.transform.position = startPos;
    //}
    private Vector3 startPos;
    private Quaternion startRot;
    public Rigidbody obj;
    public GameObject GO;

    void Awake()
    {
        startPos = new Vector3(GO.transform.position.x, GO.transform.position.y, GO.transform.position.z);
        Debug.Log(startPos);
        startRot = new Quaternion(obj.transform.rotation.x, obj.transform.rotation.y, obj.transform.rotation.z, obj.transform.rotation.w);
    }

    public void resetPos()
    {
        Debug.Log(transform.position);
        Debug.Log("Resetting the Position");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().position = startPos;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().rotation = startRot;
        transform.position = startPos;
    }
}
