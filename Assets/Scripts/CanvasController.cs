//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void BTN_Play(string Scene) => SceneManager.LoadScene(Scene);        

    public void BTN_Menu() => SceneManager.LoadScene("MainMenu");        

    public void BTN_Exit() => Application.Quit();
}