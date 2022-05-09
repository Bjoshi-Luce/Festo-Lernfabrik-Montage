using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("Verteiler-VR sockets XRIT");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void hauptmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Ferrari()
    {
        SceneManager.LoadScene("Drive Ferrari");
    }

    public void Prufenscene()
    {
        SceneManager.LoadScene("Prüfen-VR sockets XRIT");
    }
    public void Bearbeitenscene()
    {
        SceneManager.LoadScene("Bearbeiten-VR sockets XRIT");
    }
    public void Handhabenscene()
    {
        SceneManager.LoadScene("Handhaben-VR sockets XRIT");
    }
}
