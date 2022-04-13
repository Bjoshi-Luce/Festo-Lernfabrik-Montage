using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target1st;
    //public Transform target3rd;
    //public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        transform.position = target1st.position;
        transform.rotation = target1st.rotation;
    }
}
