//--------------------------------------------
//          Agustin Ruscio & Merdeces Riego
//--------------------------------------------


using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    public List<Transform> players = new ();
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

    public void AddPlayer(Transform playerTransform)
    {
        if(!players.Contains(playerTransform))
            players.Add(playerTransform);
    }

    public void RemovePlayer(Transform playerTransform)
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
            middlePoint += player.position;
        }

        middlePoint /= players.Count;

        middlePoint.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, middlePoint, cameraSpeed * Time.deltaTime);
    }
}