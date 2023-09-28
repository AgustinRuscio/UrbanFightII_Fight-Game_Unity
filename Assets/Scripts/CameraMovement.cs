using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    public List<PlayerModel> players = new List<PlayerModel>();
    public float cameraSpeed = 5f;

    private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void AddPlayer(PlayerModel playerTransform)
    {
        if(!players.Contains(playerTransform))
            players.Add(playerTransform);

        if(players.Count > 1)
        {
            players[0].SetTarget(players[1].transform);
            players[1].SetTarget(players[0].transform);

        }
    }

    public void RemovePlayer(PlayerModel playerTransform)
    {
        if(players.Contains(playerTransform))
            players.Remove(playerTransform);
    }

    private void Update()
    {
        if (players.Count == 0)
            return;

        Vector3 middlePoint = Vector3.zero;
        foreach (var player in players)
        {
            middlePoint += player.transform.position;
        }
        middlePoint /= players.Count;

        middlePoint.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, middlePoint, cameraSpeed * Time.deltaTime);
    }
}
