using System.Collections;
using System.Collections.Generic;
using UnityEngine;using TMPro;
using Fusion;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    private NetworkPlayer _playerOne, _playerTwo;

    [SerializeField]
    private Transform _playerOneSpawnPoint, _playerTwoSpawnPoint;

    private int _playerCount = 0;

    [SerializeField]
    private GameObject loseCanvas, winCanvas;
    
    public int PlayerCounting { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private float _timer = 90;

    bool MatchStarted;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPlayer(NetworkPlayer playerToAdd)
    {
        if(_playerCount == 0)
        {
            _playerOne = playerToAdd;
            _playerCount++;
            _playerOne.transform.position = _playerOneSpawnPoint.position;
            _playerOne.transform.rotation = _playerOneSpawnPoint.rotation;
            _timerText.gameObject.SetActive(true);
            MatchStarted = true;    
        }
        else
        {
            _playerTwo = playerToAdd;
            _playerTwo.transform.position = _playerTwoSpawnPoint.position;
            _playerTwo.transform.rotation = _playerTwoSpawnPoint.rotation;
        }
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
    }
    
    

    public void PlayerDeath()
    {
        loseCanvas.SetActive(true);
    }

}
