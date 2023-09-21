using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void BTN_Play(string Scene) => SceneManager.LoadScene(Scene);        

    public void BTN_Exit() => Application.Quit();

  
}