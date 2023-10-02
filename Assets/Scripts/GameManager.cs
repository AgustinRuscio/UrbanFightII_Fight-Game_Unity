using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Fusion;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Transform[] _playerTwoSpawnPoint;

    [SerializeField]
    public List<PlayerModel> _players;

    public PlayerModel _playerOne;
    public PlayerModel _playerTwo;

    [SerializeField]
    public Slider sliderP1, sliderP2;
    

    [SerializeField]
    private GameObject loseCanvas, winCanvas, FightImage, _waitingPlayerCanvas;
    
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

    IEnumerator Desapear()
    {
        yield return new WaitForSeconds(1f);
        FightImage.gameObject.SetActive(false);
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

    public void AddPLayer(PlayerModel model)
    {
        RPC_AddPLayer(model);


        //if (!Object.HasStateAuthority) return;
        //
        //if (!_players.Contains(model))
        //       _players.Add(model);

            //if (!Object.HasStateAuthority)
            //    RPC_AddPLayer(model);
            //else
            //{
            //    if (!_players.Contains(model))
            //        _players.Add(model);
            //}
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_AddPLayer(PlayerModel model)
    {
        if (!_players.Contains(model))
        {
            _players.Add(model);

            if (_playerOne == null)
            {
                _playerOne = model;
                _players[0].SetPosition(_playerTwoSpawnPoint[0].position);
                Debug.Log("I'm player one");
                sliderP1.value = _playerOne._life;
                _waitingPlayerCanvas.SetActive(true);
                
                Time.timeScale = 0f;

            }
            else 
            { 
                _playerTwo = model;
                _players[1].SetPosition(_playerTwoSpawnPoint[1].position);
                Debug.Log("I'm player two");
                sliderP2.value = _playerTwo._life;
                _waitingPlayerCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        
        
            if(_playerOne != null && _playerTwo != null)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    _players[i].SetPosition(_playerTwoSpawnPoint[i].position);
                }
            }

        }



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

        counter.text = _players.Count.ToString();


      
    }


    public void UpdateSliders()
    {
        sliderP1.value = _playerOne._life;
        sliderP2.value = _playerTwo._life;
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
