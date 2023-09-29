using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private GameObject loseCanvas, winCanvas, FightImage;
    
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
                Debug.Log("I'm player one");
            }
            else _playerTwo = model;
        
        
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
