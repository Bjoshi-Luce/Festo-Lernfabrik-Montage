using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEvents : MonoBehaviour
{
    public Light light;
    private Color defaultLightColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultLightColor = light.color;
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(ColorChange);
        socket.onSelectExited.AddListener(LightsOut);
    }

    public void ColorChange(XRBaseInteractable obj)
    {
        light.gameObject.SetActive(true);
        ColorChange colorChange = obj.gameObject.GetComponent<ColorChange>();
        if (colorChange != null)
            light.color = colorChange.GetComponent<Color>();
        else
            light.color = defaultLightColor;
    }

    public void LightsOut(XRBaseInteractable obj)
    {
        light.color = defaultLightColor;
    }
}
