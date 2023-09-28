using System.Collections;
using System.Collections.Generic;
using UnityEngine;using TMPro;
using Fusion;

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

    bool MatchStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   
    

    private void Update()
    {
        if(MatchStarted)
            _timer -= Time.deltaTime;

        if (_timer <= 0)
            MatchFinishedByTime();
    }

    private void MatchFinishedByTime()
    {
        MatchStarted = false;


    }

    public override void FixedUpdateNetwork()
    {
        if (MatchStarted)
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
        loseCanvas.SetActive(true);
    }

}
