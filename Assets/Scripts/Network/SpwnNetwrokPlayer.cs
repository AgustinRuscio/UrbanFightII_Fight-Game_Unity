using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;

public class SpwnNetwrokPlayer : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField]
    private NetworkPlayer _playerPrefab;

    [SerializeField]
    private Transform _playerOneSpawnPoint, _playerTwoSpawnPoint;

    NetworkPlayerInput _playerInputs;

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!NetworkPlayer.Local) return;
       
        if (!_playerInputs)
        {
            _playerInputs = NetworkPlayer.Local.GetComponent<NetworkPlayerInput>();
        }
        else
        {
            input.Set(_playerInputs.GetInputData());
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        if (runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("[Custom msg] On Connected to Server - Spawning local player");

            NetworkPlayer a = runner.Spawn(_playerPrefab, _playerOneSpawnPoint.position, _playerOneSpawnPoint.rotation, runner.LocalPlayer);

            // if (!runner.ActivePlayers.Any())
            // {
            //     NetworkPlayer a = runner.Spawn(_playerPrefab, _playerOneSpawnPoint.position, _playerOneSpawnPoint.rotation, runner.LocalPlayer);
            //     GameManager.instance.AddPlayer(a);
            // }
            // else
            // {
            //     NetworkPlayer a = runner.Spawn(_playerPrefab, _playerTwoSpawnPoint.position, _playerTwoSpawnPoint.rotation, runner.LocalPlayer);
            //     GameManager.instance.AddPlayer(a);
            // }
        }
    }

    #region cosa

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //Forma uno
        //GameManager.instance.AddPlayer(player);
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        //Cargar menu
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    #endregion
}