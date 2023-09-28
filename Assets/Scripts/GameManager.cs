using System.Collections;
using System.Collections.Generic;
using UnityEngine;using TMPro;
using Fusion;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Transform[] _playerTwoSpawnPoint;

    [SerializeField]
    public List<Transform> Players;

    [SerializeField]
    private GameObject loseCanvas, winCanvas;
    
    public int PlayerCounting { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _timerText, counter;

    private float _timer = 90;

    public bool MatchOn { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   
    

    private void Update()
    {
        if (!MatchOn) return;

            _timer -= Time.deltaTime;

        if (_timer <= 0)
            MatchFinishedByTime();
    }

    private void MatchFinishedByTime()
    {
        MatchOn = false;


    }

    public override void FixedUpdateNetwork()
    {
        if (MatchOn)
        {
            _timerText.text = _timer.ToString("0");
        }
        else
        {
            _timerText.gameObject.SetActive(false);
        }

        counter.text = Players.Count.ToString();
    }
    
    

    public void PlayerDeath()
    {
        Time.timeScale =0f;
        MatchOn = false;
        loseCanvas.SetActive(true);
    }

    public void PlayerWin()
    {
        Time.timeScale =0f;
        MatchOn = false;
        winCanvas.SetActive(true);
    }


}
