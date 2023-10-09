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
    public LifeBar sliderP1, sliderP2;


    [SerializeField]
    private GameObject loseCanvas, winCanvas, tieCanvas, FightImage, _waitingPlayerCanvas;

    public int PlayerCounting { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private float _timer = 90;

    private bool _matchOn = false;

    public bool MatchState => _matchOn;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    IEnumerator Desapear()
    {
        yield return new WaitForSeconds(2f);
        _matchOn = true;
        FightImage.gameObject.SetActive(false);

    }



    private void Update()
    {
        if (!_matchOn) return;

        _timer -= Time.deltaTime;

        if (_timer <= 0)
            MatchFinishedByTime();
    }

    private void MatchFinishedByTime()
    {
        _matchOn = false;

        if (_playerOne._life == _playerTwo._life)
            tieCanvas.SetActive(true);
        else if (_playerOne._life > _playerTwo._life)
            _playerTwo.RPC_Lose();
        else
            _playerOne.RPC_Lose();
    }

    public void AddPLayer(PlayerModel model)
    {
        if (!_players.Contains(model))
        {
            _players.Add(model);

            if (Object.HasStateAuthority)
            {
                if (model.HasInputAuthority)
                {
                    _playerOne = model;
                    _players[0].SetPosition(_playerTwoSpawnPoint[0].position);

                    _playerOne.lifeBar = sliderP1;
                    _playerOne.lifeBar.UpdateLifeBar(model._life / model._maxLlife);

                    Verification();
                }
                else
                {
                    _playerTwo = model;
                    _players[0].SetPosition(_playerTwoSpawnPoint[1].position);

                    _playerTwo.lifeBar = sliderP2;
                    _playerTwo.lifeBar.UpdateLifeBar(model._life / model._maxLlife);

                }
            }
            else
            {
                if (model.HasStateAuthority)
                {
                    _playerTwo = model;
                    _players[0].SetPosition(_playerTwoSpawnPoint[1].position);

                    _playerTwo.lifeBar = sliderP2;
                    _playerTwo.lifeBar.UpdateLifeBar(model._life / model._maxLlife);
                }
                else
                {
                    _playerOne = model;
                    _players[0].SetPosition(_playerTwoSpawnPoint[0].position);

                    _playerOne.lifeBar = sliderP1;
                    _playerOne.lifeBar.UpdateLifeBar(model._life / model._maxLlife);

                    Verification();
                }
            }

            if (_playerOne != null && _playerTwo != null)
            {
                Verification();

                _playerOne.SetPosition(_playerTwoSpawnPoint[0].position);
                _playerOne.MatchStarted();
                _playerTwo.MatchStarted();
                _playerTwo.SetPosition(_playerTwoSpawnPoint[1].position);


                FightImage.gameObject.SetActive(true);
                StartCoroutine(Desapear());

            }
        }
    }

    public void RemovePlayer(PlayerModel model)
    {
        if(_players.Contains(model))
            _players.Remove(model);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_RESET()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BTN_GoTomenu()
    {
        RPC_RESET();
    }

    public void Verification()
    {
        if (_players.Count == 1)
        {
            _timerText.gameObject.SetActive(false);
            _waitingPlayerCanvas.SetActive(true);
        }
        else
        {
            _waitingPlayerCanvas.SetActive(false);
            _timerText.gameObject.SetActive(true);
        }
    }
    
    public override void FixedUpdateNetwork()
    {
       if (!_matchOn) return;
        
        _timerText.text = _timer.ToString("0");
    }

    public void PlayerDeath()
    {
        Time.timeScale =0f;
        _matchOn = false;
        loseCanvas.SetActive(true);
    }

    public void PlayerWin()
    {
        Time.timeScale =0f;
        _matchOn = false;
        winCanvas.SetActive(true);
    }
}