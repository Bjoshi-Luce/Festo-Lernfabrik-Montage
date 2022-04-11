using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public Color color = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Standard"));
        mr.material.SetColor("_Color", color);
    }

}
