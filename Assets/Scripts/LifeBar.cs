//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Slider _slider;
    private void Awake() => _slider = GetComponent<Slider>();
    public void UpdateLifeBar(float life) => _slider.value = life;
}