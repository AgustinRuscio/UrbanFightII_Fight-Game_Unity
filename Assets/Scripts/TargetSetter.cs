using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSetter : MonoBehaviour
{
    public static TargetSetter Instance;


    List<PlayerModel> _players = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPlayer(PlayerModel playerToAdd)
    {
        if (!_players.Contains(playerToAdd))
            _players.Add(playerToAdd);

        if (_players.Count > 1)
        {
            _players[0].SetTarget(_players[1].transform);
            _players[1].SetTarget(_players[0].transform);

        }
    }

    public void RemovePlayer(PlayerModel playerToAdd)
    {
        if (_players.Contains(playerToAdd))
            _players.Add(playerToAdd);

        foreach (PlayerModel playerToRemove in _players)
        {
            playerToRemove.SetTarget(null);
        }
    }
}
