using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private NetworkPlayer _playerOne, _playerTwo;

    [SerializeField]
    private Transform _playerOneSpawnPoint, _playerTwoSpawnPoint;

    private int _playerCount = 0;

    public int PlayerCounting { get; private set; }

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
        }
        else
        {
            _playerTwo = playerToAdd;
            _playerTwo.transform.position = _playerTwoSpawnPoint.position;
            _playerTwo.transform.rotation = _playerTwoSpawnPoint.rotation;
        }
        
    }

}
