using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]
[RequireComponent(typeof(NetworkSceneManagerDefault))]
public class NetWorkRunnerHandler : MonoBehaviour
{
    private NetworkRunner _networkRunner;

    private void Start()
    {
        _networkRunner = GetComponent<NetworkRunner>();

        var cleintTask = InitializeGame(GameMode.Shared, SceneManager.GetActiveScene().buildIndex);
    }

    Task InitializeGame(GameMode gameMode, SceneRef sceneToLoad)
    {
        var sceneManager = GetComponent<NetworkSceneManagerDefault>();

        //Le digo que tome inputs
        _networkRunner.ProvideInput = true;

        return _networkRunner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Scene = sceneToLoad,
            SessionName = "GameSession",
            SceneManager = sceneManager,
            PlayerCount = 2,
        });
    }
}
