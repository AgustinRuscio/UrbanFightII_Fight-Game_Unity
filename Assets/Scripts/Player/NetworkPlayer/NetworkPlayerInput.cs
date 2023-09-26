using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerInput : MonoBehaviour
{

    NetworkInputData _inputData;

    bool isJumpPessed;

    void Start() => _inputData = new();
    

    void Update()
    {
        _inputData.xMovement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
            isJumpPessed = true;
    }

    public NetworkInputData GetInputData()
    {
        _inputData.isJump = isJumpPessed;
        isJumpPessed = false;

        return _inputData;
    }
}
